namespace DiceProbCalc.Services;

public class Calculator : ICalculator
{
    public double[] ToHit(int[] toHitDataArr)
    {
        int numOfAtk = toHitDataArr[0];
        int targetNum = toHitDataArr[1];
        int reRoll = toHitDataArr[2];
        int toReRoll = toHitDataArr[3];
        int onSixEvent = toHitDataArr[4];
        int hitMod = toHitDataArr[5];

        var finalTargetNum = CheckMod(targetNum, hitMod);
        var baseRoll = BaseRoll(numOfAtk, finalTargetNum);
        double finalHit = baseRoll;

        // number of hits , auto wound hits , mortal wounds
        double[] finalHitData = { 0, 0, 0 };

        if (WrongInputHandler(numOfAtk, targetNum, onSixEvent))
            return finalHitData;

        var reRolledAmount = finalHit;
        finalHit = ReRoll(numOfAtk, reRoll, toReRoll, finalHit, baseRoll, finalTargetNum);
        reRolledAmount = finalHit - reRolledAmount;

        finalHit = CutToTwoDecimalDigit(finalHit);

        finalHitData = HandleSixesOnHit(finalHit, numOfAtk, onSixEvent, reRolledAmount);
        return finalHitData;
    }

    public double[] ToWound(double[] hitsArr, int[] toWoundDataArr)
    {
        int numberOfHits = (int)Math.Floor(hitsArr[0]);
        int autoWounds = (int)Math.Floor(hitsArr[1]);
        int mortalWounds = (int)Math.Floor(hitsArr[2]);

        int targetNum = toWoundDataArr[0];
        int reRoll = toWoundDataArr[1];
        int toReRoll = toWoundDataArr[2];
        int onSixEvent = toWoundDataArr[3];
        int woundMod = toWoundDataArr[4];
        int penetration = toWoundDataArr[5];
        int damage = toWoundDataArr[6];

        var finalTargetNum = CheckMod(targetNum, woundMod);
        var baseRoll = BaseRoll(numberOfHits, finalTargetNum);
        double finalWound = baseRoll;
        // wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound
        double[] finalWoundData = { 0, 0, 0, 0, 0, 0 };

        if (WrongInputHandler(numberOfHits, targetNum, onSixEvent))
            return finalWoundData;

        var reRolledAmount = finalWound;
        finalWound = ReRoll(numberOfHits, reRoll, toReRoll, finalWound, baseRoll, finalTargetNum);
        reRolledAmount = finalWound - reRolledAmount;

        finalWound = CutToTwoDecimalDigit(finalWound);

        finalWoundData = HandleSixesOnWound(finalWound, numberOfHits, onSixEvent, reRolledAmount, penetration, damage);

        if (mortalWounds == autoWounds && mortalWounds != 0)
            finalWoundData[2] = mortalWounds * damage;


        return finalWoundData;
    }

    public double Save(double[] woundsArr, int[] saveDataArr)
    {
        // wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound

        int saveRolls = (int)Math.Floor(woundsArr[0]);
        int penetration = (int)Math.Floor(woundsArr[1]);
        int mortalWounds = (int)Math.Floor(woundsArr[2]);
        int woundsWithIncPen = (int)Math.Floor(woundsArr[3]);
        int amountOfIncPen = (int)Math.Floor(woundsArr[4]);
        int damage = (int)Math.Floor(woundsArr[5]);

        int save = saveDataArr[0];
        int saveMod = saveDataArr[1];
        int cover = saveDataArr[2];
        int reRoll = saveDataArr[3];
        int toReRoll = saveDataArr[4];
        int feelNoPain = saveDataArr[5];

        double finalUnsavedWound;
        

        var finalSaveNum = save + saveMod - penetration;
        if (finalSaveNum > 3)
            if (cover > 0)
                finalSaveNum += 1;
        
        if (WrongInputHandler(saveRolls, finalSaveNum, 0))
            return -1;

        double reRolledAmount = finalSaveNum;
        var baseRoll = BaseRoll(saveRolls, finalSaveNum);

        finalUnsavedWound = ReRoll(saveRolls, reRoll, toReRoll, reRolledAmount, baseRoll, finalSaveNum);
        //reRolledAmount = finalUnsavedWound - reRolledAmount;

        if (woundsWithIncPen < 0)
            finalUnsavedWound += BaseRoll(woundsWithIncPen, save + saveMod - penetration - amountOfIncPen);

        finalUnsavedWound *= damage;

        if (feelNoPain > 0)
            finalUnsavedWound -= (finalUnsavedWound + mortalWounds) * (feelNoPain - 1 / 6);

        finalUnsavedWound = CutToTwoDecimalDigit(finalUnsavedWound);
        return finalUnsavedWound;
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
            finalTargetNum = 1;
        else if (finalTargetNum > 6)
            finalTargetNum = 6;

        return finalTargetNum;
    }

    private static double NumOfSix(double rolls)
    {
        return Math.Ceiling(rolls / 6);
    }

    private static double ReRoll(int numOfAtk, int reRoll, int toReRoll, double finalHit, double baseRoll,
        int finalTargetNum)
    {
        if (reRoll > 0)
            if (toReRoll == 0)
                finalHit += (numOfAtk - baseRoll) * ((double)(finalTargetNum - 1) / 6);
            else
                finalHit += (numOfAtk * toReRoll / 6) * ((double)(finalTargetNum - 1) / 6);
        return finalHit;
    }

    private static double BaseRoll(int rollAmount, int finalTargetNum)
    {
        double baseRoll = rollAmount - (rollAmount * ((double)(finalTargetNum - 1) / 6));
        return baseRoll;
    }
}