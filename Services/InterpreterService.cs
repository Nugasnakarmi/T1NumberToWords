using System.Text;
using System.Text.RegularExpressions;
using T1NumberToWords.Enums;
using T1NumberToWords.Helper;
using T1NumberToWords.Interfaces;

namespace T1NumberToWords.Services;

public class InterpreterService : IInterpreterService
{
    public InterpreterService()
    { }

    public string InterpretNumberToWords(decimal inputNumber, int mode)
    {
        // Assuming the range of input numbers to be from 0.00 to 9,999,999.99 .
        // I'm also setting a premise that the numbers are to be interpreted as currency, as it was shown in the example.
        // We will throw an error when the value is not within the above range

        if (inputNumber < 0)
        {
            throw new ExceptionHelper("Cannot interpret negative values.");
        }

        if (inputNumber > 9999999999999999.99M)
        {
            throw new ExceptionHelper("Number is too large, please choose a smaller number");
        }

        //First get numbers to the right of the decimal point For eg: .45
        decimal numberAfterDecimalPoint = inputNumber % 1;
        long withoutDecimal = (long)(inputNumber - numberAfterDecimalPoint);

        //Algorithm

        string stringifiedNumber = withoutDecimal.ToString();

        StringBuilder numberInterpretation = new StringBuilder();
        NumberHelper numberHelper = new NumberHelper();

        if (withoutDecimal == 0 && numberAfterDecimalPoint == 0)
            return NumberDescription.ZERO.ToUpper();

        UpdateNumberInterpretation(numberInterpretation, numberHelper, stringifiedNumber, stringifiedNumber.Length);

        if (withoutDecimal > 0 && mode == (int)ConversionMode.CURRENCY)
            numberInterpretation.Append(" dollars");

        string afterDecimal = ((int)(numberAfterDecimalPoint * 100)).ToString();
        if (numberAfterDecimalPoint > 0)
        {
            if (withoutDecimal > 0)
                numberInterpretation.Append(" and ");
            // Interpret after decimal point
            UpdateNumberInterpretation(numberInterpretation, numberHelper, afterDecimal, afterDecimal.Length);
            if (mode == (int)ConversionMode.CURRENCY)
                numberInterpretation.Append(" cents");
            else if (afterDecimal.Length == 1)

                numberInterpretation.Append(" tenths");
            else if (afterDecimal.Length == 2)
                numberInterpretation.Append(" hundredths");
        }
        numberInterpretation.ToString().ToUpper();
        return Regex.Replace(numberInterpretation.ToString().ToUpper(), @"\s+", " ");
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
                    case (int)PlaceValue.Ones:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);                   
                        break;

                    case (int)PlaceValue.Tens:
                        i = UpdateNumberForTens(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i);
                        break;

                    case (int)PlaceValue.Hundreds:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append($" {NumberDescription.HUNDRED} ");
                        break;

                    case (int)PlaceValue.Thousands:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append($" {NumberDescription.THOUSAND} ");
                        break;

                    case (int)PlaceValue.TenThousands:
                        i = UpdateNumberForTens(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i, NumberDescription.THOUSAND);
                        break;

                    case (int)PlaceValue.HundredThousands:
                        UpdateNumberForHundreds(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i, NumberDescription.THOUSAND);
                        break;

                    case (int)PlaceValue.Millions:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append($" {NumberDescription.MILLION} ");
                        break;

                    case (int)PlaceValue.TenMillions:
                        i = UpdateNumberForTens(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i, NumberDescription.MILLION);
                        break;

                    case (int)PlaceValue.HundredMillions:
                        UpdateNumberForHundreds(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i, NumberDescription.MILLION);
                        break;

                    case (int)PlaceValue.Billions:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append($" {NumberDescription.BILLION} ");
                        break;

                    case (int)PlaceValue.TenBillions:
                        i = UpdateNumberForTens(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i, NumberDescription.BILLION);

                        break;

                    case (int)PlaceValue.HundredBillions:
                        UpdateNumberForHundreds(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i, NumberDescription.BILLION);
                        break;

                    case (int)PlaceValue.Trillions:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append($" {NumberDescription.TRILLION} ");
                        break;

                    case (int)PlaceValue.TenTrillions:
                        i = UpdateNumberForTens(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i, NumberDescription.TRILLION);

                        break;

                    case (int)PlaceValue.HundredTrillions:
                        UpdateNumberForHundreds(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, i, NumberDescription.TRILLION);
                        break;

                    case (int)PlaceValue.Quadrillions:
                        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
                        numberInterpretation.Append($" {NumberDescription.QUADRILLION} ");
                        break;
                }
            }
        }
    }

    private void UpdateNumberInterpretationTwoDigits(int currentNumberInPosition, StringBuilder numberInterpretation, NumberHelper numberHelper, string stringifiedNumber, int index)
    {
        int secondDigit;
        int.TryParse(stringifiedNumber[index + 1].ToString(), out secondDigit);
        int twoDigitNumberToInterpret = currentNumberInPosition * 10 + secondDigit;
        numberInterpretation.Append(numberHelper.numberDictionary[twoDigitNumberToInterpret]);
    }

    private int UpdateNumberForTens(int currentNumberInPosition, StringBuilder numberInterpretation, NumberHelper numberHelper, string stringifiedNumber, int index, string numberDescription = "")
    {
        string description = (numberDescription == "") ? " " : $" {numberDescription} ";

        if (currentNumberInPosition == 1)
        {
            UpdateNumberInterpretationTwoDigits(currentNumberInPosition, numberInterpretation, numberHelper, stringifiedNumber, index);
            numberInterpretation.Append(description);
            index++; //Skip next iteration
        }
        else
        {
            numberInterpretation.Append(numberHelper.numberTensDictionary[currentNumberInPosition]);
            if (stringifiedNumber[index + 1] != '0')
                numberInterpretation.Append("-");
            else
                numberInterpretation.Append(description);
        }
        return index;
    }

    private void UpdateNumberForHundreds(int currentNumberInPosition, StringBuilder numberInterpretation, NumberHelper numberHelper, string stringifiedNumber, int index, string numberDescription)
    {
        numberInterpretation.Append(numberHelper.numberDictionary[currentNumberInPosition]);
        numberInterpretation.Append($" {NumberDescription.HUNDRED} ");

        if ((stringifiedNumber[index + 1] == '0') && (stringifiedNumber[index + 2] == '0'))
            numberInterpretation.Append($"{numberDescription}");
    }
}