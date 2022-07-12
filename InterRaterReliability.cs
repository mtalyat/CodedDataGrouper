using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedDataGrouper
{
    /// <summary>
    /// This class is responsible for calculating interrater reliabilities, given the data within an EventList that it is handed.
    /// </summary>
    internal static class InterRaterReliability
    {
        private class Pair
        {
            public int LeftID { get; private set; }

            public int RightID { get; private set; }

            public Pair(int left, int right)
            {
                LeftID = left;
                RightID = right;
            }

            public override bool Equals(object? obj)
            {
                return obj != null && obj is Pair pair && LeftID == pair.LeftID && RightID == pair.RightID;
            }

            public override int GetHashCode()
            {
                return LeftID ^ RightID;
            }
        }

        public delegate double IRR(EventList list, params int[] ids);

        /// <summary>
        /// Calculates Krippendorf's Alpha, given the data within EventList.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double KrippendorfsAlpha(EventList list, params int[] ids)
        {
            //          STEP 1: Create a reliablity data matrix

            int length = list.GetEventCount();

            Dictionary<int, int> localizedGroupIDs = new Dictionary<int, int>();

            //the reliability matrix
            Dictionary<int, int[]> reliabilityMatrixDictionary = new Dictionary<int, int[]>();

            //the number of codes within each column
            int[] mu = new int[length];
            int totalMu = 0;

            int[]? d;

            int column = 0;

            //go through each event
            foreach (EventList.Event e in list.GetEvents())
            {
                //check the observers for each one, and log what event they have
                //add 1 to the behavior ID, as 0 means empty
                foreach (RowData rd in e.RowDatas)
                {
                    int observerID = rd.IDID;

                    //only add ID if it is one we are searching for
                    if (ids.Any() && !ids.Contains(observerID))
                    {
                        continue;
                    }

                    //get the array for the observer
                    if (!reliabilityMatrixDictionary.TryGetValue(observerID, out d))
                    {
                        //observer not already in the matrix, add them
                        d = new int[length];
                        reliabilityMatrixDictionary.Add(observerID, d);
                    }

                    //get the localized group id
                    int localGroupID;
                    if (!localizedGroupIDs.TryGetValue(rd.GroupID, out localGroupID))
                    {
                        localGroupID = localizedGroupIDs.Count + 1;
                        localizedGroupIDs.Add(rd.GroupID, localGroupID);
                    }

                    //observer already in the matrix, add to their data
                    d[column] = localGroupID;

                    //add to column count and total count
                    mu[column]++;
                    totalMu++;
                }

                column++;
            }

            //convert to 2D matrix
            int observerCount = reliabilityMatrixDictionary.Count;

            int[][] reliabilityMatrix = new int[observerCount][];
            for (int i = 0; i < observerCount; i++)
            {
                reliabilityMatrix[i] = reliabilityMatrixDictionary.ElementAt(i).Value;
            }

            //DEBUG
            //MainForm.Instance?.Logger.Log($"Reliability Matrix:\n{string.Join("\n", reliabilityMatrix.Select(r => $"[{string.Join(", ", r)}]"))}");

            //          STEP 2: Create coincidences within units matrix

            int coincidencesSize = localizedGroupIDs.Count;

            double[][] coincidencesMatrix = new double[coincidencesSize][];
            for (int i = 0; i < coincidencesSize; i++)
                coincidencesMatrix[i] = new double[coincidencesSize];
            double[] coincidencesRowColTotals = new double[coincidencesSize];
            double coincidencesTotal = 0;

            Dictionary<Pair, int> pairs = new Dictionary<Pair, int>();

            //go through the reliability data matrix and calculate each coincidence
            for (int i = 0; i < length; i++)
            {
                //get the number of pairs
                int count = mu[i] - 1;
                //int pairCount = mu[i] * count;

                if (count <= 0)
                {
                    //ignore events with 1 code in it
                    continue;
                }

                //calculate the pairs
                for (int j = 0; j < observerCount - 1; j++)
                {
                    int left = reliabilityMatrix[j][i] - 1;

                    if (left < 0)
                    {
                        continue;
                    }

                    for (int k = j + 1; k < observerCount; k++)
                    {
                        int right = reliabilityMatrix[k][i] - 1;

                        if (right < 0)
                        {
                            continue;
                        }

                        //subtracting 1 from left and right, as we added one earlier

                        //create a pair from left and right
                        Pair pair = new Pair(left, right);

                        //try to add it to the dictionary
                        if (!pairs.TryAdd(pair, 1))
                        {
                            //if did not add, it already exists
                            pairs[pair]++;
                        }

                        //also add reverse of this pair
                        pair = new Pair(right, left);

                        if (!pairs.TryAdd(pair, 1))
                        {
                            pairs[pair]++;
                        }
                    }
                }

                //all pairs calculated
                //insert pairs into coincidence matrix
                foreach (var kvp in pairs)
                {
                    double coincidence = (double)kvp.Value / count;

                    //MainForm.Instance?.Logger.Log($"matrix: {coincidencesMatrix[kvp.Key.LeftID, kvp.Key.RightID]}, rowColTotals: {coincidencesRowColTotals[kvp.Key.LeftID]}/{coincidencesRowColTotals[kvp.Key.RightID]}, total: {coincidencesTotal}, adding: {coincidence}");

                    coincidencesMatrix[kvp.Key.LeftID][kvp.Key.RightID] += coincidence;
                    coincidencesRowColTotals[kvp.Key.LeftID] += coincidence / 2.0;
                    coincidencesRowColTotals[kvp.Key.RightID] += coincidence / 2.0;
                    coincidencesTotal += coincidence;
                }

                //clear pairs for next iteration
                pairs.Clear();
            }

            //DEBUG
            //MainForm.Instance?.Logger.Log($"Coincidence Matrix:\n{string.Join("\n", coincidencesMatrix.Select(r => $"[{string.Join(", ", r)}]"))}");

            //          STEP 3: Skip

            //          STEP 4: Calculate alpha

            //get the matching pairs
            double[] matchingPairs = new double[coincidencesSize];
            for (int i = 0; i < coincidencesSize; i++)
            {
                matchingPairs[i] = coincidencesMatrix[i][i];
            }

            double rowColTotal = coincidencesRowColTotals.Sum(d => d * (d - 1));

            //DEBUG
            //MainForm.Instance?.Logger.Log($"K'sA: (({coincidencesTotal} - 1.0) * ({string.Join(" + ", matchingPairs)}) - ({string.Join(" + ", coincidencesRowColTotals.Select(d => $"{d} * ({d} - 1)"))})) / (({coincidencesTotal} * ({coincidencesTotal} - 1.0)) - ({string.Join(" + ", coincidencesRowColTotals.Select(d => $"{d} * ({d} - 1)"))})) = {((coincidencesTotal - 1.0) * (matchingPairs.Sum()) - (rowColTotal)) / ((coincidencesTotal * (coincidencesTotal - 1.0)) - rowColTotal)}");
            //MainForm.Instance?.Logger.Log($"K'sA: (({coincidencesTotal} - 1.0) * ({matchingPairs.Sum()}) - ({rowColTotal})) / (({coincidencesTotal} * ({coincidencesTotal} - 1.0)) - ({rowColTotal})) = {((coincidencesTotal - 1.0) * (matchingPairs.Sum()) - (rowColTotal)) / ((coincidencesTotal * (coincidencesTotal - 1.0)) - rowColTotal)}");

            double alpha = ((coincidencesTotal - 1.0) * (matchingPairs.Sum()) - (rowColTotal)) / ((coincidencesTotal * (coincidencesTotal - 1.0)) - (rowColTotal));

            //if alpha is Not a Number, return 0, which means no reliability
            return double.IsNaN(alpha) ? 0.0 : alpha;
        }

        public static double RaterPercentage(EventList list, params int[] ids)
        {
            int length = list.GetEventCount();

            List<int> idsList = new List<int>(ids);

            int observerCount = ids.Length;

            int[][] behaviors = new int[observerCount][];
            for (int i = 0; i < observerCount; i++)
                behaviors[i] = new int[length];

            int column = 0;

            //go through each event
            foreach (EventList.Event e in list.GetEvents())
            {
                //check the observers for each one, and log what event they have
                //add 1 to the behavior ID, as 0 means empty
                foreach (RowData rd in e.RowDatas)
                {
                    int observerID = rd.IDID;

                    //only add ID if it is one we are searching for
                    int idIndex = idsList.IndexOf(observerID);

                    if (idIndex < 0)
                    {
                        continue;
                    }

                    //add to behaviors
                    behaviors[idIndex][column] = rd.GroupID + 1;
                }

                column++;
            }

            //now that we have all of the behaviors and events for each rater, compare them all per event

            double[][] percents = new double[length][];
            for (int i = 0; i < length; i++)
                percents[i] = new double[observerCount];

            //get percent match for each observer, compared to the other observers
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < observerCount; j++)
                {
                    double percent = 0.0;

                    for (int k = 0; k < observerCount; k++)
                    {
                        if (j != k)
                        {
                            percent += (behaviors[j][i] == behaviors[k][i]) ? 1.0 : 0.0;
                        }
                    }

                    percent /= observerCount - 1;

                    percents[i][j] = percent;
                }
            }

            double totalPercent = 0.0;

            //get averages
            for (int i = 0; i < length; i++)
            {
                totalPercent += percents[i].Sum() / observerCount;
            }

            //get the average percent overal
            totalPercent /= length;

            return totalPercent;
        }

        public static double[] BehaviorPercentage(EventList list, params int[] ids)
        {
            throw new NotImplementedException();
        }

        public static string[] All(EventList list, IRR method, string prefix = "", string numberFormat = "0.00")
        {
            List<string> results = new List<string>();

            int observerCount = list.ObvserverCount;

            //print IRR between each of the observers
            if (observerCount > 2)
            {
                for (int i = 0; i < observerCount - 1; i++)
                {
                    for (int j = i + 1; j < observerCount; j++)
                    {
                        results.Add($"{prefix}... between {i + 1} and {j + 1}: {method(list, i, j).ToString(numberFormat)}");
                    }
                }
            }

            //print IRR between all of the observers
            results.Add($"{prefix}... between all: {method(list, Enumerable.Range(0, observerCount).ToArray()).ToString(numberFormat)}");

            return results.ToArray();
        }
    }
}
