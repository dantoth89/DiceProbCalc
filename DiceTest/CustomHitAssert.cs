using DiceProbCalc.Models;

namespace DiceTest;

using System;
using NUnit.Framework;

public static class CustomHitAssert
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

        if (expected is HitResults expectedHitResults && actual is HitResults actualHitResults)
        {
            Console.WriteLine(expectedHitResults.numberOfHits
                              + " " + actualHitResults.numberOfHits
                              + " ," + expectedHitResults.autoWounds
                              + " " + actualHitResults.autoWounds
                              + " ," + expectedHitResults.mortalWounds
                              + " " + actualHitResults.mortalWounds);

            return expectedHitResults.numberOfHits == actualHitResults.numberOfHits &&
                   expectedHitResults.autoWounds == actualHitResults.autoWounds &&
                   expectedHitResults.mortalWounds == actualHitResults.mortalWounds;
        }

        return expected.Equals(actual);
    }

}
