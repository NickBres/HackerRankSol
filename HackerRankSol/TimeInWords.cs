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
     * Complete the 'timeInWords' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts following parameters:
     *  1. INTEGER h
     *  2. INTEGER m
     */
     
    // Words for numbers 1-19
    static string[] numberWords = {
        "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        "ten", "eleven", "twelve", "thirteen", "fourteen", "quarter", "sixteen", "seventeen", "eighteen", "nineteen"
    };

    // Words for tens multiples (only multiples of 10)
    static string[] tensWords = {
        "", "", "twenty", "thirty", "forty", "fifty"
    };

    public static string timeInWords(int h, int m)
    {
        string hours, minutes;
        
        hours = numberToText(h);
        minutes = numberToText(m);

        switch (m)
        {
            case 0:
                return hours + " o' clock";
            case 1:
                return minutes + " minute past " + hours;
            case 15:
                return minutes + " past " + hours;
            case 30:
                return "half past " + hours;
            case 45:
                return "quarter to " + numberToText(h == 12 ? 1 : h + 1);
            default:
                if (m < 30) return minutes + " minutes past " + hours;
                return numberToText(60 - m) + " minutes to " + numberToText(h==12 ? 1:h+1);
        }
    }

    private static string numberToText(int n)
    {
        if (n < 20)
        {
            return numberWords[n];
        }
        else
        {
            return tensWords[n / 10] + (n % 10 != 0 ? " " + numberWords[n % 10] : "");
        }
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int h = Convert.ToInt32(Console.ReadLine().Trim());

        int m = Convert.ToInt32(Console.ReadLine().Trim());

        string result = Result.timeInWords(h, m);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}