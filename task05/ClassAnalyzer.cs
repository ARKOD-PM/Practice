using System.Reflection;

public class ClassAnalyzer
{
	private Type _type;

	public ClassAnalyzer(Type type)
	{
		_type = type;
	}

	// 1. Список публичных методов
	public IEnumerable<string> GetPublicMethods()
	{
		return _type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
			.Where(m => !m.IsSpecialName)
			.Select(m => m.Name)
			.Distinct();
	}

	// 2. Список параметров имён параметров и возвращаемого значения публичного метода
	public IEnumerable<string> GetMethodParams(string methodName)
	{
		var method = _type.GetMethods()
			.FirstOrDefault(m => m.Name == methodName && !m.IsSpecialName);
		
		var parameters = method.GetParameters()
			.Select(p => $"{p.ParameterType.Name} {p.Name}");
		
		return parameters.Append($"Возвращает: {method.ReturnType.Name}");
	}

	// 3. Список имён полей (включая приватные)
	public IEnumerable<string> GetAllFields()
	{
		return _type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
			.Select(f => f.Name);
	}

	// 4. Список имён свойств
	public IEnumerable<string> GetProperties()
	{
		return _type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
			.Select(p => p.Name);
	}

	// 5. Наличие атрибута указанного типа у класса
	public bool HasAttribute<T>() where T : Attribute
	{
		return _type.GetCustomAttributes(typeof(T), false).Any();
	}
}
