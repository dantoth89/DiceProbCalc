using DiceProbCalc.Models;
using DiceProbCalc.Services;

namespace DiceProbCalc;

public interface ICalculator
{
    public double[]ToHit(HitValues hitValues);
    public double[] ToWound(double[] hitsArr, WoundValues woundValues);
    public double Save(double[] woundsArr, SaveValues saveValues);
}