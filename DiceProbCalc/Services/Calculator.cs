using DiceProbCalc.Models;

namespace DiceProbCalc.Services;



public class Calculator : ICalculator
{
    public double[] ToHit(HitValues hitValues)
    {
        // number of hits , auto wound hits , mortal wounds
        double[] finalHitData = { 0, 0, 0 };
        var finalTargetNum = CheckMod(hitValues.targetRoll, hitValues.hitMod);
        var baseRoll = BaseRoll(hitValues.numberOfAttacks, finalTargetNum);

        if (WrongInputHandler(hitValues.numberOfAttacks, hitValues.targetRoll, hitValues.onSixEvent))
            return finalHitData;
        
        double finalHit = ReRoll(hitValues.numberOfAttacks, hitValues.reRoll, hitValues.toReRoll, baseRoll, finalTargetNum);
        double reRolledAmount = finalHit - baseRoll;

        finalHit = CutToTwoDecimalDigit(finalHit);

        finalHitData = HandleSixesOnHit(finalHit, hitValues.numberOfAttacks, hitValues.onSixEvent, reRolledAmount);
        return finalHitData;
    }

    public double[] ToWound(double[] hitsArr, WoundValues woundValues)
    {
        int numberOfHits = (int)Math.Floor(hitsArr[0]);
        int autoWounds = (int)Math.Floor(hitsArr[1]);
        int mortalWounds = (int)Math.Floor(hitsArr[2]);

        var finalTargetNum = CheckMod(woundValues.targetNum, woundValues.woundMod);
        var baseRoll = BaseRoll(numberOfHits, finalTargetNum);
        double finalWound = baseRoll;
        // wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound
        double[] finalWoundData = { 0, 0, 0, 0, 0, 0 };

        if (WrongInputHandler(numberOfHits, woundValues.targetNum, woundValues.onSixEvent))
            return finalWoundData;

        var reRolledAmount = finalWound;
        finalWound = ReRoll(numberOfHits, woundValues.reRoll, woundValues.toReRoll, baseRoll, finalTargetNum);
        reRolledAmount = finalWound - reRolledAmount;

        finalWound = CutToTwoDecimalDigit(finalWound);

        finalWoundData = HandleSixesOnWound(finalWound, numberOfHits, woundValues.onSixEvent, reRolledAmount, woundValues.penetration, woundValues.damage);

        if (mortalWounds == autoWounds && mortalWounds != 0)
            finalWoundData[2] = mortalWounds * woundValues.damage;

        return finalWoundData;
    }

    public double Save(double[] woundsArr, SaveValues saveValues)
    {
        // wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound

        int saveRolls = (int)Math.Floor(woundsArr[0]);
        int penetration = (int)Math.Floor(woundsArr[1]);
        int mortalWounds = (int)Math.Floor(woundsArr[2]);
        int woundsWithIncPen = (int)Math.Floor(woundsArr[3]);
        int amountOfIncPen = (int)Math.Floor(woundsArr[4]);
        int damage = (int)Math.Floor(woundsArr[5]);
   
        var finalSaveNum = saveValues.save - saveValues.saveMod + penetration;
        if (finalSaveNum > 3)
            if (saveValues.cover > 0)
                finalSaveNum -= 1;
        
        if (WrongInputHandler(saveRolls, finalSaveNum, 0))
            return -1;
        
        var baseRoll = BaseRoll(saveRolls, finalSaveNum);

        double savedWound = ReRoll(saveRolls, saveValues.reRoll, saveValues.toReRoll, baseRoll, finalSaveNum);

        if (woundsWithIncPen < 0)
            savedWound += BaseRoll(woundsWithIncPen, saveValues.save + saveValues.saveMod - penetration - amountOfIncPen);

        double unsavedWound = saveRolls - savedWound;
        unsavedWound *= damage;

        if (saveValues.feelNoPain > 0)
            unsavedWound = (unsavedWound + mortalWounds) * (saveValues.feelNoPain - 1.0) / 6.0;

        unsavedWound = CutToTwoDecimalDigit(unsavedWound);
        return unsavedWound;
    }

    private double[] HandleSixesOnWound(double finalWound, int numberOfHits, int eventNum, double reRolledAmount,
        int penetration, int damage)
    {
        // onSixEvent:
        // 1- -1 pen,
        // 2- +1MW,
        // 3- deal dmg as mortal;

        // wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound
        double[] finalSixWoundData = { 0, penetration, 0, 0, 0, damage };
        var sixes = NumOfSix(numberOfHits) + NumOfSix(reRolledAmount);

        switch (eventNum)
        {
            case 1:
                finalSixWoundData[0] = finalWound;
                finalSixWoundData[3] = sixes;
                finalSixWoundData[4] = 1;
                break;
            case 2:
                finalSixWoundData[0] = finalWound;
                finalSixWoundData[2] = sixes;
                break;
            case 3:
                finalSixWoundData[0] = finalWound - sixes;
                finalSixWoundData[2] = sixes * damage;
                break;
            default:
                finalSixWoundData[0] = finalWound;
                break;
        }

        return finalSixWoundData;
    }


    private static bool WrongInputHandler(int numOfAtk, int targetNum, int onSixEvent)
    {
        if (numOfAtk < 1 || targetNum < 1)
            return true;

        // fix needed
        if (onSixEvent > 5)
            return true;

        return false;
    }

    private static double[] HandleSixesOnHit(double finalHit, int numOfAtk, int eventNum, double reRolledAmount)
    {
        // onSixEvent:
        // 1- +1 hit,
        // 2- +2 hit,
        // 3- auto wound,
        // 4- +1MW,
        // 5- deal dmg as mortal;

        double[] finalSixHitData = { 0, 0, 0 };
        var sixes = NumOfSix(numOfAtk) + NumOfSix(reRolledAmount);

        switch (eventNum)
        {
            case 1:
                finalHit += sixes;
                finalSixHitData[0] = finalHit;
                break;
            case 2:
                finalHit += sixes * 2;
                finalSixHitData[0] = finalHit;
                break;
            case 3:
                finalSixHitData[0] = finalHit;
                finalSixHitData[1] = sixes;
                break;
            case 4:
                finalSixHitData[0] = finalHit;
                finalSixHitData[2] = sixes;
                break;
            case 5:
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

    private static double ReRoll(int numOfAtk, int reRoll, int toReRoll, double baseRoll,
        int finalTargetNum)
    {
        double targetRoll = (finalTargetNum - 1.0) / 6.0;
        if (reRoll > 0)
            if (toReRoll == 0)
                baseRoll += (numOfAtk - baseRoll) * targetRoll;
            else
                baseRoll += (numOfAtk * toReRoll / 6.0) * targetRoll;
        return baseRoll;
    }

    private static double BaseRoll(int rollAmount, int finalTargetNum)
    {
        double baseRoll = rollAmount - rollAmount * (finalTargetNum - 1.0) / 6.0;
        return baseRoll;
    }
}