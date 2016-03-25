///////////////////////////////////////////////////////////////
// DBEngine.cs - define noSQL database                       //
// Ver 1.3                                                   //
// Application: Demonstration for CSE687-OOD, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Dell XPS2700, Core-i7, Windows 10            //
// Author:      Jim Fawcett, CST 4-187, Syracuse University  //
//              (315) 443-3948, jfawcett@twcny.rr.com        //
// Revised by:  Tianhang Zhang, Syracuse University          //
//              (315) 383-3757, tzhan116@syr.edu             //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package implements DBEngine<Key, Value> where Value
 * is the DBElement<key, Data> type.
 *
 * This class is a starter for the DBEngine package you need to create.
 * It doesn't implement many of the requirements for the db, e.g.,
 * It doesn't remove elements, doesn't persist to XML, doesn't retrieve
 * elements from an XML file, and it doesn't provide hook methods
 * for scheduled persistance.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs, and
 *                 UtilityExtensions.cs only if you enable the test stub
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.3 : 5  Oct 15
 * ver 1.2 : 24 Sep 15
 * - removed extensions methods and tests in test stub
 * - testing is now done in DBEngineTest.cs to avoid circular references
 * ver 1.1 : 15 Sep 15
 * - fixed a casting bug in one of the extension methods
 * ver 1.0 : 08 Sep 15
 * - first release
 *
 */
//todo add placeholders for Shard
//todo add reference to class text XML content

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Xml;

namespace Project2Starter
{
  public class DBEngine<Key, Value>
  {
    private Dictionary<Key, Value> dbStore;
    private int editcount = 0;
    public DBEngine()
    {
      dbStore = new Dictionary<Key, Value>();
    }
    public bool insert(Key key, Value val)
    {
      if (dbStore.Keys.Contains(key))
        return false;
      dbStore[key] = val;
      return true;
    }
    public bool getValue(Key key, out Value val)
    {
      if(dbStore.Keys.Contains(key))
      {
        val = dbStore[key];
        return true;
      }
      val = default(Value);
      return false;
    }

    public bool getValue<Data>(Key key, out DBElement<Key,Data> elem)
    {
            if (dbStore.Keys.Contains(key))
            {
                Value val = dbStore[key];
                elem = val as DBElement<Key, Data>;
                return true;
            }
            else
            {
                elem = default(DBElement<Key, Data>);
                return false;
            }
    }

    public IEnumerable<Key> Keys()
{
    return dbStore.Keys;
}
    /*
     * More functions to implement here
     */

    /*
     * New functions coming~~
     */
     

    public bool delete(Key key)
        {
            if (!dbStore.Keys.Contains(key))
                return false;
            dbStore.Remove(key);
            return true;
        }

   public bool editName<Data>(Key key, string name)
        {
            DBElement<Key, Data> elem;
            bool iselem = getValue<Data>(key, out elem);
            if(iselem)
            {
                elem.name = name;
                ++editcount;
                return true;
            }
            return false;
        }

    public bool editDesc<Data>(Key key, string descr)
        {
            DBElement<Key, Data> elem;
            if(getValue<Data>(key, out elem))
            {
                elem.descr = descr;
                ++editcount;
                return true;
            }
            return false;
        }

      public bool deleteChild<Data>(Key key, List<Key> childKey)
        {
            DBElement<Key, Data> elem;
            if(getValue<Data>(key, out elem))
            {
                foreach (Key thekey in childKey)
                {
                    if(elem.children.Contains(thekey))
                    {
                        elem.children.Remove(thekey);
                        ++editcount;
                    }
                }
                return true;
            }
            return false;
        }

    public bool addChild<Data>(Key key, List<Key> childKey)
        {
            DBElement<Key, Data> elem;
            if (getValue<Data>(key, out elem))
            {
                foreach (Key thekey in childKey)
                {
                    if (elem.children.Contains(thekey))
                    {
                        return false;
                    }
                    else
                    {
                        elem.children.Add(thekey);
                        ++editcount;
                        return true;
                    }
                }
            }
            return false;
        }

    public bool editPayload<Data>(Key key, Data payload)
        {
            DBElement<Key, Data> elem;
            if (getValue<Data>(key, out elem))
            {
                elem.payload = payload;
                ++editcount;
                return true;
            }
            return false;
        }
    
    }

#if(TEST_DBENGINE)

  class TestDBEngine
  {
    static void Main(string[] args)
    {
      "Testing DBEngine Package".title('=');
      WriteLine();

      Write("\n  All testing of DBEngine class moved to DBEngineTest package.");
      Write("\n  This allow use of DBExtensions package without circular dependencies.");

      Write("\n\n");
    }
  }
#endif
}
