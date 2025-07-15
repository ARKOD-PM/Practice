using Xunit;

public class TestClass
{
	public int PublicField;
	private string _privateField;
	public int Property { get; set; }

	public void Method() { }
	// Создан для теста метода GetMethosParams из реализации ClassAnalyzer
	public int MethodWithParams(int a, string b) => 0;
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
	[Fact]
	public void GetPublicMethods_ReturnsCorrectMethods()
	{
		var analyzer = new ClassAnalyzer(typeof(TestClass));
		var methods = analyzer.GetPublicMethods();
	
		Assert.Contains("Method", methods);
	}
	
	[Fact]
	public void GetAllFields_IncludesPrivateFields()
	{
		var analyzer = new ClassAnalyzer(typeof(TestClass));
		var fields = analyzer.GetAllFields();
		
		Assert.Contains("_privateField", fields);
	}

	[Fact]
	public void GetMethodParams_ReturnsParametersAndReturnType()
	{
		var analyzer = new ClassAnalyzer(typeof(TestClass));
		var paramsInfo = analyzer.GetMethodParams("MethodWithParams");
		
		Assert.Contains("Int32 a", paramsInfo);
		Assert.Contains("String b", paramsInfo);
		Assert.Contains("Возвращает: Int32", paramsInfo);
	}

	[Fact]
	public void GetProperties_ReturnsPropertyNames()
	{
		var analyzer = new ClassAnalyzer(typeof(TestClass));
		var properties = analyzer.GetProperties();
		
		Assert.Single(properties);
		Assert.Contains("Property", properties);
	}

	[Fact]
	public void HasAttribute_ReturnsTrueForClassWithAttribute()
	{
		var analyzer = new ClassAnalyzer(typeof(AttributedClass));
		Assert.True(analyzer.HasAttribute<SerializableAttribute>());
	}

	[Fact]
	public void HasAttribute_ReturnsFalseForClassWithoutAttribute()
	{
		var analyzer = new ClassAnalyzer(typeof(TestClass));
		Assert.False(analyzer.HasAttribute<SerializableAttribute>());
	}
}
