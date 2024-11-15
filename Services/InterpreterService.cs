﻿using System.Text;
using T1NumberToWords.Helper;
using T1NumberToWords.Interfaces;

namespace T1NumberToWords.Services;

public class InterpreterService : IInterpreterService
{
    public InterpreterService() { }

    public string InterpretNumberToWords(decimal inputNumber)
    {
        // Assuming the range of input numbers to be from 0.00 to 9,999,999.99 .
        // I'm also setting a premise that the numbers are to be interpreted as currency, as it was shown in the example.
        // We will return error when the value is not within the above range
        
        if(inputNumber < 0)
        {
            throw new ExceptionHelper("Cannot convert negative values.");
        }

        if(inputNumber > 9999999)
        {
            throw new ExceptionHelper("Number is too large, please choose a smaller number");
        }


        return "";
    }
}
