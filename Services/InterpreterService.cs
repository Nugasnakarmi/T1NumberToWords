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
        // We will throw an error when the value is not within the above range

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
        int withoutDecimal = (int)(inputNumber - c);

        //Algorithm
        int temp = withoutDecimal;
        string stringifiedNumber = temp.ToString();
        int noOfDigits = stringifiedNumber.Length;
      
        //do
        //{
        //    placeValueCount++;
        //    temp /= 10;
        //}
        //while (temp > 0);

        StringBuilder numberInterpretation = new StringBuilder();
        NumberHelper numberHelper = new NumberHelper();

        
        UpdateNumberInterpretation(numberInterpretation,  numberHelper,  stringifiedNumber, noOfDigits);


        numberInterpretation.Append("dollars and ");
        string afterDecimal = ((int)(c * 100)).ToString();
        // Interpret after decimal point

        UpdateNumberInterpretation(numberInterpretation, numberHelper, afterDecimal, 2);

       
        numberInterpretation.Append("cents");
        return numberInterpretation.ToString().ToUpper();
    }

    public StringBuilder UpdateNumberInterpretation( StringBuilder numberInterpretation, NumberHelper numberHelper, string stringifiedNumber, int noOfDigits)
    {
      
        for (int i = 0; i < noOfDigits; i++)
        {
            //var divider = numberHelper.GetPowerValue(10, i - 1);
            //int currentNumberInPosition = temp / divider;
            int currentNumberInPosition;
            int.TryParse(stringifiedNumber[i].ToString(), out currentNumberInPosition);
            // Don't try to use dictionary if the current number is 0
            if(currentNumberInPosition > 0) {
                int j = noOfDigits - i;
                //Console.WriteLine($"temp: {temp} pVC: {noOfDigits} cure: {currentNumberInPosition}, place: {i}, divided by: {numberHelper.GetPowerValue(10, i - 1)}");
                switch (j)
                {

                    case 1:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        break;
                    case 2:
                        numberInterpretation.Append(numberHelper.numberTensDictionary[currentNumberInPosition]);
                        break;
                    case 3:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append(" Hundred");
                        break;
                }
                numberInterpretation.Append(" ");
            }
           
            //temp = temp - currentNumberInPosition * divider;
        }

        return numberInterpretation;
    }
}