using DiceProbCalc.Models.Enums;

namespace DiceProbCalc.Models;

public class AllValues
{
    // public AllValues(int numberOfAttacks, int targetRoll, int hitReRoll, int hitToReRoll, HitOnSixEvent hitOnSixEvent,
    //     int hitMod, int targetNum, int woundReRoll, int woundToReRoll, WoundOnSixEvent woundOnSixEvent, int woundMod,
    //     int penetration, int damage, int save, int saveMod, int cover, int saveReRoll, int saveToReRoll, int feelNoPain)
    // {
    //     this.numberOfAttacks = numberOfAttacks;
    //     this.targetRoll = targetRoll;
    //     this.hitReRoll = hitReRoll;
    //     this.hitToReRoll = hitToReRoll;
    //     this.hitOnSixEvent = hitOnSixEvent;
    //     this.hitMod = hitMod;
    //     this.targetNum = targetNum;
    //     this.woundReRoll = woundReRoll;
    //     this.woundToReRoll = woundToReRoll;
    //     this.woundOnSixEvent = woundOnSixEvent;
    //     this.woundMod = woundMod;
    //     this.penetration = penetration;
    //     this.damage = damage;
    //     this.save = save;
    //     this.saveMod = saveMod;
    //     this.cover = cover;
    //     this.saveReRoll = saveReRoll;
    //     this.saveToReRoll = saveToReRoll;
    //     this.feelNoPain = feelNoPain;
    // }
    //
    // public AllValues(int numberOfAttacks, int targetRoll, int hitReRoll, int hitToReRoll, int hitOnSixEventNum,
    //     int hitMod, int targetNum, int woundReRoll, int woundToReRoll, int woundOnSixEventNum, int woundMod,
    //     int penetration, int damage, int save, int saveMod, int cover, int saveReRoll, int saveToReRoll, int feelNoPain)
    // {
    //     this.numberOfAttacks = numberOfAttacks;
    //     this.targetRoll = targetRoll;
    //     this.hitReRoll = hitReRoll;
    //     this.hitToReRoll = hitToReRoll;
    //     this.hitMod = hitMod;
    //     this.targetNum = targetNum;
    //     this.woundReRoll = woundReRoll;
    //     this.woundToReRoll = woundToReRoll;
    //     this.woundMod = woundMod;
    //     this.penetration = penetration;
    //     this.damage = damage;
    //     this.save = save;
    //     this.saveMod = saveMod;
    //     this.cover = cover;
    //     this.saveReRoll = saveReRoll;
    //     this.saveToReRoll = saveToReRoll;
    //     this.feelNoPain = feelNoPain;
    //
    //     switch (hitOnSixEventNum)
    //     {
    //         case 1:
    //             hitOnSixEvent = HitOnSixEvent.PlusOneHit;
    //             break;
    //         case 2:
    //             hitOnSixEvent = HitOnSixEvent.PlusTwoHit;
    //             break;
    //         case 3:
    //             hitOnSixEvent = HitOnSixEvent.AutoWound;
    //             break;
    //         case 4:
    //             hitOnSixEvent = HitOnSixEvent.PlusOneMortalWound;
    //             break;
    //         case 5:
    //             hitOnSixEvent = HitOnSixEvent.DealDamageAsMortal;
    //             break;
    //         default:
    //             hitOnSixEvent = HitOnSixEvent.NoEvent;
    //             break;
    //     }
    //
    //     switch (woundOnSixEventNum)
    //     {
    //         case 1:
    //             woundOnSixEvent = WoundOnSixEvent.MinusOnePenetration;
    //             break;
    //         case 2:
    //             woundOnSixEvent = WoundOnSixEvent.PlusOneMortalWound;
    //             break;
    //         case 3:
    //             woundOnSixEvent = WoundOnSixEvent.DealDamageAsMortalWound;
    //             break;
    //
    //         default:
    //             woundOnSixEvent = WoundOnSixEvent.NoEvent;
    //             break;
    //     }
    // }

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