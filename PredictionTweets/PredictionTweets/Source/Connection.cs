using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionTweets.Source
{
    class Connection
    {
        private static Random rand = new Random();

        //Weight of this choice for chances
        private int weight;

        //Total options as output
        private int total;
        
        //Projected outputs
        private Dictionary<String, int> outputs = new Dictionary<string, int>();

        public Connection(String initial)
        {
            addLink(initial);
        }

        public void addLink(String word)
        {
            if (outputs.ContainsKey(word))
                outputs[word]++;
            else
                outputs.Add(word, 1);

            total++;
        }
        
        public String getNext()
        {
            int choice = rand.Next(total);
            
            foreach (String key in outputs.Keys)
            {
                choice -= outputs[key];
                if (choice < 0)
                    return key;
            }

            return "";

        }

    }
}
