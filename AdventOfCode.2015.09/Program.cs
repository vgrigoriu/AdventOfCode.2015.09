using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015._09
{
    class Program
    {
        static void Main(string[] args)
        {
            var distances = new int[8, 8];
            distances[0, 1] = 66;
            distances[0, 2] = 28;
            distances[0, 3] = 60;
            distances[0, 4] = 34;
            distances[0, 5] = 34;
            distances[0, 6] = 3;
            distances[0, 7] = 108;
            distances[1, 2] = 22;
            distances[1, 3] = 12;
            distances[1, 4] = 91;
            distances[1, 5] = 121;
            distances[1, 6] = 111;
            distances[1, 7] = 71;
            distances[2, 3] = 39;
            distances[2, 4] = 113;
            distances[2, 5] = 130;
            distances[2, 6] = 35;
            distances[2, 7] = 40;
            distances[3, 4] = 63;
            distances[3, 5] = 21;
            distances[3, 6] = 57;
            distances[3, 7] = 83;
            distances[4, 5] = 9;
            distances[4, 6] = 50;
            distances[4, 7] = 60;
            distances[5, 6] = 27;
            distances[5, 7] = 81;
            distances[6, 7] = 90;

            var paths = Permutations.Of(8);
            var total_distances = paths.Select(path => ComputeDistance.From(path, distances));

            var max = total_distances.Max();

            Console.WriteLine(max);
        }
    }

    static class ComputeDistance
    {
        public static int From(IEnumerable<int> order, int[,] distances)
        {
            var distance = order.Zip(order.Skip(1), distance_between).Sum();
            // Console.WriteLine($"Order: {string.Join(", ", order)}; d: {distance}");

            return distance;

            int distance_between(int a, int b)
            {
                if (a < b)
                {
                    return distances[a, b];
                }
                else
                {
                    return distances[b, a];
                }
            }
        }
    }

    static class Permutations
    {
        public static IEnumerable<IEnumerable<int>> Of(int n)
        {
            if (n <= 0)
            {
                yield return Enumerable.Empty<int>();
                yield break;
            }



            var all_permutations = Of(n - 1).SelectMany(perm => InsertEverywhere(perm, n - 1));

            foreach (var new_permutation in all_permutations)
            {
                yield return new_permutation;
            }
        }

        private static IEnumerable<IEnumerable<int>> InsertEverywhere(
            IEnumerable<int> initialPermutation, int newElement)
        {
            var length = initialPermutation.Count();
            foreach (var i in Enumerable.Range(0, length + 1).Reverse())
            {
                yield return Insert(initialPermutation, newElement, i);
            }
        }

        private static IEnumerable<int> Insert(
            IEnumerable<int> initialPermutation,
            int newElement,
            int position)
        {
            foreach (var x in initialPermutation.Take(position))
            {
                yield return x;
            }

            yield return newElement;

            foreach (var x in initialPermutation.Skip(position))
            {
                yield return x;
            }
        }
    }
}
