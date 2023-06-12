namespace DiceProbCalc.Models;

public class WoundResults
{
    public WoundResults(double saveRolls, double penetration, double mortalWounds, double woundsWithIncPen,
        double amountOfIncPen, double damage)
    {
        this.saveRolls = saveRolls;
        this.penetration = penetration;
        this.mortalWounds = mortalWounds;
        this.woundsWithIncPen = woundsWithIncPen;
        this.amountOfIncPen = amountOfIncPen;
        this.damage = damage;
    }

    public WoundResults(double[] finalWoundData)
    {
        saveRolls = finalWoundData[0];
        penetration = finalWoundData[1];
        mortalWounds = finalWoundData[2];
        woundsWithIncPen = finalWoundData[3];
        amountOfIncPen = finalWoundData[4];
        damage = finalWoundData[5];
    }
    
    public double saveRolls;
    public double penetration;
    public double mortalWounds;
    public double woundsWithIncPen;
    public double amountOfIncPen;
    public double damage;
}