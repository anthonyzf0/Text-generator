using PredictionTweets.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            for(int i=0; i<lines.Length;i++)
            {
                String[] words = lines[i].Split();
                if (words.Length < 4) continue;

                for (int j = 0; j < words.Length - 3; j++)
                    addConnection(words[j] + " " + words[j+1], words[j + 2] + " " + words[j + 3]);
                for (int j = 0; j < words.Length - 1; j++) 
                    addConnection(words[j], words[j + 1]);

                addConnection(words[words.Length - 1], "\n");
                addConnection(words[words.Length - 2] + " " + words[words.Length - 1], "\n");
                addConnection(words[words.Length - 3] + " " + words[words.Length - 2], "\n");
            }
        }

        static void Main(string[] args)
        {   
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\..\\..\\Files\\moon.txt");
            String file = sr.ReadToEnd();
            interpritText(file);
            sr.Close();


            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                List<String> output = new List<string>();
                String word = wordMap.ElementAt(rand.Next(wordMap.Keys.Count())).Key;
                while (true)
                {
                    output.Add(word);

                    if (output.Count > 2)
                    {

                        bool two = rand.NextDouble() < 0.5;
                        if (two)
                        {
                            String key = output[output.Count-2]+" "+output[output.Count - 1];
                            if (wordMap.ContainsKey(key))
                                word = wordMap[key].getNext();
                            else
                                word = wordMap[output[output.Count - 1]].getNext();
                        }
                        else
                            word = wordMap[output[output.Count - 1]].getNext();
                    }
                    else
                        word = wordMap[output[output.Count - 1]].getNext();
                    
                    if (word == "\n")  break;
                }
                foreach (String a in output) Console.Write(a+" ");
                Console.WriteLine();
            }

        }
    }
}
