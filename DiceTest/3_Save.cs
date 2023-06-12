using DiceProbCalc;
using DiceProbCalc.Models;
using DiceProbCalc.Services;

namespace DiceTest;

public class SaveTest
{
    private ICalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator();
    }
    
    [Test]
    public void Save_Failed_input_Test()
    {
        SaveValues saveValues = new SaveValues( 0, 0, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults(0, 2, 0, 1, 1, 0);
        double result = _calculator.Save(woundResults, saveValues);
        double expected = -1;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_Low()
    {
        SaveValues saveValues = new SaveValues( 6, 0, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 6, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 5;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_High()
    {
        SaveValues saveValues = new SaveValues( 6, 0, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 93, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 77.5;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_Low_Positive_Mod()
    {
        SaveValues saveValues = new SaveValues( 6, 1, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 6, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 4;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_High_Positive_Mod()
    {
        SaveValues saveValues = new SaveValues( 6, 1, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 93, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 62;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_Low_Negative_Mod()
    {
        SaveValues saveValues = new SaveValues( 5, -1, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 6, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 5;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_High_Negative_Mod()
    {
        SaveValues saveValues = new SaveValues( 5, -1, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 93, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 77.5;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_Low_Cover()
    {
        SaveValues saveValues = new SaveValues( 5, 0, 1, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 6, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 3;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_High_Cover()
    {
        SaveValues saveValues = new SaveValues( 6, 0, 1, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 93, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 62;
        Assert.AreEqual(expected, result);
    }
    
        
    [Test]
    public void Save_Roll_Low_Full_ReRoll()
    {
        SaveValues saveValues = new SaveValues( 4, 0, 0, 1, 0, 0 );
        WoundResults woundResults = new WoundResults( 12, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 3;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_High_Full_ReRoll()
    {
        SaveValues saveValues = new SaveValues( 4, 0, 0, 1, 0, 0 );
        WoundResults woundResults = new WoundResults( 60, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 15;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_Low_1_ReRoll()
    {
        SaveValues saveValues = new SaveValues( 2, 0, 0, 1, 1, 0 );
        WoundResults woundResults = new WoundResults( 12, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 0.33;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_High_1_ReRoll()
    {
        SaveValues saveValues = new SaveValues( 5, 0, 0, 1, 1, 0 );
        WoundResults woundResults = new WoundResults( 90, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 55;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_Low_FNP()
    {
        SaveValues saveValues = new SaveValues( 4, 0, 0, 0, 0, 5 );
        WoundResults woundResults = new WoundResults( 12, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 4;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_High_FNP()
    {
        SaveValues saveValues = new SaveValues( 4, 0, 0, 0, 0, 5 );
        WoundResults woundResults = new WoundResults( 90, 0, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 30;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_Low_With_Penetration()
    {
        SaveValues saveValues = new SaveValues( 3, 0, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 12, 1, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 6;
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Save_Roll_High_With_Penetration()
    {
        SaveValues saveValues = new SaveValues( 2, 0, 0, 0, 0, 0 );
        WoundResults woundResults = new WoundResults( 90, 2, 0, 0, 0, 1 );
        double result = _calculator.Save(woundResults, saveValues);
        double expected = 45;
        Assert.AreEqual(expected, result);
    }
}