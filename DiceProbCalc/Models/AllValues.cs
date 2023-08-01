using DiceProbCalc.Models.Enums;

namespace DiceProbCalc.Models;

public class AllValues
{
    public int numberOfAttacks { get; set; }
    public int targetRollToHit { get; set; }
    public int hitReRoll { get; set; }
    public int hitToReRoll { get; set; }
    public HitOnSixEvent hitOnSixEvent { get; set; }
    public int hitMod { get; set; }

    public int targetRollToWound { get; set; }
    public int woundReRoll { get; set; }
    public int woundToReRoll { get; set; }
    public WoundOnSixEvent woundOnSixEvent { get; set; }
    public int woundMod { get; set; }
    public int penetration { get; set; }
    public int damage { get; set; }

    public int save { get; set; }
    public int saveMod { get; set; }
    public int cover { get; set; }
    public int saveReRoll { get; set; }
    public int saveToReRoll { get; set; }
    public int feelNoPain { get; set; }
}