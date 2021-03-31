using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsFigureSkating
{
    class SkatersDemo
    {
        static void Main(string[] args)
        {
            List<Skaters> skatersArray = new List<Skaters>();

            try
            {
                var path = @"..\..\Pairs.txt";
                StreamReader sr = new StreamReader(path);

                // read the first line
                //string line = sr.ReadLine();
				string line;

                while (line = sr.ReadLine() != null)
                {
                    // create variable of type Skaters and store data from reading
                    var skater = new Skaters();
                    skater.player1 = line;
                    skater.player2 = sr.ReadLine();
                    skater.country = sr.ReadLine();

                    // read a line of scores as a string and split into string array
                    var scores = sr.ReadLine();
                    List<string> list = new List<string>(scores.Split(' '));

                    // convert array from string to type double
                    foreach (var num in list)
                    {
                        skater.scoreList1.Add(Convert.ToDouble(num));
                    }

                    // repeat for scoreList2
                    scores = sr.ReadLine();
                    list = new List<string>(scores.Split(' '));
                    foreach (var num in list)
                    {
                        skater.scoreList2.Add(Convert.ToDouble(num));
                    }

                    // call method to calculate finalScore
                    skater.finalScore = skater.CalFinalScore();

                    // store object value into skatersArray
                    skatersArray.Add(skater);

                    // for checking while condition
                   // line = sr.ReadLine();

                }


                // 1st solution: sort the array based on final score in descending order
                List<Skaters> skatersArraySort = new Skaters[skatersArray.Count].ToList();
                for (int m = 0; m < skatersArray.Count; m++)
                {
                    int count = 0;
                    var tempSkaters = new List<Skaters>();
                    for (int n = 0; n < skatersArray.Count; n++)
                    {
                        if (skatersArray[m].finalScore < skatersArray[n].finalScore)
                        {
                            count++;
                        }

                        if ((Math.Abs(skatersArray[m].finalScore - skatersArray[n].finalScore) <= 0.00001) && (m != n))
                        {
                            tempSkaters = new List<Skaters>();
                            tempSkaters.Add(skatersArray[n]);
                        }
                    }
                    skatersArraySort[count] = skatersArray[m];

                    for (int i = 0; i < tempSkaters.Count; i++)
                    {
                        skatersArraySort[count + 1 + i] = tempSkaters[i];
                    }
                }

                // 2nd solution:
                // implement IComparable interface to invoke Sort() method; default is ascending, so Sort() and then Reverse()
                //skatersArray.Sort();
                //skatersArray.Reverse();

                // display results in descending order 
                Console.WriteLine("{0,-25}{1,-10}\t\t{2}", "Players", "Country", "Final Score");
                Console.WriteLine("===========================================================");
                //foreach (var skaters in skatersArray)   //results by IComparable interface method
                foreach (var skaters in skatersArraySort)
                {
                    DisplayPlayer(skaters);
                }


                // method to select top players after the existing upper level medals 
                List<Skaters> SelectTopPlayers(int fromIndex)
                {
                    List<Skaters> topPlayers = new List<Skaters>();
                    if (fromIndex < 3)
                    {
                        for (int i = fromIndex; i < skatersArraySort.Count; i++)
                        {

                            if (Math.Abs(skatersArraySort[fromIndex].finalScore - skatersArraySort[i].finalScore) <= 0.00001)
                            {
                                topPlayers.Add(skatersArraySort[i]);
                            }
                        }
                    }
                    return topPlayers;
                }

                // call method to distribute medals of gold, silver, bronze to selected players
                //  List<string> medalList = new List<string>();
                int totalMedal = 0;
                List<Skaters> goldWinners = new List<Skaters>(SelectTopPlayers(totalMedal));
                totalMedal += goldWinners.Count;

                List<Skaters> silverWinners = new List<Skaters>(SelectTopPlayers(totalMedal));
                totalMedal += silverWinners.Count;

                List<Skaters> bronzeWinners = new List<Skaters>(SelectTopPlayers(totalMedal));


                // display winners
                Console.WriteLine("\nGold Medal Winners: ");
                foreach (var skaters in goldWinners)
                {
                    DisplayPlayer(skaters);
                }
                Console.WriteLine("Silver Medal Winners: ");
                foreach (var skaters in silverWinners)
                {
                    DisplayPlayer(skaters);
                }
                Console.WriteLine("Bronze Medal Winners: ");
                foreach (var skaters in bronzeWinners)
                {
                    DisplayPlayer(skaters);
                }

                // close the file
                sr.Close();
                Console.ReadLine();
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Excecuting finally block.");
            }



            // display skaters
            void DisplayPlayer(Skaters skater)
            {
                Console.WriteLine("{0,-25}{1,-10}\t\t{2}", skater.player1 + ", " + skater.player2, skater.country, skater.finalScore);
            }


        }
    }
}
