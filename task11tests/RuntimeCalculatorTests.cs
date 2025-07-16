using Xunit;
using RuntimeCalculator;

public class RuntimeCalculatorTests
{
	private const string realization = @"
		public class Calculator
		{
		public int Add(int a, int b) => a + b;
		public int Minus(int a, int b) => a - b;
		public int Mul(int a, int b) => a * b;
		public int Div(int a, int b) => a / b;
		}";

	[Fact]
	public void Add_ShouldReturnSum()
	{
		ICalculator calculator = CalculatorGenerator.GenerateCalculator(realization);
		Assert.Equal(1337, calculator.Add(1210, 127));
	}

	[Fact]
	public void Minus_ShouldReturnDifference()
	{
		ICalculator calculator = CalculatorGenerator.GenerateCalculator(realization);
		Assert.Equal(993, calculator.Minus(1000, 7));
	}

	[Fact]
	public void Mul_ShouldReturnProduct()
	{
		ICalculator calculator = CalculatorGenerator.GenerateCalculator(realization);
		Assert.Equal(30, calculator.Mul(10, 3));
	}

	[Fact]
	public void Div_ShouldReturnQuotient()
	{
		ICalculator calculator = CalculatorGenerator.GenerateCalculator(realization);
		Assert.Equal(4, calculator.Div(12, 3));
	}

	[Fact]
	public void DivByZero_ShouldThrowDivideByZeroException()
	{
		ICalculator calculator = CalculatorGenerator.GenerateCalculator(realization);
		Assert.Throws<DivideByZeroException>(() => calculator.Div(0, 0));
	}
}
