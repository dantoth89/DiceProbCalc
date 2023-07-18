using DiceProbCalc.Models.Enums;

namespace DiceProbCalc.Models;

public class AllValues
{
    public AllValues(int numberOfAttacks, int targetRoll, int hitReRoll, int hitToReRoll, HitOnSixEvent hitOnSixEvent, int hitMod, int targetNum, int woundReRoll, int woundToReRoll, WoundOnSixEvent woundOnSixEvent, int woundMod, int penetration, int damage, int save, int saveMod, int cover, int saveReRoll, int saveToReRoll, int feelNoPain)
    {
        this.numberOfAttacks = numberOfAttacks;
        this.targetRoll = targetRoll;
        this.hitReRoll = hitReRoll;
        this.hitToReRoll = hitToReRoll;
        this.hitOnSixEvent = hitOnSixEvent;
        this.hitMod = hitMod;
        this.targetNum = targetNum;
        this.woundReRoll = woundReRoll;
        this.woundToReRoll = woundToReRoll;
        this.woundOnSixEvent = woundOnSixEvent;
        this.woundMod = woundMod;
        this.penetration = penetration;
        this.damage = damage;
        this.save = save;
        this.saveMod = saveMod;
        this.cover = cover;
        this.saveReRoll = saveReRoll;
        this.saveToReRoll = saveToReRoll;
        this.feelNoPain = feelNoPain;
    }

    public int numberOfAttacks;
    public int targetRoll;
    public int hitReRoll;
    public int hitToReRoll;
    public HitOnSixEvent hitOnSixEvent;
    public int hitMod;

    public int targetNum;
    public int woundReRoll;
    public int woundToReRoll;
    public WoundOnSixEvent woundOnSixEvent;
    public int woundMod;
    public int penetration;
    public int damage;

    public int save;
    public int saveMod;
    public int cover;
    public int saveReRoll;
    public int saveToReRoll;
    public int feelNoPain;
    
}