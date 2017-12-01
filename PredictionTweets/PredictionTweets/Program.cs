using PredictionTweets.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PredictionTweets
{
    class Program
    {
        private static Dictionary<String, Connection> wordMap = new Dictionary<string, Connection>();
        
        private static void addConnection(String a, String b)
        {
            if (wordMap.ContainsKey(a))
                wordMap[a].addLink(b);

            else
                wordMap.Add(a, new Connection(b));
        }

        static void interpritText(String s)
        {
            String[] lines = s.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                String[] words = lines[i].Split();

                if (words.Length < 4) continue;

                for (int pos = 0; pos < words.Length - 1; pos++)
                {
                    
                    addConnection(words[pos], words[pos + 1]);

                    if (pos < words.Length - 2)
                    {
                        addConnection(words[pos] + " " + words[pos + 1], words[pos + 2]);
                        addConnection(words[pos],  words[pos + 1] + " " +words[pos + 2]);
                    }

                    if (pos < words.Length - 3)
                    {
                        addConnection(words[pos] + " " + words[pos + 1], words[pos + 2] + " " + words[pos + 3]);
                        addConnection(words[pos], words[pos + 1] + " " + words[pos + 2] + " " + words[pos + 3]);
                        addConnection(words[pos] + " " + words[pos + 1] + " " + words[pos + 2], words[pos + 3]);
                    }
                }

                int a = words.Length;

                addConnection(words[a - 3] + " " + words[a - 2] + " " + words[a - 1], "--");
                addConnection(words[a - 2] + " " + words[a - 1], "--");
                addConnection(words[a - 1], "--");

                addConnection("++", words[0]);
                addConnection("++", words[0] + " " + words[1]);
                addConnection("++", words[0] + " " + words[1] + " " + words[2]);
                
            }
        }
        
        static void Main(string[] args)
        {
            Random rand = new Random();

            //Loads a file for use
            String file = FileLoader.loadFile();
            Console.Clear();

            Console.WriteLine("Reading file....");
            interpritText(file);
            Console.WriteLine("Done!");

            Console.WriteLine("How many word per line do you want? (3 - 16)");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num < 3) num = 3;
            if (num > 16) num = 16;

            Console.WriteLine("______________\n");

            int outputs = 8;
            while (true)
            {
                String baseWord = "++";

                String output = "";
                int count = 0;

                for (int i = 0; i< num+4; i++)
                {
                    baseWord = wordMap[baseWord].getNext();

                    if (baseWord == "--") break;

                    output += " " + baseWord;
                    count++;
                    
                    if (!wordMap.ContainsKey(baseWord) || count > num + 4) break;
                }

                if (count > num - 2 && count < num + 2)
                {
                    Console.WriteLine(output);
                    outputs--;

                    if (outputs == 0) break;
                }
            }

            Console.WriteLine("______________\n");

            Console.WriteLine("\n\nPress enter to restart.");
            Console.ReadLine();

            var fileName = Assembly.GetExecutingAssembly().Location;
            System.Diagnostics.Process.Start(fileName);
            
        }
    }
}
