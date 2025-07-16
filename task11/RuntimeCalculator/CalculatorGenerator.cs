using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace RuntimeCalculator;

public static class CalculatorGenerator
{
	public static ICalculator GenerateCalculator(string originalCode)
	{
		string modifiedCode = originalCode.Replace(
			"public class Calculator", 
			"public class Calculator : ICalculator"
		);
		
		string fullCode = "using RuntimeCalculator;\n" + modifiedCode;
		
		var syntaxTree = CSharpSyntaxTree.ParseText(fullCode);

		var references = new List<MetadataReference>
		{
			MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
			MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
		};

		var compilation = CSharpCompilation.Create(
			assemblyName: Path.GetRandomFileName(),
			syntaxTrees: new[] { syntaxTree },
			references: references,
			options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
		);

		using var ms = new MemoryStream();
		var result = compilation.Emit(ms);

		if (!result.Success)
		{
			var errors = result.Diagnostics
				.Where(d => d.Severity == DiagnosticSeverity.Error)
				.Select(e => e.GetMessage());
			throw new Exception($"Ошибка компиляции:\n{string.Join("\n", errors)}");
		}

		ms.Seek(0, SeekOrigin.Begin);
		var assembly = Assembly.Load(ms.ToArray());
		var type = assembly.GetType("Calculator");
		var instance = Activator.CreateInstance(type);

		return (ICalculator)instance;
	}
}
