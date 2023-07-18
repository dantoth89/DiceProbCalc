using DiceProbCalc.Models;
using DiceProbCalc.Models.Enums;
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
        var HV = new HitValues(60, 4, 0, 0, HitOnSixEvent.NoEvent, 0);
        var WV = new WoundValues(4, 0, 0, WoundOnSixEvent.NoEvent, 0, 0, 1);
        var SV = new SaveValues(4, 0, 0, 0, 0, 0);

        var hits = _calculator.ToHit(HV);
        var wounds = _calculator.ToWound(hits, WV);
        var final = _calculator.Save(wounds, SV);

        return Ok("Not yet, final fix test " + final);
    }

    [HttpGet("/body")]
    public IActionResult TestBody([FromBody] AllValues allValues)
    {
        var HV = new HitValues(allValues.numberOfAttacks, allValues.targetRoll, allValues.hitReRoll,
            allValues.hitToReRoll, allValues.hitOnSixEvent, allValues.hitMod);
        var WV = new WoundValues(allValues.targetNum, allValues.woundReRoll, allValues.woundToReRoll,
            allValues.woundOnSixEvent, allValues.woundMod, allValues.penetration, allValues.damage);
        var SV = new SaveValues(allValues.save, allValues.saveMod, allValues.cover, allValues.saveReRoll,
            allValues.saveToReRoll, allValues.feelNoPain);

        var hits = _calculator.ToHit(HV);
        var wounds = _calculator.ToWound(hits, WV);
        var final = _calculator.Save(wounds, SV);

        return Ok("Not yet, frombody test " + final);
    }
}