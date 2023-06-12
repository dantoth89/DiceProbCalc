using DiceProbCalc.Models;
using DiceProbCalc.Services;

namespace DiceProbCalc;

public interface ICalculator
{
    public HitResults ToHit(HitValues hitValues);
    public WoundResults ToWound(HitResults hitResults, WoundValues woundValues);
    public double Save(WoundResults woundResults, SaveValues saveValues);
}