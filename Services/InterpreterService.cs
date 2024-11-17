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

        if (inputNumber > 9999999.99M)
        {
            throw new ExceptionHelper("Number is too large, please choose a smaller number");
        }

        //First get numbers to the right of the decimal point For eg: .45
        decimal numberAfterDecimalPoint = inputNumber % 1;
        int withoutDecimal = (int)(inputNumber - numberAfterDecimalPoint);

        //Algorithm

        string stringifiedNumber = withoutDecimal.ToString();

        StringBuilder numberInterpretation = new StringBuilder();
        NumberHelper numberHelper = new NumberHelper();

        UpdateNumberInterpretation(numberInterpretation, numberHelper, stringifiedNumber, stringifiedNumber.Length);

        numberInterpretation.Append("dollars ");
        string afterDecimal = ((int)(numberAfterDecimalPoint * 100)).ToString();
        if (numberAfterDecimalPoint > 0)
        {
            numberInterpretation.Append("and ");
            // Interpret after decimal point
            UpdateNumberInterpretation(numberInterpretation, numberHelper, afterDecimal, afterDecimal.Length);
            numberInterpretation.Append("cents");
        }

        return numberInterpretation.ToString().ToUpper();
    }

    private void UpdateNumberInterpretation(StringBuilder numberInterpretation, NumberHelper numberHelper, string stringifiedNumber, int noOfDigits)
    {
        for (int i = 0; i < noOfDigits; i++)
        {
            int currentNumberInPosition;
            int.TryParse(stringifiedNumber[i].ToString(), out currentNumberInPosition);
            // Don't try to use dictionary if the current number is 0
            if (currentNumberInPosition > 0)
            {
                int j = noOfDigits - i;
                switch (j)
                {
                    case 1:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append(" ");
                        break;

                    case 2:
                        if (currentNumberInPosition == 1)
                        {
                            NumberInterpretationTwoDigits(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i);
                            numberInterpretation.Append(" ");
                            i++; //Skip next iteration
                        }
                        else
                        {
                            numberInterpretation.Append(numberHelper.numberTensDictionary[currentNumberInPosition]);
                            if (stringifiedNumber[i + 1] != '0')
                                numberInterpretation.Append("-");
                            else
                                numberInterpretation.Append(" ");
                        }

                        break;

                    case 3 or 6:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append(" Hundred ");
                        break;

                    case 5:
                        if (currentNumberInPosition == 1)
                        {
                            NumberInterpretationTwoDigits(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i);
                            numberInterpretation.Append(" Thousand ");
                            numberInterpretation.Append(" ");
                            i++; //Skip next iteration
                        }
                        else
                        {
                            numberInterpretation.Append(numberHelper.numberTensDictionary[currentNumberInPosition]);
                            if (stringifiedNumber[i + 1] != '0')
                                numberInterpretation.Append("-");
                        
                            else
                                numberInterpretation.Append(" Thousand ");
                        }

                        break;

                    case 4:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append(" Thousand ");
                        break;

                    case 7:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append(" Million ");
                        break;
                }
            }
        }
    }

    private void NumberInterpretationTwoDigits(int currentNumberInPosition, StringBuilder numberInterpretation, NumberHelper numberHelper, string stringifiedNumber, int index)
    {
        int secondDigit;
        int.TryParse(stringifiedNumber[index + 1].ToString(), out secondDigit);
        int twoDigitNumberToInterpret = currentNumberInPosition * 10 + secondDigit;
        numberInterpretation.Append(numberHelper.numberDictionary[twoDigitNumberToInterpret]);
    }
}