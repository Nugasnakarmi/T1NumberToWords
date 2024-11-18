using T1NumberToWords.Enums;
using T1NumberToWords.Interfaces;
using T1NumberToWords.Services;

namespace T1NumberToWords.Tests;

public class InterpreterServiceTest
{
    private readonly IInterpreterService _interpreterService;

    private Dictionary<decimal, string> testDataCurrency = new Dictionary<decimal, string>() {
        {123.45M, "ONE HUNDRED TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS" },
        {.03M ,"THREE CENTS"},
        {3002000.03M, "THREE MILLION TWO THOUSAND DOLLARS AND THREE CENTS" },
        {1.2M, "ONE DOLLARS AND TWENTY CENTS" },
        {1200.33M, "ONE THOUSAND TWO HUNDRED DOLLARS AND THIRTY-THREE CENTS" },
        {10010.00M, "TEN THOUSAND TEN DOLLARS" },
        {31.56M, "THIRTY-ONE DOLLARS AND FIFTY-SIX CENTS" },
        {0M, "ZERO" },
        {9999999.99M, "NINE MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE DOLLARS AND NINETY-NINE CENTS" },
        {19999999.99M, "NINETEEN MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE DOLLARS AND NINETY-NINE CENTS" },
        {343434343.22M, "THREE HUNDRED FORTY-THREE MILLION FOUR HUNDRED THIRTY-FOUR THOUSAND THREE HUNDRED FORTY-THREE DOLLARS AND TWENTY-TWO CENTS" },
        {999999M, "NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE DOLLARS" },
        {909999M, "NINE HUNDRED NINE THOUSAND NINE HUNDRED NINETY-NINE DOLLARS" },
        {999999990M, "NINE HUNDRED NINETY-NINE MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY DOLLARS" },
        {120220000M, "ONE HUNDRED TWENTY MILLION TWO HUNDRED TWENTY THOUSAND DOLLARS" }
    };

    private Dictionary<decimal, string> testDataWords = new Dictionary<decimal, string>() {
        {123.45M, "ONE HUNDRED TWENTY-THREE AND FORTY-FIVE HUNDREDTHS" },
        {.03M ,"THREE HUNDREDTHS"},
        {3002000.03M, "THREE MILLION TWO THOUSAND AND THREE HUNDREDTHS" },
        {1.2M, "ONE AND TWENTY HUNDREDTHS" },
        {1200.33M, "ONE THOUSAND TWO HUNDRED AND THIRTY-THREE HUNDREDTHS" },
        {10010.00M, "TEN THOUSAND TEN" },
        {31.56M, "THIRTY-ONE AND FIFTY-SIX HUNDREDTHS" },
        {0M, "ZERO" },
        {9999999.99M, "NINE MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE AND NINETY-NINE HUNDREDTHS" },
        {19999999.99M, "NINETEEN MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE AND NINETY-NINE HUNDREDTHS" },
        {343434343.22M, "THREE HUNDRED FORTY-THREE MILLION FOUR HUNDRED THIRTY-FOUR THOUSAND THREE HUNDRED FORTY-THREE AND TWENTY-TWO HUNDREDTHS" },
        {999999M, "NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE" },
        {909999M, "NINE HUNDRED NINE THOUSAND NINE HUNDRED NINETY-NINE" },
        {999999990M, "NINE HUNDRED NINETY-NINE MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY" },
        {120220000M, "ONE HUNDRED TWENTY MILLION TWO HUNDRED TWENTY THOUSAND" }
    };

    public InterpreterServiceTest()
    {
        //DI - Injecting InterpreterService here by instantiating instance in the constructor
        _interpreterService = new InterpreterService();
    }

    [Fact]
    public void InterpretNumbersToCurrency()
    {
        foreach (KeyValuePair<decimal, string> entry in testDataCurrency)
        {
            //Arrange - the data
            decimal testNumber = entry.Key;
            string expectedString = entry.Value;
            //Act
            string interpretedString = _interpreterService.InterpretNumberToWords(testNumber, (int)ConversionMode.CURRENCY);
            //Assert - match result with expected to assert if the method works correctly!
            Assert.Equal(expectedString, interpretedString);
        }
    }

    [Fact]
    public void InterpretNumbersToWords()
    {
        foreach (KeyValuePair<decimal, string> entry in testDataWords)
        {
            //Arrange - the data
            decimal testNumber = entry.Key;
            string expectedString = entry.Value;
            //Act
            string interpretedString = _interpreterService.InterpretNumberToWords(testNumber, (int)ConversionMode.WORDS);
            //Assert - match result with expected to assert if the method works correctly!
            Assert.Equal(expectedString, interpretedString);
        }
    }
}