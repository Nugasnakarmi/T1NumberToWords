# Intro

This web server routine for conversion of numbers to words is clearly a program that is algorithm heavy. 
The easiest approach would have been to use ASP.NET MVC such that the UI views could be generated in the server-side itself.

# My approach

My approach for this challenge was to develop the UI in a separate frontend project using the Angular framework, and the core logic in the backend WebAPI project in .NET 8. The reason for going this route is because I prefer a clear separation of concern between the UI and the server-side work. I know it's much simpler and takes less time through the MVC approach but I find it easier to separate tasks of the UI or cosmetic nature to the frontend project and the tasks of the data processing or domain/business logic etc to the API project.

A Web API is also reusable by mobile apps and IoT devices, and the API endoints can even be tested independently using Swagger's UI or through third-party tools like Postman.

Hence, my preference to this approach, although I know this is a limited project and it would be easier with MVC.

# Algorithm explanation

The algorithm I developed here first evaluates the number to the right of the decimal point by simply doing a modulo operation (%) with 1. Then the numbers to the left of the decimal is evaluated by subtracting the previous number with the original number.

Dictionaries for numbers 1 to 20 and a tens dictionary from 10 to 90 have been built with keys of the number and value of the converted string. These dictionaries are used extensively in calculating the result string.

A StringBuilder is used to gradually append the resulting string, since strings are immutable in C#.
The algorithm uses the stringified number to left of the decimal first and then iterates over every digit of the number. 
Based on the place value of the digit ( For eg, 1000 , 1 is is in the Thousands place value ), the algorithm chooses different methods to gradually update the Stringbuilder.

After finishing the update for the numbers to the left of the decimal point, the algorithm then proceeds to the numbers to the right and updates the Stringbuilder accordingly.

Here, instead of opting to completely do mathematical calculations of the numbers using loops( originally I was going that route but thought that another way is better) I have converted them to strings as they can be iterated over and it's easier to understand what we are doing. This also saved me several lines of code than the mathematical looping method. The mathematical apporach consisted of determing the first number to evaluate by dividing a number like 438 by 10^2 to get 4.
In the next iteration we wold get the the remainder and use the number 38 ( 438 % 100 = 38) and so on. This was a very calculation heavy approach, and it was clear that the other approach was better.

I have given two options to convert to currency or words. For converting to just words, in the interest of simplicity for example a 0.2 is simply converted to 20 hundredths instead of 2 tenths. Similar to what we are doing for the currency, 0.2 being 20 cents and 0.02 being 2 cents.

# Assumptions

I have set the premise for the user to only enter numbers from [0 to 9999999999999999.99], which is from ZERO to NINE QUADRILLION NINE HUNDRED NINETY-NINE TRILLION NINE HUNDRED NINETY-NINE BILLION NINE HUNDRED NINETY-NINE MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE AND NINETY-NINE HUNDREDTHS.

The algorithm can easily increase the range by adding enum values and switch cases for Sextillion, Septillion, Octillion, Nonillion and Decillion etc. It is simple to add these as the number interpretations for place values of the Tens( Tens, TenThousands, TenMIllions, TenBillions ...) use the same logic, similarly for the Hundreds( HundredThousands, HundredMillions, HundredBillions, HunddredTrillions ... ).


# Technology

I chose .NET 8 over .NET 9 as it is currently the LTS version or Long-Term Support version. The C# version used is 12.
I used Angular 18 to build the frontend project ( Angular 19 is scheduled to be release 19th Nov, 2024 which is today.)

Visual Studio 2022 was used to develop the Web API.
Visual Studio Code was used to develop the Web App.

# Testing
Unit Tests have been developed using the xUnit framework (v2.9.2) to test the backend service of converting number to words. XUnit is widely used by .Net Developers and also there is much support and frequent updates.

The controller has been tested by the help of the Moq framework (v4.20.72), this is used to simulate the functionality of the backend service for testing the controller. The main reason it's used is it allows us to control the service behavior and isolate it from the controller, since we want to test the controller in an isolated environment where we want every other component to be working correctly.

Since this challenge is mainly focused on the algorithmic server side problem, tests have been created for the server side only.