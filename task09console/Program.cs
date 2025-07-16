using System.Reflection;

namespace MetadataAnalyzer;

public class Program
{
	public static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("Использование: dotnet run --project <путь-до-проекта> <путь-до-dll>");
			return;
		}

		var assemblyPath = args[0];
		
		try
		{
			var assembly = Assembly.LoadFrom(assemblyPath);
			AnalyzeAssembly(assembly);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Ошибка загрузки сборки: {ex.Message}");
		}
	}

	private static void AnalyzeAssembly(Assembly assembly)
	{
		Console.WriteLine($"Сборка: {assembly.FullName}");
		Console.WriteLine();

		foreach (var type in assembly.GetTypes())
		{
			PrintTypeInfo(type);
		}
	}

	private static void PrintTypeInfo(Type type)
	{
		Console.WriteLine($"Класс: {type.FullName}");
		
		PrintAttributes(type.GetCustomAttributes());
		
		PrintConstructors(type);
		
		PrintMethods(type);
		
		Console.WriteLine();
	}

	private static void PrintAttributes(IEnumerable<Attribute> attributes)
	{
		var attrList = attributes.ToList();
		if (!attrList.Any()) return;
		
		Console.WriteLine("	Атрибуты:");
		foreach (var attr in attrList)
		{
			Console.WriteLine($"		{attr.GetType().Name}");
			
			if (attr is DisplayNameAttribute dna)
			{
				Console.WriteLine($"		Отображаемое имя: {dna.DisplayName}");
			}
			else if (attr is VersionAttribute va)
			{
				Console.WriteLine($"		Версия: {va.Major}.{va.Minor}");
			}
		}
	}

	private static void PrintConstructors(Type type)
	{
		var constructors = type.GetConstructors(
			BindingFlags.Public | 
			BindingFlags.Instance | 
			BindingFlags.Static | 
			BindingFlags.NonPublic);
		
		if (!constructors.Any()) return;
		
		Console.WriteLine("	Конструкторы:");
		foreach (var ctor in constructors)
		{
			var parameters = ctor.GetParameters();
			var paramString = parameters.Any() 
				? string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}")) 
				: "";
			
			Console.WriteLine($"		{ctor.Name}({paramString})");
		}
	}

	private static void PrintMethods(Type type)
	{
		var methods = type.GetMethods(
			BindingFlags.Public | 
			BindingFlags.Instance | 
			BindingFlags.Static | 
			BindingFlags.DeclaredOnly | 
			BindingFlags.NonPublic);
		
		if (!methods.Any()) return;
		
		Console.WriteLine("	Методы:");
		foreach (var method in methods)
		{
			if (method.IsSpecialName) continue;
			
			var parameters = method.GetParameters();
			var paramString = parameters.Any() 
				? string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}")) 
				: "";
			
			var displayAttr = method.GetCustomAttribute<DisplayNameAttribute>();
			var displayName = displayAttr != null 
				? $" [{displayAttr.DisplayName}]" 
				: "";
			
			Console.WriteLine($"		{method.ReturnType.Name} {method.Name}({paramString}){displayName}");
		}
	}
}
