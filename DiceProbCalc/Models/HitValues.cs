namespace DiceProbCalc.Models;

public class HitValues
{
    public HitValues(int numberOfAttacks, int targetRoll, int reRoll, int toReRoll, int onSixEvent, int hitMod)
    {
        this.numberOfAttacks = numberOfAttacks;
        this.targetRoll = targetRoll;
        this.reRoll = reRoll;
        this.toReRoll = toReRoll;
        this.onSixEvent = onSixEvent;
        this.hitMod = hitMod;
    }

    public int numberOfAttacks;
    public int targetRoll;
    public int reRoll;
    public int toReRoll;
    public int onSixEvent;
    public int hitMod;
}