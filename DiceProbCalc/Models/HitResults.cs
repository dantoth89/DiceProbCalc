namespace DiceProbCalc.Models;

public class HitResults
{
    public HitResults(double numberOfHits, double autoWounds, double mortalWounds)
    {
        this.numberOfHits = numberOfHits;
        this.autoWounds = autoWounds;
        this.mortalWounds = mortalWounds;
    }
    
    public HitResults(double[] hitResults)
    {
        numberOfHits = hitResults[0];
        autoWounds = hitResults[1];
        mortalWounds = hitResults[2];
    }
    
    public double numberOfHits;
    public double autoWounds;
    public double mortalWounds;
}