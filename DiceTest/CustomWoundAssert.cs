using DiceProbCalc.Models;

namespace DiceTest;

public class CustomWoundAssert
{
    public static void AreEqual(object expected, object actual)
    {
        if (!AreEqualInternal(expected, actual))
        {
            string message = $"Expected: {expected}\nActual: {actual}";
            throw new AssertionException(message);
        }
    }

    private static bool AreEqualInternal(object expected, object actual)
    {
        if (expected == null || actual == null)
            return false;

        if (!expected.GetType().Equals(actual.GetType()))
            return false;

        if (expected is WoundResults expectedWoundResults && actual is WoundResults actualWoundResults)
        {
            return expectedWoundResults.saveRolls == actualWoundResults.saveRolls &&
                   expectedWoundResults.penetration == actualWoundResults.penetration &&
                   expectedWoundResults.mortalWounds == actualWoundResults.mortalWounds &&
                   expectedWoundResults.woundsWithIncPen == actualWoundResults.woundsWithIncPen &&
                   expectedWoundResults.amountOfIncPen == actualWoundResults.amountOfIncPen &&
                   expectedWoundResults.damage == actualWoundResults.damage;
        }

        return expected.Equals(actual);
    }
}