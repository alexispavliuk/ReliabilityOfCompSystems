using System;

namespace ReliabilityOfCompSystems
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = new int[] 
            { 
                166, 1975, 995, 1897, 1253, 840, 181, 1024, 
                383, 967, 260, 188, 1420, 53, 2893, 67, 787, 
                784, 667, 1572, 1480, 589, 607, 270, 618,
                396, 518, 1307, 204, 160, 1461, 1850, 952,
                5767, 3518, 56, 829, 168, 664, 695, 2237, 
                298, 1154, 542, 403, 1541, 76, 950, 847, 
                1054, 72, 1296, 886, 486, 26, 2098, 463, 
                1815, 1416, 1743, 218, 344, 1269, 288,
                1137, 175, 39, 293, 618, 794, 2142, 40, 915,
                676, 1640, 166, 538, 875, 215, 211, 2959,
                961, 140, 28, 649, 457, 1935, 727, 1545, 76, 
                46, 2007, 1881, 689, 398, 340, 8, 2959, 461,
                328
            };

            double percentage = 0.63f; 
            int hoursCountForProbability = 3168; // hours without breakdown
            int hoursCountForIntencity = 5210;
            int k = 10;

            Lab1 lab1 = new Lab1(k, values);

            var tValue = lab1.GetTValue(percentage);
            var probability = lab1.GetProbabilityOfWorkWithoutBreakdown(hoursCountForProbability);
            var intensity = lab1.GetIntensityOfBreakdowns(hoursCountForIntencity);

            Console.WriteLine($"Mean value of time for breakdown: {tValue:0.##}");
            Console.WriteLine($"Probability of work without breakdown for {hoursCountForProbability} hours: {probability:0.####}");
            Console.WriteLine($"Intensity of breakdowns for {hoursCountForIntencity} hours of work: {intensity:0.####}");
        }
    }
}
