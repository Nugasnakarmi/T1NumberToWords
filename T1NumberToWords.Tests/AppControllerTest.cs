
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using T1NumberToWords.Controllers;
using T1NumberToWords.Enums;
using T1NumberToWords.Helper;
using T1NumberToWords.Interfaces;
using T1NumberToWords.Models;

namespace T1NumberToWords.Tests;

public class AppControllerTest
{
    private readonly AppController _appController;
    private readonly Mock<IInterpreterService> _mockInterpreterService;
    private const string internalServerErrorNegative = "Cannot interpret negative values.";
     private const string internalServerErrorTooLarge = "Number is too large, please choose a smaller number";
    public AppControllerTest()
    {
        //DI - Injecting InterpreterService here by instantiating instance in the constructor
        _mockInterpreterService = new Mock<IInterpreterService>();
        _appController = new AppController(_mockInterpreterService.Object);
    }

    [Fact]
    public void GetReturnsOkResultTest()
    {
        var returnObject = new Result{ interpretedString = "ONE HUNDRED TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS" };
        //Arrange
        _mockInterpreterService.Setup(o => o.InterpretNumberToWords(123.45M, (int)ConversionMode.CURRENCY)).Returns(returnObject.interpretedString);

        // Act
        var result = _appController.Interpret(123.45M, (int)ConversionMode.CURRENCY);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Result>(okResult.Value);
        Assert.Equal(returnObject.interpretedString, returnValue.interpretedString);
    }

    [Fact]
    public void GetReturnsInternalServerError_WhenNegativeValue()
    {   //Arrange
        _mockInterpreterService.Setup(service => service.InterpretNumberToWords(-3M, (int)ConversionMode.CURRENCY)).Throws(new ExceptionHelper(internalServerErrorNegative));

        //Act
        var result = _appController.Interpret(-3M, (int)ConversionMode.CURRENCY);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(internalServerErrorNegative, statusCodeResult.Value);

    }

    [Fact]
    public void GetReturnsInternalServerError_WhenValueTooLarge()
    {   //Arrange
        _mockInterpreterService.Setup(service => service.InterpretNumberToWords(9999999999999999999999999M, (int)ConversionMode.CURRENCY)).Throws(new ExceptionHelper(internalServerErrorTooLarge));

        //Act
        var result = _appController.Interpret(9999999999999999999999999M,0);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(internalServerErrorTooLarge, statusCodeResult.Value);
    }
}
