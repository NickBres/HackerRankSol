using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'countSort' function below.
     *
     * The function accepts 2D_STRING_ARRAY arr as parameter.
     */

    public static void countSort(List<List<string>> arr)
    {
        int maxValue = arr.Max(sublist => int.Parse(sublist[0]));
        int minValue = arr.Min(sublist => int.Parse(sublist[0]));
        
        int range = maxValue - minValue + 1;
        
        List<string>[] countArr = new List<string>[range];
        for (int i = 0; i < range; i++)
        {
            countArr[i] = new List<string>();
        }

        for (int i = 0; i < arr.Count; i++)
        {
            var sublist = arr[i];
            int index = int.Parse(sublist[0]) - minValue;

            if (i < arr.Count / 2)
            {
                countArr[index].Add("-");
            }
            else
            {
                countArr[index].Add(sublist[1]);
            }
        }
        
        List<string> result = new List<string>();

        // Iterate through each bucket in countArr
        foreach (var bucket in countArr)
        {
            result.AddRange(bucket); // Add all strings in the bucket to the result
        }

        // Print the result as a space-separated string
        Console.WriteLine(string.Join(" ", result));
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<string>> arr = new List<List<string>>();

        for (int i = 0; i < n; i++)
        {
            arr.Add(Console.ReadLine().TrimEnd().Split(' ').ToList());
        }

        Result.countSort(arr);
    }
}