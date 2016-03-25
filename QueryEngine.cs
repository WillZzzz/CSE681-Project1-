///////////////////////////////////////////////////////////////
// QueryEngines.cs - Test Requirements for Project #7         //
// Ver 1.0                                                   //
// Application: Demonstration for CSE687-OOD, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Dell XPS2700, Core-i7, Windows 10            //
// Author:      Tianhang Zhang, Syracuse University          //
//              (315) 383-3757, tzhan116@syr.edu             //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package respond to queries on keys and child.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   TestExec.cs,  DBElement.cs, DBEngine, Display, 
 *   DBExtensions.cs, UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 6  Oct 15
 * - first release
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project2Starter
{
    public static class QueryEngine
    {
        public static bool queryValue<Key, Value>(this DBEngine<Key, Value> db, Key key, out DBElement<Key, Value> val)
        {
            if (db.getValue(key, out val))
                return true;
            else
                return false;
        }

        public static bool queryChild<Key, Value, Data>(this DBEngine<Key, Value> db, Key key, out string child)
        {
            DBElement<Key, Data> val;
            if (db.getValue<Data>(key, out val))
            {
                if (typeof(Key) == typeof(int))
                {
                    DBElement<int, string> elem = val as DBElement<int, string>;
                    child = list2String1(elem.children);
                }
                else
                {
                    DBElement<string, List<string>> elem = val as DBElement<string, List<string>>;
                    child = list2String2(elem.children);
                }
                return true;
            }
            else
                child = default(string);
            return false;
        }

        public static string list2String1(List<int> list)
        {
            if (list.Count == 0)
            {
                return "";
            }
            StringBuilder s = new StringBuilder("");
            foreach (int i in list)
            {
                s.Append(i);
                s.Append(",");
            }
            s.Remove(s.Length - 1, 1);
            return s.ToString();
        }

        public static string list2String2(List<string> list)
        {
            if (list.Count == 0)
            {
                return "";
            }
            StringBuilder s = new StringBuilder("");
            foreach (string i in list)
            {
                s.Append(i);
                s.Append(",");
            }
            s.Remove(s.Length - 1, 1);
            return s.ToString();
        }
    }
}