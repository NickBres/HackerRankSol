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
     * Complete the 'initialize' function below.
     *
     * The function accepts STRING s as parameter.
     */
    private static int[,] cft; // cumulative frequency table
    
    private static long[] factorial;
    private static long[] inverseFactorial;
    private const int MOD = 1000000007;

    public static void initialize(string s)
    {
        cft = new int[s.Length + 1, 26];
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                cft[i,j] = 0;
            }
        }

        for (int l = 1; l <= s.Length; l++) // Start from 1 for 1-based indexing
        {
            int letterIndex = s[l - 1] - 'a'; // Calculate the letter's index (0 to 25)

            for (int j = 0; j < 26; j++)
            {
                // Copy frequencies from the previous row
                cft[l, j] = cft[l - 1, j];
            }

            // Increment the frequency for the current letter
            cft[l, letterIndex]++;
        }
        
        int maxFactorial = s.Length + 1;
        factorial = new long[maxFactorial];
        inverseFactorial = new long[maxFactorial];

        // Compute factorials modulo MOD
        factorial[0] = 1;
        for (int i = 1; i < maxFactorial; i++)
        {
            factorial[i] = (factorial[i - 1] * i) % MOD;
        }

        // Compute modular inverses using Fermat's Little Theorem
        for (int i = 0; i < maxFactorial; i++)
        {
            inverseFactorial[i] = ModularInverse(factorial[i], MOD);
        }
    }
    
    private static long ModularInverse(long a, int mod)
    {
        return PowerMod(a, mod - 2, mod);
    }

    private static long PowerMod(long x, long y, int mod)
    {
        long result = 1;
        x %= mod;
        while (y > 0)
        {
            if ((y & 1) == 1)
                result = (result * x) % mod;
            y >>= 1;
            x = (x * x) % mod;
        }
        return result;
    }

    /*
     * Complete the 'answerQuery' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER l
     *  2. INTEGER r
     */

    public static int answerQuery(int l, int r)
    {
        int sumOfHalfCounts = 0;
        long mulOfFactOfHalfCounts = 1; // Use long to prevent overflow
        int numOfOdd = 0;

        // Iterate over all 26 characters ('a' to 'z')
        for (int i = 0; i < 26; i++)
        {
            // Frequency of character i in the substring [l:r]
            int freq = cft[r, i] - cft[l - 1, i];

            if (freq % 2 != 0)
            {
                numOfOdd++; // Count odd frequencies
            }

            sumOfHalfCounts += freq / 2; // Add half-counts for even parts

            if (freq / 2 > 0)
            {
                // Multiply factorial of half-count modulo MOD
                mulOfFactOfHalfCounts = (mulOfFactOfHalfCounts * factorial[freq / 2]) % MOD;
            }
        }

        // Numerator: Factorial of sum of half-counts
        long numerator = factorial[sumOfHalfCounts];

        // Denominator: Modular inverse of product of factorials of half-counts
        long denominator = ModularInverse(mulOfFactOfHalfCounts, MOD);

        // Compute result: (numerator / denominator) % MOD
        long result = (numerator * denominator) % MOD;

        // Multiply by numOfOdd if there are odd characters
        if (numOfOdd > 0)
        {
            result = (result * numOfOdd) % MOD;
        }

        return (int)result; // Cast result to int for final output
    }

    private static long FactorialMod(int n, int mod)
    {
        long result = 1;
        for (int i = 1; i <= n; i++)
        {
            result = (result * i) % mod; // Apply modulo at every step
        }
        return result;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        Result.initialize(s);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        for (int qItr = 0; qItr < q; qItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int l = Convert.ToInt32(firstMultipleInput[0]);

            int r = Convert.ToInt32(firstMultipleInput[1]);

            int result = Result.answerQuery(l, r);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}