using DiceProbCalc;
using DiceProbCalc.Models;
using DiceProbCalc.Models.Enums;
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
        HitValues attack = new HitValues( 0, 2, 0, 1, HitOnSixEvent.PlusOneHit, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 0, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_High_Chance()
    {
        HitValues attack = new HitValues( 12, 2, 0, 0, 0, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 10, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Mid_Chance()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, 0, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 3, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Low_Chance()
    {
        HitValues attack = new HitValues( 6, 6, 0, 0, 0, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 1, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_NotWholeNum_1()
    {
        HitValues attack = new HitValues( 3, 4, 0, 0, 0, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 1.5, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_NotWholeNum_2()
    {
        HitValues attack = new HitValues( 1, 6, 0, 0, 0, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 0.16, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_Positive()
    {
        HitValues attack = new HitValues( 6, 5, 0, 0, 0, 1 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 3, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_Negative()
    {
        HitValues attack = new HitValues( 6, 3, 0, 0, 0, -1 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 3, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_NotValid_Positive()
    {
        HitValues attack = new HitValues( 6, 5, 0, 0, 0, 100 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 3, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_Modifiers_NotValid_Negative()
    {
        HitValues attack = new HitValues( 6, 5, 0, 0, 0, -100 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 1, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_all()
    {
        HitValues attack = new HitValues( 6, 4, 1, 0, 0, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 4.5, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_1_Low()
    {
        HitValues attack = new HitValues( 6, 4, 1, 1, 0, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 3.5, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_1_High()
    {
        HitValues attack = new HitValues( 42, 4, 1, 1, 0, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 24.5, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ExpSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, HitOnSixEvent.PlusOneHit, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 4, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ExpSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, HitOnSixEvent.PlusOneHit, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 24, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, HitOnSixEvent.PlusOneMortalWound, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 3, 0, 1 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, HitOnSixEvent.PlusOneMortalWound, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 18, 0, 6 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_AWonSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, HitOnSixEvent.AutoWound, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 3, 1, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_AWonSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, HitOnSixEvent.AutoWound, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 18, 6, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_Twin_Exp_onSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, HitOnSixEvent.PlusTwoHit, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 5, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_Twin_Exp_onSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, HitOnSixEvent.PlusTwoHit, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 30, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_DmgAsMW_onSix_Low()
    {
        HitValues attack = new HitValues( 6, 4, 0, 0, HitOnSixEvent.DealDamageAsMortal, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 3, 0, -1 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_DmgAsMW_onSix_High()
    {
        HitValues attack = new HitValues( 36, 4, 0, 0, HitOnSixEvent.DealDamageAsMortal, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 18, 0, -6 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_Low_With_ReRollAll()
    {
        HitValues attack = new HitValues( 12, 4, 1, 0, HitOnSixEvent.PlusOneMortalWound, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 9, 0, 3 );
        CustomHitAssert.AreEqual(expected, result);
    }
    
    [Test]
    public void ToHit_Test_MWonSix_High_With_ReRollAll()
    {
        HitValues attack = new HitValues( 36, 4, 1, 0,  HitOnSixEvent.PlusOneMortalWound, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 27, 0, 8 );
        CustomHitAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToHit_Test_ReRoll_all_ExpSix()
    {
        HitValues attack = new HitValues( 12, 4, 1, 0, HitOnSixEvent.PlusOneHit, 0 );
        HitResults result = _calculator.ToHit(attack);
        HitResults expected = new HitResults( 12, 0, 0 );
        CustomHitAssert.AreEqual(expected, result);
    }
}