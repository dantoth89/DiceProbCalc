using DiceProbCalc;
using DiceProbCalc.Models;
using DiceProbCalc.Services;

namespace DiceTest;

public class ToHitTests
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
        HitValues attack = new HitValues( 0, 2, 0, 1, 1, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 0, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_High_Chance()
    {
        HitValues attack = new HitValues( 12, 2, 0, 0, 0, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 10, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Mid_Chance()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, 0, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Low_Chance()
    {
        HitValues attack = new HitValues( 6, 6, 0, 0, 0, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 1, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_NotWholeNum_1()
    {
        HitValues attack = new HitValues( 3, 4, 0, 0, 0, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 1.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_NotWholeNum_2()
    {
        HitValues attack = new HitValues( 1, 6, 0, 0, 0, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 0.16, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_Positive()
    {
        HitValues attack = new HitValues( 6, 5, 0, 0, 0, 1 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_Negative()
    {
        HitValues attack = new HitValues( 6, 3, 0, 0, 0, -1 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_NotValid_Positive()
    {
        HitValues attack = new HitValues( 6, 5, 0, 0, 0, 100 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 3, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_NotValid_Negative()
    {
        HitValues attack = new HitValues( 6, 5, 0, 0, 0, -100 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 1, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_all()
    {
        HitValues attack = new HitValues( 6, 4, 1, 0, 0, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 4.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_1_Low()
    {
        HitValues attack = new HitValues( 6, 4, 1, 1, 0, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 3.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_1_High()
    {
        HitValues attack = new HitValues( 42, 4, 1, 1, 0, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 24.5, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ExpSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, 1, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 4, 0, 0 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ExpSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, 1, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 24, 0, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, 4, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 3, 0, 1 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, 4, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 18, 0, 6 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_AWonSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, 3, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 3, 1, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_AWonSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, 3, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 18, 6, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_Twin_Exp_onSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, 2, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 5, 0, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_Twin_Exp_onSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, 2, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 30, 0, 0 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_DmgAsMW_onSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, 5, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 3, 1, 1 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_DmgAsMW_onSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, 5, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 18, 6, 6 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_Low_With_ReRollAll()
    {
        HitValues attack = new HitValues( 12, 4, 1, 0, 4, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 9, 0, 3 };
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_High_With_ReRollAll()
    {
        HitValues attack = new HitValues( 36, 4, 1, 0, 4, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 27, 0, 8 };
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_all_ExpSix()
    {
        HitValues attack = new HitValues( 12, 4, 1, 0, 1, 0 );
        double[] result = _calculator.ToHit(attack);
        double[] expected = { 12, 0, 0 };
        Assert.AreEqual(expected, result);
    }
}