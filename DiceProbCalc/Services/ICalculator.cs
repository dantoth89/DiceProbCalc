namespace DiceProbCalc;

public interface ICalculator
{
    public double[] ToHit(int[] toHitDataArr);
    public double[] ToWound(double[] hitsArr, int[] toWoundDataArr);
    public double Save(double[] woundsArr, int[] saveDataArr);
}