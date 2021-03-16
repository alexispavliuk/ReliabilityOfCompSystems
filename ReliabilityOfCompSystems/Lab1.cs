using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ReliabilityOfCompSystems
{
    public class Lab1
    {
        public Lab1(int k, int[] data)
        {
            K = k;
            Data = data;
            Data = Data.OrderBy(x => x).ToArray();

            var maxValue = data.Max();
            IntervalWidth = (double)maxValue / k;

            Intervals = new double[K+1];

            for (int i = 0; i < Intervals.Length; i++)
                Intervals[i] = i * IntervalWidth;
        }

        public int K { get; }

        public int[] Data { get; }

        public double IntervalWidth { get; }

        public int N { get => Data.Count(); }

        public double[] Intervals { get; }


        private List<int>[] DistributeDataByIntervals()
        {
            List<int>[] result = new List<int>[K];

            foreach (var number in Data)
            {
                for (int i = 0; i < K; i++)
                {
                    if (result[i] == null)
                        result[i] = new List<int>();

                    if (number >= Intervals[i] && number <= Intervals[i + 1])
                        result[i].Add(number);
                }
            }

            return result;
        }

        private double[] CalculateF()
        {
            var dataByIntervals = DistributeDataByIntervals();

            var result = new double[K];

            for (int i = 0; i < K; i++)
                result[i] = dataByIntervals[i].Count / (N * IntervalWidth);

            return result;
        }

        private List<double> CalculateP()
        {
            var fValues = CalculateF();
            var result = new List<double>();

            for (int i = 0; i < K; i++)
            {
                double temp = 0f;
                for (var j = 0; j < i + 1; j++)
                    temp += fValues[j] * IntervalWidth;

                result.Add(1 - temp);
            }

            return result;
        }

        private int GetInterval(double number)
        {
            for (int i = 0; i < K; i++)
            {
                if (number >= Intervals[i] && number <= Intervals[i+1])
                    return i;
            }
            return -1;
        }

        public double GetTValue(double percentage)
        {
            var pValues = CalculateP();
            pValues.Insert(0, 1);

            for (int i = 0; i < pValues.Count; i++)
            {
                if (pValues[i] > percentage)
                {
                    pValues.Insert(i + 1, percentage);
                    break;
                }
            }

            var index = pValues.IndexOf(percentage);
            var d = (pValues[index + 1] - percentage) / (pValues[index + 1] - pValues[index - 1]);
            return Intervals[index] - IntervalWidth * d;
        }

        public double GetProbabilityOfWorkWithoutBreakdown(int hoursCount)
        {
            var fValues = CalculateF();
            var intervalNumber = GetInterval(hoursCount);
            double temp = 0f;

            for (int i = 0; i < intervalNumber + 1; i++)
            {
                if (i != intervalNumber)
                    temp += fValues[i] * IntervalWidth;
                else
                    temp += fValues[i] * (hoursCount - Intervals[i]);
            }

            return 1 - temp;
        }

        public double GetIntensityOfBreakdowns(int hoursCount)
        {
            var fValues = CalculateF();
            var intervalNumber = GetInterval(hoursCount);
            var probability = GetProbabilityOfWorkWithoutBreakdown(hoursCount);

            return fValues[intervalNumber] / probability;
        }
    }
}
