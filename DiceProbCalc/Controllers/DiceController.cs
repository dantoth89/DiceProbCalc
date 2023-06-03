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
        double[] final =  _calculator.ToHit(12, 2, false, 1, false, 1, 1, "d6");

        return Ok("The final is: " + final);
    }
}