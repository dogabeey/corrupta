using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public static class ListExtensions
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this List<T> list, int? seed = null)
    {
        int n = list.Count; 
        Random rng = seed.HasValue ? new Random(seed.Value) : new Random();

        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    public static T GetRandomElement<T>(this List<T> list, int? seed = null)
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("The list is empty.");
        }

        Random rng = seed.HasValue ? new Random(seed.Value) : new Random();

        int randomIndex = rng.Next(list.Count);
        return list[randomIndex];
    }
    public static T GetWeightedRandomElement<T>(this List<T> list, List<int> weights,  int? seed = null)
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("The list is empty.");
        }
        if (list.Count != weights.Count)
        {
            throw new InvalidOperationException("Size of list must be equal to amount of weights.");
        }

        List<T> weightedList = new List<T>();
        for (int i = 0; i < weights.Count; i++)
        {
            for (int j = 0; j < weights[i]; j++)
            {
                weightedList.Add(list[i]);
            }
        }

        Random rng = seed.HasValue ? new Random(seed.Value) : new Random();

        int randomIndex = rng.Next(list.Count);
        return weightedList[randomIndex];
    }

    public static async Task<T> FindAsync<T>(this List<T> list, Predicate<T> match)
    {

        return await Task.Run(() => list.Find(match));
    }

    public static List<T> GetRandomElements<T>(this List<T> list, int count, int? seed = null)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Count must be greater than zero.");
        }

        int listCount = list.Count;
        if (count >= listCount)
        {
            // If count is greater than or equal to the list count, return a copy of the entire list.
            return new List<T>(list);
        }

        // Initialize the random number generator with the provided seed or use a random seed if none is provided.
        Random rng = seed.HasValue ? new Random(seed.Value) : new Random();

        List<T> randomElements = new List<T>(count);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = rng.Next(listCount);

            // Add the random element to the result list and remove it from the original list to avoid duplicates.
            randomElements.Add(list[randomIndex]);
            list.RemoveAt(randomIndex);
            listCount--;
        }

        return randomElements;
    }
}
