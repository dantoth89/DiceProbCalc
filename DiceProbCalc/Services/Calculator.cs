using DiceProbCalc.Models;
using DiceProbCalc.Models.Enums;

namespace DiceProbCalc.Services;

public class Calculator : ICalculator
{
    public HitResults ToHit(HitValues hitValues)
    {
        // number of hits , auto wound hits , mortal wounds
        double[] finalHitData = { 0, 0, 0 };
        if (InputHandler(hitValues.numberOfAttacks, hitValues.targetRoll))
            return new HitResults(finalHitData);

        var finalTargetNum = CheckMod(hitValues.targetRoll, hitValues.hitMod);
        var baseRoll = BaseRoll(hitValues.numberOfAttacks, finalTargetNum);
        double finalHit = ReRoll(hitValues.numberOfAttacks, hitValues.reRoll, hitValues.toReRoll, baseRoll,
            finalTargetNum);
        double reRolledAmount = finalHit - baseRoll;

        finalHit = CutToTwoDecimalDigit(finalHit);

        finalHitData = HandleSixesOnHit(finalHit, hitValues.numberOfAttacks, hitValues.onSixEvent, reRolledAmount);
        return new HitResults(finalHitData);
    }

    public WoundResults ToWound(HitResults hitResults, WoundValues woundValues)
    {
        // wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound
        double[] finalWoundData = { 0, 0, 0, 0, 0, 0 };
        if (InputHandler(hitResults.numberOfHits, woundValues.targetNum))
            return new WoundResults(finalWoundData);
        
        var finalTargetNum = CheckMod(woundValues.targetNum, woundValues.woundMod);
        var baseRoll = BaseRoll(hitResults.numberOfHits, finalTargetNum);
        double finalWound = baseRoll;
        var reRolledAmount = finalWound;
        finalWound = ReRoll(hitResults.numberOfHits, woundValues.reRoll, woundValues.toReRoll, baseRoll,
            finalTargetNum);
        reRolledAmount = finalWound - reRolledAmount;

        finalWound = CutToTwoDecimalDigit(finalWound);

        finalWoundData = HandleSixesOnWound(finalWound, hitResults.numberOfHits, woundValues.onSixEvent,
            reRolledAmount, woundValues.penetration, woundValues.damage);

        if ((int)hitResults.mortalWounds == (int)hitResults.autoWounds && (int)hitResults.mortalWounds != 0)
            finalWoundData[2] = (int)hitResults.mortalWounds * woundValues.damage;

        return new WoundResults(finalWoundData);
    }

    public double Save(WoundResults woundResults, SaveValues saveValues)
    {
        // wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound
        var finalSaveNum = saveValues.save - saveValues.saveMod + (int)woundResults.penetration;
        if (finalSaveNum > 3)
            if (saveValues.cover > 0)
                finalSaveNum -= 1;

        if (InputHandler(woundResults.saveRolls, finalSaveNum))
            return -1;

        var baseRoll = BaseRoll(woundResults.saveRolls, finalSaveNum);

        double savedAttack = ReRollSave(woundResults.saveRolls, saveValues.reRoll, saveValues.toReRoll, baseRoll,
            finalSaveNum);
        
        // if ((int)woundResults.woundsWithIncPen < 0)
        //     savedAttack += BaseRoll(woundResults.woundsWithIncPen,
        //         saveValues.save + saveValues.saveMod - (int)woundResults.penetration -
        //         (int)woundResults.amountOfIncPen);

        double unsavedWound = (int)woundResults.saveRolls - savedAttack;
        unsavedWound *= (int)woundResults.damage;

        if (saveValues.feelNoPain > 0)
            unsavedWound = (unsavedWound + (int)woundResults.mortalWounds) * (saveValues.feelNoPain - 1.0) / 6.0;

        unsavedWound = CutToTwoDecimalDigit(unsavedWound);
        return unsavedWound;
    }

    private double[] HandleSixesOnWound(double finalWound, double numberOfHits, WoundOnSixEvent sixEvent,
        double reRolledAmount,
        int penetration, int damage)
    {
        // wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound
        double[] finalSixWoundData = { 0, penetration, 0, 0, 0, damage };
        var sixes = NumOfSix(numberOfHits) + NumOfSix(reRolledAmount);

        switch (sixEvent)
        {
            case WoundOnSixEvent.MinusOnePenetration:
                finalSixWoundData[0] = finalWound;
                finalSixWoundData[3] = sixes;
                finalSixWoundData[4] = 1;
                break;
            case WoundOnSixEvent.PlusOneMortalWound:
                finalSixWoundData[0] = finalWound;
                finalSixWoundData[2] = sixes;
                break;
            case WoundOnSixEvent.DealDamageAsMortalWound:
                finalSixWoundData[0] = finalWound - sixes;
                finalSixWoundData[2] = sixes * damage;
                break;
            default:
                finalSixWoundData[0] = finalWound;
                break;
        }

        return finalSixWoundData;
    }


    private static bool InputHandler(double numOfAtk, int targetNum)
    {
        if (numOfAtk < 1 || targetNum < 1)
            return true;

        return false;
    }

    private static double[] HandleSixesOnHit(double finalHit, int numOfAtk, HitOnSixEvent sixEvent,
        double reRolledAmount)
    {
        double[] finalSixHitData = { 0, 0, 0 };
        var sixes = NumOfSix(numOfAtk) + NumOfSix(reRolledAmount);

        switch (sixEvent)
        {
            case HitOnSixEvent.PlusOneHit:
                finalHit += sixes;
                finalSixHitData[0] = finalHit;
                break;
            case HitOnSixEvent.PlusTwoHit:
                finalHit += sixes * 2;
                finalSixHitData[0] = finalHit;
                break;
            case HitOnSixEvent.AutoWound:
                finalSixHitData[0] = finalHit;
                finalSixHitData[1] = sixes;
                break;
            case HitOnSixEvent.PlusOneMortalWound:
                finalSixHitData[0] = finalHit;
                finalSixHitData[2] = sixes;
                break;
            case HitOnSixEvent.DealDamageAsMortal:
                finalSixHitData[0] = finalHit;
                finalSixHitData[1] = sixes;
                finalSixHitData[2] = sixes;
                break;
            default:
                finalSixHitData[0] = finalHit;
                break;
        }

        return finalSixHitData;
    }


    private static double CutToTwoDecimalDigit(double finalHit)
    {
        return Math.Floor(finalHit * 100) / 100;
    }

    private static int CheckMod(int targetNum, int mod)
    {
        if (mod > 1)
            mod = 1;
        if (mod < -1)
            mod = -1;

        int finalTargetNum = targetNum - mod;

        if (finalTargetNum < 1)
            return 1;
        if (finalTargetNum > 6)
            return 6;

        return finalTargetNum;
    }

    private static double NumOfSix(double rolls)
    {
        return Math.Ceiling(rolls / 6);
    }

    private static double ReRoll(double numOfAtk, int reRoll, int toReRoll, double baseRoll, int finalTargetNum)
    {
        double targetRoll = (finalTargetNum - 1.0) / 6.0;
        if (reRoll > 0)
            if (toReRoll == 0)
                baseRoll += targetRoll * (numOfAtk - baseRoll);
            else
                baseRoll += targetRoll * (numOfAtk * toReRoll / 6);
        return baseRoll;
    }
    
    private static double ReRollSave(double numOfSaveRolls, int reRoll, int toReRoll, double baseRoll, int finalTargetNum)
    {
        double targetRoll = 1 - (finalTargetNum - 1.0) / 6.0;
        if (reRoll > 0)
            if (toReRoll == 0)
                baseRoll += targetRoll * (numOfSaveRolls - baseRoll);
            else
                baseRoll += targetRoll * (numOfSaveRolls * toReRoll / 6);
        return baseRoll;
    }
    
    private static double BaseRoll(double rollAmount, int finalTargetNum)
    {
        return rollAmount - rollAmount * (finalTargetNum - 1) / 6;
    }
}