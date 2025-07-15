using Xunit;

public class StudentServiceTests
{
    private List<Student> _testStudents;
    private StudentService _service;

    public StudentServiceTests()
    {
        _testStudents = new List<Student>
        {
            new() { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } }
        };
        _service = new StudentService(_testStudents);
    }

    // Найти всех студентов указанного факультета. 
    [Fact]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();
        Assert.Equal(2, result.Count);
        Assert.True(result.All(s => s.Faculty == "ФИТ"));
    }
    
    // Получить студентов с средним баллом выше заданного.
    [Fact]
    public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsWithMinAverageGrade(4.7).ToList();
        Assert.Single(result);
        Assert.Equal("Петр", result[0].Name);
    }

    // Отсортировать студентов по имени в алфавитном порядке.
    [Fact]
    public void GetStudentsOrderedByName_ReturnsOrderedList()
    {
        var result = _service.GetStudentsOrderedByName().ToList();
        Assert.Equal("Анна", result[0].Name);
        Assert.Equal("Иван", result[1].Name);
        Assert.Equal("Петр", result[2].Name);
    }

    // Сгруппировать студентов по факультету.
    [Fact]
    public void GroupStudentsByFaculty_ReturnsCorrectGroups()
    {
        var lookup = _service.GroupStudentsByFaculty();
        Assert.Equal(2, lookup["ФИТ"].Count());
        Assert.Single(lookup["Экономика"]);
    }

    // Найти факультет с самым высоким средним баллом.
    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();
        Assert.Equal("Экономика", result);
    }
}
