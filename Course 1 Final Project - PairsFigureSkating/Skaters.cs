using System;
using System.Collections.Generic;

namespace PairsFigureSkating
{
    public class Skaters : IComparable
    {
        public string player1 { get; set; }
        public string player2 { get; set; }
        public string country { get; set; }
        public double finalScore { get; set; }

        public List<double> scoreList1 = new List<double>();
        public List<double> scoreList2 = new List<double>();

        public double CalFinalScore()
        {
            double sum = 0;

            for (int i = 0; i < scoreList1.Count; i++)
            {
                sum += scoreList1[i];

            }
            for (int i = 0; i < scoreList2.Count; i++)
            {
                sum += scoreList2[i];
            }
            finalScore = sum / scoreList2.Count;

            return finalScore;
        }


        // IComparable interface method for comparing object array
        public int CompareTo(object obj)
        {
            Skaters skater = obj as Skaters;
            if (!finalScore.Equals(skater.finalScore))
                return finalScore.CompareTo(skater.finalScore);
            else
                return player1.CompareTo(skater.player1);
        }

    }
}
