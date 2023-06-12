using System.Runtime.InteropServices.JavaScript;
using DiceProbCalc;
using DiceProbCalc.Models;
using DiceProbCalc.Models.Enums;
using DiceProbCalc.Services;

namespace DiceTest;

public class ToWoundTest
{
    private ICalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }

    [Test]
    public void ToWound_Failed_input_Test()
    {
        HitResults hitResults = new HitResults(0, 0, 0);
        WoundValues woundValues = new WoundValues(0, 2, 0, 0, 1, 0, 0);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(0, 0, 0, 0, 0, 0);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_High_Chance()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(2, 0, 0, 0, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(10, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Low_Chance()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(6, 0, 0, 0, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(2, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Mid_Chance()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(4, 0, 0, 0, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(6, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Pen_Added()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(4, 0, 0, 0, 0, 2, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(6, 2, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Chance_NotWhole_1()
    {
        HitResults hitResults = new HitResults(3, 0, 0);
        WoundValues woundValues = new WoundValues(4, 0, 0, 0, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(1.5, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Chance_NotWhole_2()
    {
        HitResults hitResults = new HitResults(1, 0, 0);
        WoundValues woundValues = new WoundValues(6, 0, 0, 0, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(0.16, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Modifier_Positive()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(5, 0, 0, 0, 1, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(6, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Modifier_Positive_NotValid()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(5, 0, 0, 0, 100, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(6, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Modifier_Negative()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(3, 0, 0, 0, -1, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(6, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Modifier_Negative_NotValid()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(3, 0, 0, 0, -100, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(6, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_ReRollAll()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(4, 1, 0, 0, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(9, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_ReRoll_1_Low()
    {
        HitResults hitResults = new HitResults(6, 0, 0);
        WoundValues woundValues = new WoundValues(4, 1, 1, 0, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(3.5, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_ReRoll_1_High()
    {
        HitResults hitResults = new HitResults(42, 0, 0);
        WoundValues woundValues = new WoundValues(4, 1, 1, 0, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(24.5, 0, 0, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_MWSix()
    {
        HitResults hitResults = new HitResults(6, 0, 0);
        WoundValues woundValues = new WoundValues(4, 0, 0, WoundOnSixEvent.PlusOneMortalWound, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(3, 0, 1, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_Ap_Six()
    {
        HitResults hitResults = new HitResults(6, 0, 0);
        WoundValues woundValues = new WoundValues(4, 0, 0, WoundOnSixEvent.MinusOnePenetration, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(3, 0, 0, 1, 1, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_DMG_as_MW_Six_1()
    {
        HitResults hitResults = new HitResults(6, 0, 0);
        WoundValues woundValues = new WoundValues(4, 0, 0, WoundOnSixEvent.DealDamageAsMortalWound, 0, 0, 1);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(2, 0, 1, 0, 0, 1);
        CustomWoundAssert.AreEqual(expected, result);
    }

    [Test]
    public void ToWound_Test_DMG_as_MW_Six_2()
    {
        HitResults hitResults = new HitResults(12, 0, 0);
        WoundValues woundValues = new WoundValues(4, 0, 0, WoundOnSixEvent.DealDamageAsMortalWound, 0, 0, 2);
        WoundResults result = _calculator.ToWound(hitResults, woundValues);
        WoundResults expected = new WoundResults(4, 0, 4, 0, 0, 2);
        CustomWoundAssert.AreEqual(expected, result);
    }
}