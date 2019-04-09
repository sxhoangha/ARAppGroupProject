using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Score
    {
        public static int ScoreUnit { get; set; } = 100;
        public int score;
        public int goals;
        public int elapsedTime;
        public IntVector2 size;
        public string date;

        public Score(int score, int goals, int elapsedTime, IntVector2 size)
        {
            this.score = score;
            this.goals = goals;
            this.elapsedTime = elapsedTime;
            this.size = size;
            this.date = DateTime.Now.Date.ToShortDateString();
        }

        public int AddScore(int elapsedTime)
        {
            int scoreVariable = (int)(2 * Math.Sqrt(size.x * size.z) - elapsedTime / 4);
            int additionalScore = ScoreUnit * ((scoreVariable > 1) ? scoreVariable : 1);
            score += additionalScore;
            this.elapsedTime = elapsedTime;
            return additionalScore;
        }

        public int SaveScore()
        {
            date = DateTime.Now.Date.ToShortDateString();

            // load scores
            List<Score> Scores = LoadScore();

            // add a new list into the list and sort
            Scores.Add(this);
            Scores.Sort(delegate (Score s1, Score s2) { return s1.score.CompareTo(s2.score); });

            StringBuilder sb = new StringBuilder();

            // serialization
            for (int i = 0; i < Scores.Count; i++)
            {
                sb.Append(JsonUtility.ToJson(Scores.ElementAt(i)) + ';');   // split entries by ';'
            }

            // save the score on PlayerPrefs
            PlayerPrefs.SetString("Scores", sb.ToString());
            PlayerPrefs.Save();

            return score;
        }

        public List<Score> LoadScore()
        {
            // load saved score
            string strScores = PlayerPrefs.GetString("Scores");
            string[] strScoreArray = strScores.Split(";".ToCharArray());

            List<Score> scoreArray = new List<Score>();

            for (int i = 0; i < strScoreArray.Length - 1; i++)  // last item always ends with ';', so minus 1 from the length
            {
                // deserialization
                scoreArray.Add(JsonUtility.FromJson<Score>(strScoreArray[i]));
            }            

            return scoreArray;
        }

        public static void ResetScore()
        {
            // delete key in PlayerPrefs
            PlayerPrefs.DeleteKey("Scores");
            PlayerPrefs.Save();
            Debug.Log("Scores have been removed!");
        }

    }
}
