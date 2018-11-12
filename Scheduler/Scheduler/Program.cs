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

        static void Main(string[] args)
        {
            //Variables
            List<string> parameters = new List<string>();//Holds the command line arguments
            List<string> textFile = new List<string>();//Hold the text file line by line
            List<string> edited = new List<string>();
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

            //seperate lines into a edited list

            Console.WriteLine("BEGIN EDITINGONS");
            string[] test12 = { " ", "  "};
            var test = textFile[1].Split(test12,System.StringSplitOptions.RemoveEmptyEntries);
            
            Console.WriteLine(test[0]);
            Console.WriteLine(test[1]);
            Console.WriteLine(test[2]);

            if (parameters.Count == 3)
            {
  
                //See if they want EDF or RM
                if (parameters[1].Equals("EDF") && parameters[2].Equals("EE"))
                {
                    Console.WriteLine("You want EDF W/ EE");
                }
                else if (parameters[1].Equals("RM") && parameters[2].Equals("EE"))
                {
                    Console.WriteLine("You want RM W/ EE");
                }
                else if (parameters[1].Equals("EDF"))
                {
                    Console.WriteLine("You want EDF");
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
    }
}
