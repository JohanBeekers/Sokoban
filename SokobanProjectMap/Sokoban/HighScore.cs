using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class HighScore
    {
        List<ModelScore> scores = new List<ModelScore>();
        String file;

        public List<ModelScore> getHighScore(String level)
        {
            file = "map/" + level + ".score";

            if(File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);

                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    String[] scoreInfo = line.Split(';');
                    scores.Add(new ModelScore(scoreInfo[0], Convert.ToInt32(scoreInfo[1]), Convert.ToInt32(scoreInfo[2])));
                }
                sr.Close();
            }
            return scores;
        }

        public void saveScore(ModelScore score, String level)
        {
            getHighScore(level);
            int insertAt = zoekInsertPositie(score);
            scores.Insert(insertAt, score);

            StreamWriter sw = new StreamWriter(file);
            foreach (ModelScore arrayScore in scores)
            {
                sw.WriteLine(arrayScore.PlayerName + ";" + arrayScore.Moves + ";" + arrayScore.Time);
            }

            sw.Close();
        }

        // vraag 4 punt 1:
        private int zoekInsertPositie(ModelScore score)
        {
            if (scores.Count == 0)
            {
                return 0;
            }

            int lowerbound = 0;
            int upperbound = scores.Count - 1;
            int middle;
            int compareResult;

            while (true)
            {
                middle = (upperbound + lowerbound) / 2;
                compareResult = score.compareTo(scores[middle]);

                if (compareResult == 0)
                {
                    return middle; //Hetzelfde element staat hier al! (rekening houdend dat dubbele waarden mogen voorkomen, kan hier de nieuwe waarde bij komen. 
                }
                else if (lowerbound > upperbound)
                {
                    return lowerbound; //Plaats gevonden voor het nieuwe element. 
                }
                else if (compareResult < 0)
                {
                    upperbound = middle - 1; //Element moet in de onderste helft toegevoegd worden.
                }
                else if (compareResult > 0)
                {
                    lowerbound = middle + 1; //Element moet in de bovenste helft toegevoegd worden.
                }
            }
        }
    }
}
