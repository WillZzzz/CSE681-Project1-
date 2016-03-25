///////////////////////////////////////////////////////////////
// TestExec.cs - Test Requirements for Project #2            //
// Ver 1.2                                                   //
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
 * This package begins the demonstration of meeting requirements.
 * Much is left to students to finish.
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
 *ver  1.2 : 7  Oct 15

 * ver 1.1 : 24 Sep 15
 * ver 1.0 : 18 Sep 15
 * - first release
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Xml;
using System.Xml.Linq;


namespace Project2Starter
{
  class TestExec
  {
    private DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();
    private DBEngine<string, DBElement<string, List<string>>> newdb = new DBEngine<string, DBElement<string, List<string>>>();
    static Scheduler<string, List<String>> scheduler;
    DBElement<string, List<string>> newerelem1;
    DBElement<string, List<string>> newerelem2;
    DBElement<int, string> intString1;
    DBElement<int, string> intString2;
    DBElement<int, string> intString3;
    static string random;

        void TestR2()
    {
            "Demonstrating Requirement #2".title();
            Write("\n --- Test DBElement<int,string> ---");
            intString1 = new DBElement<int, string>();
            intString1.payload = "a payload";
            Write(intString1.showElement<int, string>());
            WriteLine();

            intString2 = new DBElement<int, string>("Darth Vader", "Evil Overlord");
            intString2.payload = "The Empire strikes back!";
            Write(intString2.showElement<int, string>());
            WriteLine();

            intString3 = new DBElement<int, string>("Luke Skywalker", "Young HotShot");
            intString3.children.AddRange(new List<int> { 1, 5, 23 });
            intString3.payload = "X-Wing fighter in swamp - Oh oh!";
            Write(intString3.showElement<int, string>());
            WriteLine();


            Write("\n --- Test DBElement<string,List<string>> ---");
            newerelem1 = new DBElement<string, List<string>>();
            newerelem1.name = "newerelem1";
            newerelem1.descr = "better formatting";
            newerelem1.payload = new List<string> { "alpha", "beta", "gamma" };
            newerelem1.payload.Add("delta");
            newerelem1.payload.Add("epsilon");
            Write(newerelem1.showElement<string, List<string>, string>());
            WriteLine();

            newerelem2 = new DBElement<string, List<string>>();
            newerelem2.name = "newerelem2";
            newerelem2.descr = "better formatting";
            newerelem1.children.AddRange(new[] { "first", "second" });
            newerelem2.payload = new List<string> { "a", "b", "c" };
            newerelem2.payload.Add("d");
            newerelem2.payload.Add("e");
            Write(newerelem2.showElement<string, List<string>, string>());
            WriteLine();

        }
    void TestR3()
    {
        "Demonstrating Requirement #3".title();
        WriteLine();
        Write("\n --- Test DBEngine<int,DBElement<int,string> Insertion ---");
        int key = 0;
        Func<int> keyGen = () => { ++key; return key; };

        bool p1 = db.insert(keyGen(), intString1);
        bool p2 = db.insert(keyGen(), intString2);
        bool p3 = db.insert(keyGen(), intString3);
        if (p1 && p2 && p3)
            Write("\n  all inserts succeeded");
        else
            Write("\n  at least one insert failed");
        db.show<int, DBElement<int, string>, string>();
            WriteLine();

        DBElement<string, List<string>> newelem1 = new DBElement<string, List<string>>();
        newelem1.name = "newelem1";
        newelem1.descr = "test new type";
        newelem1.payload = new List<string> { "one", "two", "three" };
        Write(newelem1.showElement<string, List<string>>());
        WriteLine();

        Write("\n --- Test DBEngine<string,DBElement<string,List<string>>> Insertion ---");

        int seed = 0;
        string skey = seed.ToString();
        Func<string> skeyGen = () =>
        {
            ++seed;
            skey = "string" + seed.ToString();
            skey = skey.GetHashCode().ToString();
            return skey;
        };

        newdb.insert(skeyGen(), newerelem1);
        newdb.insert((TestExec.random = skeyGen()), newerelem2);
        newdb.show<string, DBElement<string, List<string>>, List<string>, string>();
            WriteLine();


       }
    void TestR4()
    {
            "Demonstrating Requirement #4".title();
            WriteLine();
            Write("\n --- Test DBEngine Edition ---");
            db.show<int, DBElement<int, string>, string>();
            DBElement<int, string> editElement1 = new DBElement<int, string>();
            db.getValue<string>(1, out editElement1);
            editElement1.showElement<int, string>();
            db.editName<string>(1, "editedName");
            //editElement.name = "editedName";
            db.editDesc<string>(1, "editedDescription");
            //editElement.descr = "editedDescription";
            db.show<int, DBElement<int, string>, string>();
            Write("\n\n -edit children and payload-");
            DBElement<string, List<string>> editElement2 = new DBElement<string, List<string>>();
            newdb.getValue<List<string>>(TestExec.random, out editElement2);
            List<string> childrenList = new List<string>();
            childrenList.Add("third");
            childrenList.Add("forth");
            newdb.addChild<List<string>>(TestExec.random, childrenList);

            List<string> payloadList = new List<string>();
            payloadList.Add("This is the first load");
            payloadList.Add("This is the second load");
            newdb.editPayload<List<string>>(TestExec.random, payloadList);
            Write(editElement2.showElement<string, List<string>, string>());

            Write("\n --- Test DBEngine Deletion ---");
            Write("\n  the number of elements in db is " + db.Keys().Count());
            Write("\n  start deletion process ");
            IEnumerable<int> keys = new List<int>(db.Keys());
            foreach (var eachKey in keys)
            {
                db.delete(eachKey);
            }
            Write("\n  the number of elements in db is " + db.Keys().Count());
            db.show<int, DBElement<int, string>, string>();
            WriteLine();
        }
  //      void ReinsertElement()
        
            //Write("\n--- Add elements first ---");
            //DBElement<int, string> intString1 = new DBElement<int, string>();
            //intString1.payload = "a payload";
            //Write(intString1.showElement<int, string>());
            //WriteLine();

            //DBElement<int, string> intString2 = new DBElement<int, string>("Darth Vader", "Evil Overlord");
            //intString2.payload = "The Empire strikes back!";
            //Write(intString2.showElement<int, string>());
            //WriteLine();

            //var intString3 = new DBElement<int, string>("Luke Skywalker", "Young HotShot");
            //intString3.children.AddRange(new List<int> { 1, 5, 23 });
            //intString3.payload = "X-Wing fighter in swamp - Oh oh!";
            //Write(intString3.showElement<int, string>());
            //WriteLine();

      

            //int key = 0;
            //Func<int> keyGen = () => { ++key; return key; };

            //bool p1 = db.insert(keyGen(), intString1);
            //bool p2 = db.insert(keyGen(), intString2);
            //bool p3 = db.insert(keyGen(), intString3);
            //if (p1 && p2 && p3)
            //    Write("\n  all inserts succeeded");
            //else
            //    Write("\n  at least one insert failed");
            //db.show<int, DBElement<int, string>, string>();
        
    void TestR5()
    {
            Write("\n--- Add elements first ---");
            DBElement<int, string> intString1 = new DBElement<int, string>();
            intString1.payload = "a payload";
            Write(intString1.showElement<int, string>());
            WriteLine();

            DBElement<int, string> intString2 = new DBElement<int, string>("Darth Vader", "Evil Overlord");
            intString2.payload = "The Empire strikes back!";
            Write(intString2.showElement<int, string>());
            WriteLine();

            var intString3 = new DBElement<int, string>("Luke Skywalker", "Young HotShot");
            intString3.children.AddRange(new List<int> { 1, 5, 23 });
            intString3.payload = "X-Wing fighter in swamp - Oh oh!";
            Write(intString3.showElement<int, string>());
            WriteLine();



            int key = 0;
            Func<int> keyGen = () => { ++key; return key; };

            bool p1 = db.insert(keyGen(), intString1);
            bool p2 = db.insert(keyGen(), intString2);
            bool p3 = db.insert(keyGen(), intString3);
            if (p1 && p2 && p3)
                Write("\n  all inserts succeeded");
            else
                Write("\n  at least one insert failed");
            db.show<int, DBElement<int, string>, string>();


            "Demonstrating Requirement #5".title();
            WriteLine();

            "testing persistence".title();

            db.Persist2Xml<int, DBElement<int, string>, string>("firstPersistence");
            newdb.Persist2Xml<string, DBElement<string, List<String>>, List<String>>("secondPersistence");

            "testing parsing Xml files".title();

            DBEngine<int, DBElement<int, string>> receiveDB = new DBEngine<int, DBElement<int, string>>();
            receiveDB.loadFromXml1("firstPersistence.xml");
            receiveDB.show<int, DBElement<int, string>, string>();

            DBEngine<string, DBElement<string, List<string>>> receiveDB2 = new DBEngine<string, DBElement<string, List<string>>>();
            receiveDB2.loadFromXml2("secondPersistence.xml");
            receiveDB2.show<string, DBElement<string, List<string>>, List<string>, string>();
        }
    void TestR6()
    {
        "Demonstrating Requirement #6".title();
        WriteLine();
        "\n -- Test Schedule -- Persist every 2 seconds--".title();
        scheduler = new Scheduler<string, List<string>>();
        scheduler.schedule();
     }
    void TestR7()
    {
      "Demonstrating Requirement #7".title();
      WriteLine();

            "testing query to database".title();

            string child;
            int thekey = 3;
            //db.queryChild<int, DBElement<int, string>, string>(thekey, out child);
            if (db.queryChild<int, DBElement<int, string>, string>(thekey, out child))
            {
                Console.WriteLine(child);
            }
            else
            {
                Console.WriteLine("This node have no children!");
            }
            //newdb.queryChild<string, DBElement<string, List<string>>, List<string>>("-1587306074", out child);
            if (newdb.queryChild<string, DBElement<string, List<string>>, List<string>>("-1587306074", out child))
            {
                Console.WriteLine(child);
            }
            else
            {
                Console.WriteLine("This node have no children!");
            }
        }
    void TestR8()
    {
      "Demonstrating Requirement #8".title();
      WriteLine();
    }

    void TestR9()
        {
            "Demonstrating Requirement #9".title();
            WriteLine();

            XDocument content = new XDocument();
            content.Declaration = new XDeclaration("1.0", "utf-8", "yes");

            XComment comment = new XComment("Demonstration XML");
            content.Add(comment);

            XElement root = new XElement("Project2");
            content.Add(root);

            XElement[] TestExec =
            {
                new XElement("Package",
                new XElement("Name", "TestExec"),
                new XElement("PackageDependency",
                new XElement("Package","QueryEngine"),
                new XElement("Package","DBEngine"),
                new XElement("Package","Presistence"),
                new XElement("Package","Scheduler"),
                new XElement("Package","Display")))
            };

            XElement[] QueryEngine ={
                new XElement("Package",
                new XElement("Name", "QueryEngine"),
                new XElement("PackageDependency",
                new XElement("Package","DBEngine")))
            };

            XElement[] DBEngine ={
                new XElement("Package",
                new XElement("Name", "DBEngine"),
                new XElement("PackageDependency",
                new XElement("Package","Presistence")))
            };

            XElement[] Presistence ={
                new XElement("Package",
                new XElement("Name", "Presistence"),
                new XElement("PackageDependency",
                new XElement("Package","No Dependency")))
            };

            XElement[] Scheduler ={
                new XElement("Package",
                new XElement("Name", "Scheduler"),
                new XElement("PackageDependency",
                new XElement("Package","DBEngine")))
            };

            XElement[] Display ={
                new XElement("Package",
                new XElement("Name", "Display"),
                new XElement("PackageDependency",
                new XElement("Package","No Dependency")))
            };

            root.Add(TestExec);
            root.Add(QueryEngine);
            root.Add(DBEngine);
            root.Add(Presistence);
            root.Add(Scheduler);
            root.Add(Display);

            content.Save("ContentsOfProject2.xml");
            Console.WriteLine(content);
        }
    static void Main(string[] args)
    {
            TestExec exec = new TestExec();
            "Demonstrating Project#2 Requirements".title('=');
            WriteLine();
            exec.TestR2();
            exec.TestR3();
            exec.TestR4();
            exec.TestR5();
            exec.TestR6();
            exec.TestR7();
            exec.TestR8();
            Write("\n\n");

            Console.ReadLine();

 

        }
    }
}
