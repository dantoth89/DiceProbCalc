using DiceProbCalc;

namespace DiceTest;

public class ToHitTests
{
    private ICalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }
    
    // array fields: number of attack / target number / re roll / what to re roll / on six / on six event / hit mod

    [Test]
    public void ToHit_Failed_input_Test()
    {
        int[] arr = { 0, 2, 0, 1, 1, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 0, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_High_Chance()
    {
        int[] arr = { 12, 2, 0, 0, 0, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 10, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Mid_Chance()
    {
        int[] arr = { 6, 4, 0, 0, 0, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Low_Chance()
    {
        int[] arr = { 6, 6, 0, 0, 0, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 1, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_NotWholeNum_1()
    {
        int[] arr = { 3, 4, 0, 0, 0, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 1.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_NotWholeNum_2()
    {
        int[] arr = { 1, 6, 0, 0, 0, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 0.16, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_Positive()
    {
        int[] arr = { 6, 5, 0, 0, 0, 1 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_Negative()
    {
        int[] arr = { 6, 3, 0, 0, 0, -1 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_NotValid_Positive()
    {
        int[] arr = { 6, 5, 0, 0, 0, 100 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_NotValid_Negative()
    {
        int[] arr = { 6, 5, 0, 0, 0, -100 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 1, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_all()
    {
        int[] arr = { 6, 4, 1, 0, 0, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 4.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_1_Low()
    {
        int[] arr = { 6, 4, 1, 1, 0, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 3.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_1_High()
    {
        int[] arr = { 42, 4, 1, 1, 0, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 24.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ExpSix_Low()
    {
        int[] arr = { 6, 4, 0, 0, 1, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 4, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ExpSix_High()
    {
        int[] arr = { 36, 4, 0, 0, 1, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 24, 0, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_Low()
    {
        int[] arr = { 6, 4, 0, 0, 4, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 3, 0, 1 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_High()
    {
        int[] arr = { 36, 4, 0, 0, 4, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 18, 0, 6 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_AWonSix_Low()
    {
        int[] arr = { 6, 4, 0, 0, 3, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 3, 1, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_AWonSix_High()
    {
        int[] arr = { 36, 4, 0, 0, 3, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 18, 6, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_Twin_Exp_onSix_Low()
    {
        int[] arr = { 6, 4, 0, 0, 2, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 5, 0, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_Twin_Exp_onSix_High()
    {
        int[] arr = { 36, 4, 0, 0, 2, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 30, 0, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_DmgAsMW_onSix_Low()
    {
        int[] arr = { 6, 4, 0, 0, 5, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 3, 1, 1 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_DmgAsMW_onSix_High()
    {
        int[] arr = { 36, 4, 0, 0, 5, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 18, 6, 6 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_Low_With_ReRollAll()
    {
        int[] arr = { 12, 4, 1, 0, 4, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 9, 0, 3 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_High_With_ReRollAll()
    {
        int[] arr = { 36, 4, 1, 0, 4, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 27, 0, 8 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_all_ExpSix()
    {
        // 12d - 4+ -> 6 + 2
        // 6d - 4+ -> 3 + 1
        // ~ 7 + 4 -> 11
        int[] arr = { 12, 4, 1, 0, 1, 0 };
        double[] result = _calculator.ToHit(arr);
        double[] expected = { 12, 0, 0 };
        Assert.AreEqual(expected, result);
    }
}