namespace DiceProbCalc;

public class Calculator : ICalculator
{
    public double[] ToHit(int numOfAtk, int targetNum, bool reRoll, int toReRoll, bool onSix, int onSixEvent, int hitMod, string dmg)
    {
        // number of hits , auto wound hits , mortal wounds
        double[] finalHitData = new double[] { 0, 0, 0 };
        if (numOfAtk < 1 || targetNum < 1)
            return finalHitData ;

        // onSixEvent:
        // 1- +1 hit,
        // 2- +2 hit,
        // 3- +1 hit Roll,
        // 4- auto wound,
        // 5- +1MW,
        // 6- deal dmg as mortal;

        var finalTargetNum = CheckMod(targetNum, hitMod);
        var baseRoll = BaseRoll(numOfAtk, finalTargetNum);
        double finalHit = baseRoll;
        
        if (reRoll && onSix)
        {
            finalHit = ReRoll(numOfAtk, reRoll, toReRoll, finalHit, baseRoll, finalTargetNum);
            finalHit += NumOfSix(finalHit);
        }
        else
        {
            finalHit = ReRoll(numOfAtk, reRoll, toReRoll, finalHit, baseRoll, finalTargetNum);
            if(onSix)
            finalHit += NumOfSix(finalHit);
        }
        
        finalHit = CutToTwoDecimalDigit(finalHit);

        finalHitData[0] = finalHit;
        return finalHitData;
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

    private static double ReRoll(int numOfAtk, bool reRoll, int toReRoll, double finalHit, double baseRoll,
        int finalTargetNum)
    {
        if (reRoll)
            if (toReRoll == 0)
                finalHit += (numOfAtk - baseRoll) * ((double)(finalTargetNum - 1) / 6);
            else
                finalHit += (numOfAtk * toReRoll / 6) * ((double)(finalTargetNum - 1) / 6);
        return finalHit;
    }

    private static double BaseRoll(int numOfAtk, int finalTargetNum)
    {
        double baseRoll = numOfAtk - (numOfAtk * ((double)(finalTargetNum - 1) / 6));
        return baseRoll;
    }

    public double[] ToWound()
    {
        throw new NotImplementedException();
    }

    public double Save()
    {
        throw new NotImplementedException();
    }
}