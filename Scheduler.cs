///////////////////////////////////////////////////////////////
// Scheduler.cs - Test Requirements for Project #6            //
// Ver 1.0                                                   //
// Application: Demonstration for CSE687-OOD, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Dell XPS2700, Core-i7, Windows 10            //
// Author:  Tianhang Zhang, Syracuse University              //
//              (315) 383-3757, tzhan116@syr.edu             //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package watch for the execution duration and persist the
 * database every positive duration of time.
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
using System.Timers;


namespace Project2Starter
{
    public class Scheduler<Key, Value>
    {
        private static Timer aTimer;
        int interval = 1000 * 1;
        bool isAutoReset = true;
        bool enable = true;
        //DBEngine<Key, Value> db;

        public Scheduler()
        {
            aTimer = new System.Timers.Timer(interval);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = isAutoReset;
            aTimer.Enabled = enable;
        }

        public void schedule()
        {
            aTimer.Start();
        }
        public void stop()
        {
            aTimer.Stop();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("/n --- Scheduler called persistence ---");
            //db.persist();
        }
    }
}