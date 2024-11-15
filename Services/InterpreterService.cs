using System.Text;
using T1NumberToWords.Helper;
using T1NumberToWords.Interfaces;

namespace T1NumberToWords.Services;

public class InterpreterService : IInterpreterService
{
    public InterpreterService()
    { }

    public string InterpretNumberToWords(decimal inputNumber)
    {
        // Assuming the range of input numbers to be from 0.00 to 9,999,999.99 .
        // I'm also setting a premise that the numbers are to be interpreted as currency, as it was shown in the example.
        // We will return error when the value is not within the above range

        if (inputNumber < 0)
        {
            throw new ExceptionHelper("Cannot interpret negative values.");
        }

        if (inputNumber > 9999999)
        {
            throw new ExceptionHelper("Number is too large, please choose a smaller number");
        }

        //First get numbers to the right of the decimal point For eg: .45
        decimal c = inputNumber % 1;
        var withoutDecimal = inputNumber - c;

        //Algorithm
        int temp = (int)withoutDecimal; //Remove trailing zeroes
        int placeValueCount = 0;
        do
        {
            placeValueCount++;
            temp /= 10;
        }
        while (temp > 0);

        StringBuilder numberInterpretation = new StringBuilder();
        NumberHelper numberHelper = new NumberHelper();

        temp = (int)withoutDecimal;
      
        for (int i = placeValueCount; i > 0; i--)
        {
            int currentNumberInPosition = temp / (numberHelper.GetPowerValue(10, i - 1));
            Console.WriteLine($"cure: {currentNumberInPosition}, place: {placeValueCount}");
            numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition].ToString());
            temp %= 10;
        }

        return numberInterpretation.ToString();
    }
}