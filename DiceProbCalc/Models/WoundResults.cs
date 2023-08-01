namespace DiceProbCalc.Models;

public class WoundResults
{
    public WoundResults(double saveRolls, double penetration, double mortalWounds, double damage)
    {
        this.saveRolls = saveRolls;
        this.penetration = penetration;
        this.mortalWounds = mortalWounds;
        this.damage = damage;
    }

    public WoundResults(double[] finalWoundData)
    {
        saveRolls = finalWoundData[0];
        penetration = finalWoundData[1];
        mortalWounds = finalWoundData[2];
        damage = finalWoundData[3];
    }
    
    public double saveRolls;
    public double penetration;
    public double mortalWounds;
    public double damage;
}