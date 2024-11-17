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
     * Complete the 'larrysArray' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts INTEGER_ARRAY A as parameter.
     */

    public static string larrysArray(List<int> A)
    {
        if (inversionsCount(A) % 2 == 0) return "YES";
        return "NO";
    }

    private static int inversionsCount(List<int> A)
    {
        List<int>temp = new List<int>(new int[A.Count]);
        return mergeSortAndCount(A, temp, 0 , A.Count -1);
    }

    private static int mergeSortAndCount(List<int> A, List<int> temp, int left, int right)
    {
        int mid, inversions = 0;
        if (left < right)
        {
            mid = (right + left) / 2;
            
            inversions += mergeSortAndCount(A, temp, left, mid);
            inversions += mergeSortAndCount(A, temp, mid + 1, right);

            inversions += mergeAndCount(A, temp, left, mid, right);
        }

        return inversions;

    }

    private static int mergeAndCount(List<int> A, List<int> temp, int left, int mid, int right)
    {
        int i = left;
        int j = mid + 1;
        int k = left;
        int inversions = 0;

        while (i <= mid && j <= right)
        {
            if (A[i] <= A[j])
            {
                temp[k++] = A[i++];
            }
            else
            {
                temp[k++] = A[j++];
                inversions += (mid + 1 - i);
            }
        }
        while (i <= mid)
        {
            temp[k++] = A[i++];
        }
        while (j <= right)
        {
            temp[k++] = A[j++];
        }
        for (i = left; i <= right; i++)
        {
            A[i] = temp[i];
        }

        return inversions;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> A = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList();

            string result = Result.larrysArray(A);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}