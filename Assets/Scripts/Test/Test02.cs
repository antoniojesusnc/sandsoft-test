using System;
using System.Collections.Generic;

// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Test02 {
    public string Solution(string input)
    {
        List<int> intInput = new List<int>();
        
        foreach (char numberCharacter in input)
        {
            intInput.Add((int)Char.GetNumericValue(numberCharacter));
        }
        
        intInput.Sort();

        return FindNumber(intInput).ToString();
    }

    private int FindNumber(List<int> intInput)
    {
        var middleValue = 0;
        List<int> number = new List<int>();
        for (int i = intInput.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                if (middleValue == 0)
                {
                    middleValue = intInput[i];
                }
                else
                {
                    continue;
                }
            }

            if (middleValue == 0)
            {
                if (intInput[i] != intInput[i - 1])
                {
                    middleValue = intInput[i];
                }
            }

            if (intInput[i] == intInput[i - 1])
            {
                if (intInput[i] != 0)
                {
                    number.Add(intInput[i]);
                }
                
                i--;
            }
        }

        return GenerateFinalNumber(number, middleValue);
    }

    private int GenerateFinalNumber(List<int> number, int middleValue)
    {
        if (number.Count <= 0)
        {
            if (middleValue == 0)
            {
                return 0;
            }
                
            return middleValue;
        }
        
        int finalNumber = number[0];
        for (int i = 1; i < number.Count; i++)
        {
            finalNumber *= 10;
            finalNumber += number[i];
        }
        finalNumber *= 10;
        finalNumber += middleValue;
        
        for (int i = number.Count - 1; i >= 0; i--)
        {
            finalNumber *= 10;
            finalNumber += number[i];
        }
        return finalNumber;
    }
}