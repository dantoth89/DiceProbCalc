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
        this.numberOfHits = hitResults[0];
        this.autoWounds = hitResults[1];
        this.mortalWounds = hitResults[2];
    }
    
    public double numberOfHits;
    public double autoWounds;
    public double mortalWounds;
}