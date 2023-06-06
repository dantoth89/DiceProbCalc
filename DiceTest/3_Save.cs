using DiceProbCalc;
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
        // save / saveMod / cover / reRoll / toReRoll / feelNoPain 
        
        int[] saveArr = { 0, 0, 0, 0, 0, 0 };
        double[] WoundArr = { 0, 2, 0, 1, 1, 0, 0 };
        double result = _calculator.Save(WoundArr, saveArr);
        double expected = -1;
        Assert.AreEqual(expected, result);
    }
    
    
}