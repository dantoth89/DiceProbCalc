namespace DiceProbCalc;

public interface ICalculator
{
    public double[] ToHit(int numOfAtk, int targetNum, bool reroll, int toReroll, bool onSix, int onSixEvent, int hitMod, string dmg);
    public double[] ToWound();
    public double Save();
}