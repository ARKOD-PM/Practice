using Xunit;

public class StudentTests
{
	[Fact]
	public void SerializeDeserializeTest()
	{
		var student = new Student
		{
			FirstName = "Иван",
			LastName = "Иванов",
			BirthDate = new DateTime(1991, 8, 19),
			Grades = new List<Subject>
			{
				new Subject { Name = "Филология", Grade = 95 },
				new Subject { Name = "ОРГ", Grade = 10 }
			}
		};

		string json = JsonHelper.Serialize(student);

		var deserialized = JsonHelper.Deserialize(json);

		Assert.Equal(student.FirstName, deserialized.FirstName);
		Assert.Equal(student.LastName, deserialized.LastName);
		Assert.Equal(student.BirthDate.Date, deserialized.BirthDate.Date);
		Assert.Equal(student.Grades.Count, deserialized.Grades.Count);
		for (int i = 0; i < student.Grades.Count; i++)
		{
			Assert.Equal(student.Grades[i].Name, deserialized.Grades[i].Name);
			Assert.Equal(student.Grades[i].Grade, deserialized.Grades[i].Grade);
		}
	}

	[Fact]
	public void SaveLoadTest()
	{
		var student = new Student
		{
			FirstName = "Товарищ",
			LastName = "Сталин",
			BirthDate = new DateTime(1878, 12, 6),
			Grades = new List<Subject>
			{
				new Subject { Name = "История", Grade = 100 }
			}
		};
		string filePath = Path.Combine(Path.GetTempPath(), "student.json");

		JsonHelper.SaveToFile(filePath, student);
		var loadedStudent = JsonHelper.LoadFromFile(filePath);

		Assert.Equal(student.FirstName, loadedStudent.FirstName);
		Assert.Equal(student.LastName, loadedStudent.LastName);
		Assert.Equal(student.BirthDate.Date, loadedStudent.BirthDate.Date);
		Assert.Single(loadedStudent.Grades);
		Assert.Equal("История", loadedStudent.Grades[0].Name);
		Assert.Equal(100, loadedStudent.Grades[0].Grade);

		File.Delete(filePath);
	}
}
