using DiceProbCalc;

namespace DiceTest;

public class ToWoundTest
{
    private ICalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }

    // toWoundArr : targetNum, reRoll, toReRoll, onSixEvent, woundMod, penetration, damage 
    // finalWoundData : wounds / penetration / mortal wounds / wounds with increased penetration / amount of increase / damage per wound

    [Test]
    public void ToWound_Failed_input_Test()
    {
        double[] hitsArr = { 0, 0, 0 };
        int[] toWoundArr = { 0, 2, 0, 1, 1, 0, 0 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 0, 0, 0, 0, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_High_Chance()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 2, 0, 0, 0, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 10, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Low_Chance()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 6, 0, 0, 0, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 2, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Mid_Chance()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 4, 0, 0, 0, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 6, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToWound_Test_Pen_Added()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 4, 0, 0, 0, 0, 2, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 6, 2, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Chance_NotWhole_1()
    {
        double[] hitsArr = { 3, 0, 0 };
        int[] toWoundArr = { 4, 0, 0, 0, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 1.5, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Chance_NotWhole_2()
    {
        double[] hitsArr = { 1, 0, 0 };
        int[] toWoundArr = { 6, 0, 0, 0, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 0.16, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Modifier_Positive()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 5, 0, 0, 0, 1, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 6, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Modifier_Positive_NotValid()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 5, 0, 0, 0, 100, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 6, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Modifier_Negative()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 3, 0, 0, 0, -1, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 6, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Modifier_Negative_NotValid()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 3, 0, 0, 0, -100, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 6, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_ReRollAll()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 4, 1, 0, 0, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 9, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_ReRoll_1_Low()
    {
        double[] hitsArr = { 6, 0, 0 };
        int[] toWoundArr = { 4, 1, 1, 0, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 3.5, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_ReRoll_1_High()
    {
        double[] hitsArr = { 42, 0, 0 };
        int[] toWoundArr = { 4, 1, 1, 0, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 24.5, 0, 0, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_MWSix()
    {
        double[] hitsArr = { 6, 0, 0 };
        int[] toWoundArr = { 4, 0, 0, 2, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 3, 0, 1, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToWound_Test_Ap_Six()
    {
        double[] hitsArr = { 6, 0, 0 };
        int[] toWoundArr = { 4, 0, 0, 1, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 3, 0, 0, 1, 1, 1 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToWound_Test_DMG_as_MW_Six_1()
    {
        double[] hitsArr = { 6, 0, 0 };
        int[] toWoundArr = { 4, 0, 0, 3, 0, 0, 1 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 2, 0, 1, 0, 0, 1 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToWound_Test_DMG_as_MW_Six_2()
    {
        double[] hitsArr = { 12, 0, 0 };
        int[] toWoundArr = { 4, 0, 0, 3, 0, 0, 2 };
        double[] result = _calculator.ToWound(hitsArr, toWoundArr);
        double[] expected = { 4, 0, 4, 0, 0, 2 };
        Assert.AreEqual(expected, result);
    }
}