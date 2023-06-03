using DiceProbCalc;

namespace DiceTest;

public class Tests
{
    private ICalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }

    [Test]
    public void ToHit_Failed_input_Test()
    {
        double[] result = _calculator.ToHit(0, 2, false, 1, false, 1, 0, "1");
        double[] expected = { 0, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_High_Chance()
    {
        double[] result = _calculator.ToHit(12, 2, false, 1, false, 1, 0, "1");
        double[] expected = { 10, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Mid_Chance()
    {
        double[] result = _calculator.ToHit(6, 4, false, 1, false, 1, 0, "1");
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Low_Chance()
    {
        double[] result = _calculator.ToHit(6, 6, false, 1, false, 1, 0, "1");
        double[] expected = { 1, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_NotWholeNum_1()
    {
        double[] result = _calculator.ToHit(3, 4, false, 1, false, 1, 0, "1");
        double[] expected = { 1.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_NotWholeNum_2()
    {
        double[] result = _calculator.ToHit(1, 6, false, 1, false, 1, 0, "1");
        double[] expected = { 0.16, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_Positive()
    {
        double[] result = _calculator.ToHit(6, 5, false, 1, false, 1, 1, "1");
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_Negative()
    {
        double[] result = _calculator.ToHit(6, 3, false, 1, false, 1, -1, "1");
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_NotValid_Positive()
    {
        double[] result = _calculator.ToHit(6, 5, false, 1, false, 1, 100, "1");
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_NotValid_Negativre()
    {
        double[] result = _calculator.ToHit(6, 3, false, 1, false, 1, -100, "1");
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_all()
    {
        double[] result = _calculator.ToHit(6, 4, true, 0, false, 1, 0, "1");
        double[] expected = { 4.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_1_Low()
    {
        double[] result = _calculator.ToHit(6, 4, true, 1, false, 1, 0, "1");
        double[] expected = { 3.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_1_High()
    {
        double[] result = _calculator.ToHit(42, 4, true, 1, false, 1, 0, "1");
        double[] expected = { 24.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ExpSix_Low()
    {
        double[] result = _calculator.ToHit(6, 4, false, 0, true, 1, 0, "1");
        double[] expected = { 4, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ExpSix_High()
    {
        double[] result = _calculator.ToHit(36, 4, false, 0, true, 1, 0, "1");
        double[] expected = { 21, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_all_ExpSix()
    {
        // 12d - 4+ -> 6 + 1
        // 6d - 4+ -> 3 + 1
        // ~ 7 + 4 -> 11
        double[] result = _calculator.ToHit(12, 4, true, 0, true, 1, 0, "1");
        double[] expected = { 11, 0, 0 };
        Assert.AreEqual(expected, result);
    }
}