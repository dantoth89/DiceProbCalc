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
        int[] arr = { 6, 5, 1, 0, 0, 0, 1, 0};

        double[] final =  _calculator.ToHit(arr);

        return Ok("The final is: " + final);
    }
}