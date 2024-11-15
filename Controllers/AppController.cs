using Microsoft.AspNetCore.Mvc;
using T1NumberToWords.Interfaces;

namespace T1NumberToWords.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppController : ControllerBase
{   
    private readonly IInterpreterService _interpreterService;
    //By injecting dependency through constructor, we are inverting the control (IoC) to the AppController to choose the instance of IInterpreterService interface.
    public AppController( IInterpreterService interpreterService)  {
     _interpreterService = interpreterService;

    }

    [HttpGet("interpret")]
    public IActionResult Interpret(decimal inputNumber)
    {
        try
        {
            string result = _interpreterService.InterpretNumberToWords(inputNumber);
            return Ok(result);
        }
        catch (Exception ex) { 
            return BadRequest(ex.Message);
        }
 
    }
    
  
}
