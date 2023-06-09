﻿using DiceProbCalc.Models.Enums;

namespace DiceProbCalc.Models;

public class WoundValues
{
    public WoundValues(int targetNum, int reRoll, int toReRoll, WoundOnSixEvent onSixEvent, int woundMod,
        int penetration,
        int damage)
    {
        this.targetNum = targetNum;
        this.reRoll = reRoll;
        this.toReRoll = toReRoll;
        this.onSixEvent = onSixEvent;
        this.woundMod = woundMod;
        this.penetration = penetration;
        this.damage = damage;
    }

    public int targetNum;
    public int reRoll;
    public int toReRoll;
    public WoundOnSixEvent onSixEvent;
    public int woundMod;
    public int penetration;
    public int damage;
}