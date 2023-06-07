namespace DiceProbCalc.Models;

public class SaveValues
{
    public SaveValues(int save, int saveMod, int cover, int reRoll, int toReRoll, int feelNoPain)
    {
        this.save = save;
        this.saveMod = saveMod;
        this.cover = cover;
        this.reRoll = reRoll;
        this.toReRoll = toReRoll;
        this.feelNoPain = feelNoPain;
    }
    public int save;
    public int saveMod;
    public int cover;
    public int reRoll;
    public int toReRoll;
    public int feelNoPain;
}