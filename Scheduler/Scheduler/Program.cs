using System;
using System.Collections.Generic;
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
                Console.WriteLine(parameters[i]);
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
            editedFiles.Add("-");
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
                }
                else if (parameters[1].Equals("EDF"))
                {
                    Console.WriteLine("You want EDF");
                    EDF(editedFiles);

                }
                else if (parameters[1].Equals("RM"))
                {
                    Console.WriteLine("You want RM");
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


        }

        /// <summary>
        /// EDF scheduling 
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="energyEff"></param>
        static void EDF(List<string> tasks, string energyEff = "NO")
        {
            int timeCount = 1;//Execution time counter.
            

            //Get priorities,
            //get periods,
            //Get runtimes,


            if(energyEff.Equals("NO"))//No energy efficient
            {

                Console.WriteLine("IMPLEMENT1");
            }
            else if(energyEff.Equals("EE"))
            {
                Console.WriteLine("IMPLEMENT");
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
        static void RM(List<string> tasks, string energyEff = null)
        {
            if (energyEff == null)//No energy efficient
            {


            }
            else if (energyEff.Equals("EE"))
            {
                Console.WriteLine("IMPLEMENT");
            }
            else
            {
                Console.WriteLine("Enter EE or leave it blank in your command line call");
            }

            return;
        }





    }
    class task
    {

        public string name { get; set; }
        public int periodDead { get; set; }
        public int wcet1188 { get; set; }
        public int wcet918 { get; set; }
        public int wcet648 { get; set; }
        public int wcet384 { get; set; }

        public task(List<string> thing)
        {
            name = thing[1];
            periodDead = Int32.Parse(thing[2]);
            wcet1188 = Int32.Parse(thing[3]);
            wcet918 = Int32.Parse(thing[4]);
            wcet648 = Int32.Parse(thing[5]);
            wcet384 = Int32.Parse(thing[6]);


        }


    }
}
