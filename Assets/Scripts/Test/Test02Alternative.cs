using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Test02Alternative
{

    private const char NON_VALID_VALUE = '0';
    public string Solution(string input)
    {
        var listInput = input.ToList();
        listInput.Sort();
        
        return FindNumber(listInput);
    }

    private string FindNumber(List<char> intInput)
    {
        char middleValue = NON_VALID_VALUE;
        List<char> palindromeNumbers = new List<char>();

        int index;
        for (index = intInput.Count - 1; index >= 1; index--)
        {
            if (middleValue == NON_VALID_VALUE)
            {
                if (intInput[index] != intInput[index - 1])
                {
                    middleValue = intInput[index];
                }
            }

            if (intInput[index] == intInput[index - 1])
            {
                if (intInput[index] != NON_VALID_VALUE)
                {
                    palindromeNumbers.Add(intInput[index]);
                }
                
                index--;
            }
        }
        
        if (IsNaturalExit(index) && middleValue == NON_VALID_VALUE)
        {
            middleValue = intInput[0];
        }

        return GenerateFinalNumber(palindromeNumbers.ToArray(), middleValue);
    }

    private bool IsNaturalExit(int index) => index == 0;

    private string GenerateFinalNumber(char[] palindromeNumbers, char middleValue)
    {
        if (palindromeNumbers.Length <= 0)
        {
            if (middleValue == 0)
            {
                return NON_VALID_VALUE.ToString();
            }
                
            return middleValue.ToString();
        }

        var finalNumber = new StringBuilder(palindromeNumbers.Length + middleValue != NON_VALID_VALUE?1:0);
        finalNumber.Append(palindromeNumbers);
        if (middleValue != NON_VALID_VALUE)
        {
            finalNumber.Append(middleValue);
        }
        Array.Reverse(palindromeNumbers);
        finalNumber.Append(palindromeNumbers);
        return finalNumber.ToString();
    }
}