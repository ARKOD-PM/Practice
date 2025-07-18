using Xunit;

public class DefiniteIntegralTests
{
    [Fact]
    public void IntegralOfX_FromMinus1To1_ShouldBeZero()
    {
        var X = (double x) => x;
        double result = DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2);
        Assert.Equal(0, result, 4);
    }

    [Fact]
    public void IntegralOfSin_FromMinus1To1_ShouldBeZero()
    {
        var SIN = (double x) => Math.Sin(x);
        double result = DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8);
        Assert.Equal(0, result, 4);
    }

    [Fact]
    public void IntegralOfX_From0To5_ShouldBe12_5()
    {
        var X = (double x) => x;
        double result = DefiniteIntegral.Solve(0, 5, X, 1e-6, 8);
        Assert.Equal(12.5, result, 5);
    }
}
