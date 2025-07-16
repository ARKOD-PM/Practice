using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonHelper
{
	public static JsonSerializerOptions GetOptions()
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		};
		options.Converters.Add(new DateTimeConverter());
		return options;
	}

	public static string Serialize(Student student) => 
		JsonSerializer.Serialize(student, GetOptions());

	public static Student Deserialize(string json) => 
		JsonSerializer.Deserialize<Student>(json, GetOptions());

	public static void SaveToFile(string filePath, Student student)
	{
		var json = Serialize(student);
		File.WriteAllText(filePath, json);
	}

	public static Student LoadFromFile(string filePath) => 
		Deserialize(File.ReadAllText(filePath));
}
