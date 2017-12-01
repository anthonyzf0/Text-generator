using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionTweets.Source
{
    class FileLoader
    {
        private static String[] names = new string[] { "donald", "finn", "getty", "gwen", "indepen", "lincon", "moon", "raven", "romeo"};

        public static string loadFile()
        {
            for (int i = 0; i < names.Length; i++)
                Console.WriteLine("[" + i + "] " + names[i]);

            int index = Convert.ToInt32(Console.ReadLine());

            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\..\\..\\Files\\" + names[index]+".txt");
            String file = sr.ReadToEnd();
            sr.Close();

            return file;

        }
    }
}
