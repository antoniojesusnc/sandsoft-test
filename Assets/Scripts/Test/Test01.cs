using System;
using System.Collections.Generic;
using UnityEngine;

// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Test01
{
    private const char UP = '^';
    private const char DOWN = 'v';
    private const char RIGHT = '>';
    private const char LEFT = '<';

    public int Solution(string input)
    {
        Dictionary<char, int> inputsAmount = GetCharacterUsed(input);
        char directionMoreUsed = GetMoreUsed(inputsAmount);
        if (directionMoreUsed == '0')
            return 0;
        
        return input.Length - inputsAmount[directionMoreUsed];
    }

    private char GetMoreUsed(Dictionary<char, int> inputsAmount)
    {
        int upUsed = 0;
        int downUsed = 0;
        int rightUsed = 0;
        int leftUsed = 0;
        inputsAmount.TryGetValue(UP, out upUsed);
        inputsAmount.TryGetValue(DOWN, out downUsed);
        inputsAmount.TryGetValue(RIGHT, out rightUsed);
        inputsAmount.TryGetValue(LEFT, out leftUsed);
        
        if (upUsed >= downUsed
            && upUsed >= rightUsed
            && upUsed >= leftUsed)
            return UP;
        
        if (downUsed >= upUsed
            && downUsed >= rightUsed
            && downUsed >= leftUsed)
            return DOWN;
        
        if (rightUsed >= upUsed
            && rightUsed >= downUsed
            && rightUsed >= leftUsed)
            return RIGHT;
        
        if (leftUsed >= upUsed
            && leftUsed >= downUsed
            && leftUsed >= rightUsed)
            return LEFT;

        return '0';
    }

    private Dictionary<char, int> GetCharacterUsed(string input)
    {
        Dictionary<char, int> inputsAmount = new();
        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case UP: AddInput(inputsAmount, UP);
                    break;    
                case DOWN: AddInput(inputsAmount, DOWN);
                    break;    
                case RIGHT: AddInput(inputsAmount, RIGHT);
                    break;    
                case LEFT: AddInput(inputsAmount, LEFT);
                    break;    
            }
        }

        return inputsAmount;
    }
    
    private void AddInput(Dictionary<char, int> inputsAmount, char character)
    {
        if (inputsAmount.ContainsKey(character))
        {
            inputsAmount[character] += 1;
        }
        else
        {
            inputsAmount.Add(character,1);
        }
    }
}
