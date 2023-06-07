using DiceProbCalc.Services;
using Microsoft.AspNetCore.Mvc;
namespace DiceProbCalc;

[ApiController, Route("[controller]")]
public class DiceController : ControllerBase
{
    private readonly ICalculator _calculator;

    public DiceController(ICalculator calculator)
    {
        _calculator = calculator;
    }

    [HttpGet]
    public IActionResult Test()
    {
        return Ok("The final is: SZAR!!");
    }
}