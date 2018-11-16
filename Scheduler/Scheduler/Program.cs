using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scheduler
{
    class Program
    {
        private static TextReader input = Console.In;//Used for reading text file.
        List<int> periods = new List<int>();
        List<int> exeTime = new List<int>();


        static void Main(string[] args)
        {
            //Variables
            List<string> parameters = new List<string>();//Holds the command line arguments
            List<string> textFile = new List<string>();//Hold the text file line by line
            

            #region Reading Command Line Arguments          
            for (int i = 0; i < args.Length; i++)
            {
                parameters.Add(Convert.ToString(args[i]));
               // Console.WriteLine(parameters[i]);
            }
            #endregion

            #region Reading txt document into list(line by line)
            ////Source: https://stackoverflow.com/questions/12771347/how-to-read-from-file-in-command-line-arguments-otherwise-standard-in-emulate
            if (args.Any())
            {
                var path = parameters[0];//This holds the txt file path.
                if (File.Exists(path))
                {
                    input = File.OpenText(path);
                }
            }
            for (string line; (line = input.ReadLine()) != null;)//Display the txt file line by line.
            {
                //Console.WriteLine(line);
                textFile.Add(line);
            }

            //Close the textfile to save resources
            input.Close();

            #endregion

            //Display their original text file
            Console.WriteLine("Your task are:");
            foreach (var word in textFile)
            {
                Console.WriteLine(word);
            }

            #region Formatting task so they are one element per row/column

            //Console.WriteLine("BEGIN EDITINGONS");
            string[] removeThis = { " ", "  " };
            
            List<string> editedFiles = new List<string>();//Does not contain the frist line
            //editedFiles.Add("-");

            //Parsing the First Line
            List<string> firstLine = new List<string>();
            var lineaUno = textFile[0].Split(removeThis, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lineaUno.Length; i++)
            {
                firstLine.Add(lineaUno[i]);
            }
            //Ending parsing first line

            for (int a = 1; a < textFile.Count; a++)//The a = 1 makes it so first line is not included
            {
                var allvalues = textFile[a].Split(removeThis, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < allvalues.Length; i++)
                {
                    editedFiles.Add(allvalues[i]);
                }
                editedFiles.Add("-");
            }



            //foreach (var VARIABLE in editedFiles)
            //{
            //    Console.WriteLine(VARIABLE);
            //}
            //Console.WriteLine(editedFiles.Count);
            #endregion


            if (parameters.Count == 3)
            {
  
                //See if they want EDF or RM
                if (parameters[1].Equals("EDF") && parameters[2].Equals("EE"))
                {
                    Console.WriteLine("You want EDF W/ EE");
                    EDF(editedFiles, parameters[2]);
                }
                else if (parameters[1].Equals("RM") && parameters[2].Equals("EE"))
                {
                    Console.WriteLine("You want RM W/ EE");
                    RM(editedFiles,firstLine, parameters[2]);
                }
                else if (parameters[1].Equals("EDF"))
                {
                    Console.WriteLine("You want EDF");
                    EDF(editedFiles);

                }
                else if (parameters[1].Equals("RM"))
                {
                    Console.WriteLine("You want RM");
                    RM(editedFiles,firstLine);
                }
                else
                {
                    Console.WriteLine("ERROR");
                }
            }
            else if (parameters.Count == 2)
            {
                if (parameters[1].Equals("EDF"))
                {
                    Console.WriteLine("You want EDF");
                    EDF(editedFiles);
                    // foreach (var param in parameters) { Console.WriteLine(param); }
                }
                else if (parameters[1].Equals("RM"))
                {
                    Console.WriteLine("You want RM");
                    RM(editedFiles,firstLine);
                }
                else
                {
                    Console.WriteLine("ERROR");
                }
            }
            else
            {
                Console.WriteLine("Please enter EDF or RM & optionally EE in format inputfile.txt EDF EE");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// EDF scheduling 
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="energyEff"></param>
        static void EDF(List<string> tasks, string energyEff = "NO")
        {
            
            int numTask = 0;
            int location = 0;
            List<task> taskList = new List<task>();
            
            
            for (int i = 0; i < tasks.Count; i++)//Gives you the number of tasks
            {
                if (tasks[i].Equals("-"))
                {
                    numTask++;
                }
            }

            for (int i = 0; i < numTask; i++)//Adds specific tasks until end of list
            {
                while (!tasks[location].Equals("-"))
                {
                    task test = new task();
                    test.name = tasks[location];
                    test.periodDead = Int32.Parse(tasks[location + 1]);
                    test.wcet1188 = Int32.Parse(tasks[location + 2]);
                    test.wcet918 = Int32.Parse(tasks[location + 3]);
                    test.wcet648 = Int32.Parse(tasks[location + 4]);
                    test.wcet384 = Int32.Parse(tasks[location + 5]);
                    location = location + 6;
                    taskList.Add(test);
                }
                location++;
            }

            //Arranging the lists into earliest deadline first
            List<task> priorityList = taskList.OrderBy(o => o.periodDead).ToList();//https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
            Console.WriteLine("Your priority @ time 0 is");
            foreach (var item in priorityList)
            {
                Console.Write(item.name + " ");
            }
            Console.WriteLine();






            if (energyEff.Equals("NO"))//No energy efficient
            {

                Console.WriteLine("Terribly sorry I couldn't implement it");
            }
            else if(energyEff.Equals("EE"))
            {
                Console.WriteLine("Terribly sorry I couldn't implement it");
            }
            else
            {
                Console.WriteLine("Enter EE or leave it blank in your command line call");
            }

            return;
        }
        /// <summary>
        /// RM Scheduling
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="energyEff"></param>
        static void RM(List<string> tasks, List<string> firstLine, string energyEff = null)
        {
            
            int numTask = 0;
            int location = 0;
            List<task> taskList = new List<task>();

            for (int i = 0; i < tasks.Count; i++)//Gives you the number of tasks, redundant incase first line of txt file isn't there
            {
                if (tasks[i].Equals("-"))
                {
                    numTask++;
                }
            }

            for (int i = 0; i < numTask; i++)//Adds task to my list of task
            {
                while (!tasks[location].Equals("-"))
                {
                    task test = new task();
                    test.name = tasks[location];
                    test.periodDead = Int32.Parse(tasks[location + 1]);
                    test.wcet1188 = Int32.Parse(tasks[location + 2]);
                    test.wcet918 = Int32.Parse(tasks[location + 3]);
                    test.wcet648 = Int32.Parse(tasks[location + 4]);
                    test.wcet384 = Int32.Parse(tasks[location + 5]);
                    location = location + 6;
                    taskList.Add(test);
                }
                location++;
            }

            //check to see if scheduable
            //Get U, if more than 1 not scheduable



            //Arranging the lists into earliest period first. Order won't change since periods are cosntant
            List<task> priorityList = taskList.OrderBy(o => o.periodDead).ToList();///https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
            Console.WriteLine("Your priority is the following");
            foreach (var item in priorityList)
            {
                Console.Write(item.name + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            //Console.WriteLine(firstLine[1]);

            bool cpuIdle = false;
            int idleCounter = 0;
            int lastRan = 0;
            int currentRan = 0;
            //bool lastIdle = false;


            if (energyEff == null)//No energy efficient
            {
                for (int i = 0; i < priorityList.Count; i++)
                {
                    priorityList[i].notIdle = true;
                }
                
                for (int time = 1; time <= Int32.Parse(firstLine[1]); time++)
                {
                    //Run top priority task that can run
                    for (int i = 0; i < priorityList.Count; i++)
                    {
                        if(priorityList[i].notIdle == true)
                        {
                           //Console.WriteLine(time.ToString() +" " + priorityList[i].name);
                            priorityList[i].ranFor++;
                            cpuIdle = false;
                            currentRan = i;
                            //Console.WriteLine(priorityList[i].ranFor);
                            break;
                        }
                        else
                        {
                            cpuIdle = true;
                        }
                    }
                    //Display
                    if (cpuIdle)
                    {
                        //Console.WriteLine("IDLE");
                        idleCounter++;
                    }

                    if (currentRan != lastRan && cpuIdle != true)
                    {
                        Console.WriteLine(time - priorityList[lastRan].ranFor + " " + priorityList[lastRan].name + " " + priorityList[lastRan].ranFor);
                        lastRan = currentRan; 
                    }                   
                    
                    
                    //What can't run
                    for (int i = 0; i < priorityList.Count; i++)
                    {
                        if (priorityList[i].ranFor == priorityList[i].wcet1188)
                        {
                            priorityList[i].notIdle = false;
                            //Console.WriteLine(time + " " + priorityList[i].name + " " + priorityList[i].wcet1188);
                            
                        }
                    }
                    //What can run
                    for (int i = 0; i < priorityList.Count; i++)
                    {
                        if(time % priorityList[i].periodDead == 0)
                        {
                            priorityList[i].notIdle = true;
                            priorityList[i].ranFor = 0;
                        }
                        
                    }
                    
                }
                Console.WriteLine("Total cycles IDLE = " + idleCounter);
            }
            else if (energyEff.Equals("EE"))
            {
                Console.WriteLine("Terribly sorry I couldn't implement it");
            }
            else
            {
                Console.WriteLine("Enter EE or leave it blank in your command line call");
            }

            return;
        }





    }
    /// <summary>
    /// Info pertaining to a task.
    /// </summary>
    class task
    {

        public string name { get; set; }
        public int periodDead { get; set; }
        public int wcet1188 { get; set; }
        public int wcet918 { get; set; }
        public int wcet648 { get; set; }
        public int wcet384 { get; set; }
        public bool notIdle { get; set; }
        public int ranFor { get; set; }

    


    }
}
