///////////////////////////////////////////////////////////////
// Persistence.cs - Test Requirements for Project #5         //
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
 * This package persist database to Xml files and load Xml files
 * to databases, DBEngines.
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
using System.Xml;
using static System.Console;
using System.Xml.Linq;

namespace Project2Starter
{
    public static class Persistence
    {
        public static bool Persist2Xml<Key, Value, Data>(this DBEngine<Key, Value> dbStore, string filename)
        {
            if (dbStore.Keys().Count() <= 0) {
                return false;
            }
            XDocument xml = new XDocument();
            xml.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XComment comment = new XComment("Demonstration XML");
            xml.Add(comment);
            XElement root = new XElement("root");
            xml.Add(root);
            foreach (Key key in dbStore.Keys())
            {
                if (typeof(Key) == typeof(int))
                {
                    DBElement<Key, string> val;
                    dbStore.getValue<string>(key, out val);
                    DBElement<int, string> elem = val as DBElement<int, string>;
                    string sChildren = list2String1(elem.children);
                    XElement[] metaData = {
                    new XElement("Element", new XAttribute("TypeofKey", key.GetType().ToString()),
                    new XAttribute("TypeofData", val.GetType().ToString()),
                    new XElement("Key", key),new XElement("Name", elem.name),
                    new XElement("Description", elem.descr),new XElement("ConstructionTime", elem.timeStamp),
                    new XElement("ChildNodes", sChildren),new XElement("Payload", elem.payload))
                };
                    root.Add(metaData);
                }
                else
                {
                    DBElement<Key, List<string>> val;
                    dbStore.getValue<List<string>>(key, out val);
                    DBElement<string, List<string>> elem = val as DBElement<string, List<string>>;
                    string sChildren = list2String2(elem.children);
                    string payloadString = list2String2(elem.payload);
                    XElement[] metaData = {
                    new XElement("Element", new XAttribute("TypeofKey", key.GetType().ToString()),
                    new XAttribute("TypeofData", val.GetType().ToString()),
                    new XElement("Key", key),new XElement("Name", elem.name),
                    new XElement("Description", elem.descr),new XElement("ConstructionTime", elem.timeStamp),
                    new XElement("ChildNodes", sChildren),new XElement("Payload", payloadString))
                };
                    root.Add(metaData);
                }
                xml.Save(filename + ".xml");
                Console.WriteLine();
                Console.Write(xml.ToString());
            }
            return true;
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

        public static bool loadFromXml1(this DBEngine<int, DBElement<int, string>> db, string filename)
        {
            XDocument doc = XDocument.Load(filename);
            string TypeofKey = doc.Root.Element("Element").Attribute("TypeofKey").Value;
            string TypeofData = doc.Root.Element("Element").Attribute("TypeofData").Value;

            if (TypeofKey.Equals("System.Int32"))
            {
                foreach (XElement element in doc.Root.Elements("Element"))
                {
                    int key = Int32.Parse(element.Element("Key").Value);
                    String name = element.Element("Name").Value;
                    String descr = element.Element("Description").Value;
                    String timestamp = element.Element("ConstructionTime").Value;
                    String childrenS = element.Element("ChildNodes").Value;
                    Char[] ss = ",".ToCharArray(); string[] childrenStringA = childrenS.Split(ss);
                    List<int> childKeyL = new List<int>();
                    if (childrenS.Length != 0)
                    {
                        foreach (string s in childrenStringA)
                        {
                            childKeyL.Add(Int32.Parse(s));
                        }
                    }
                    String payload = element.Element("Payload").Value;
                    DBElement<int, string> ele = new DBElement<int, string>();
                    ele.name = name; ele.descr = descr;
                    ele.timeStamp = Convert.ToDateTime(timestamp);
                    db.insert(key, ele);
                }
                return true;
            }
            return false;
        }

        public static bool loadFromXml2(this DBEngine<string, DBElement<string, List<string>>> db, string filename)
        {
            XDocument doc = XDocument.Load(filename);
            string TypeofKey = doc.Root.Element("Element").Attribute("TypeofKey").Value;
            string TypeofData = doc.Root.Element("Element").Attribute("TypeofData").Value;
            if (TypeofKey.Equals("System.String"))
            {
                foreach (XElement element in doc.Root.Elements("Element"))
                {
                    string key = element.Element("Key").Value;
                    string name = element.Element("Name").Value;
                    String descr = element.Element("Description").Value;
                    String timestamp = element.Element("ConstructionTime").Value;
                    String childrenS = element.Element("ChildNodes").Value;
                    String payloadS = element.Element("Payload").Value;
                    Char[] ss = ",".ToCharArray();
                    string[] childrenStringA = childrenS.Split(ss);
                    List<string> childKeyL = new List<string>();
                    foreach (string s in childrenStringA)
                    {
                        childKeyL.Add(s);
                    }
                    String payload = element.Element("Payload").Value;
                    DBElement<string, List<string>> ele = new DBElement<string, List<string>>();
                    ele.name = name; ele.descr = descr;
                    ele.timeStamp = Convert.ToDateTime(timestamp);
                    ele.payload = new List<string>();
                    string[] payloadSA = payloadS.Split(ss);
                    foreach (string s in payloadSA)
                    {
                        ele.payload.Add(s);
                    }
                    db.insert(key, ele);
                }
                return true;
            }
            return false;
        }
    }






    //#if (TEST_PERSISTENCE)
    class TestPersistence
    {
        static void Main(string[] args)
        {
            "Testing Persistence Package".title('=');
            WriteLine();

            Write("\n  All testing of Persistence class moved to DBElementTest package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");

            Write("\n\n");
        }
    }
    //#endif
}

