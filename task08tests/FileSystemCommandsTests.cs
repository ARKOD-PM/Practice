using Xunit;
using FileSystemCommands;

public class FileSystemCommandsTests : IDisposable
{
	private readonly string _testDir;

	public FileSystemCommandsTests()
	{
		_testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
		Directory.CreateDirectory(_testDir);

		File.WriteAllText(Path.Combine(_testDir, "file1.txt"), "Test content");
		File.WriteAllText(Path.Combine(_testDir, "file2.log"), "Log content");
		Directory.CreateDirectory(Path.Combine(_testDir, "subdir"));
		File.WriteAllText(Path.Combine(_testDir, "subdir", "file3.txt"), "Another file");
	}

	[Fact]
	public void DirectorySizeCommand_ShouldCalculateSize()
	{
		var command = new DirectorySizeCommand(_testDir);
		
		command.Execute();
		
		Assert.True(command.TotalSize > 0);
		Assert.Equal(
			new FileInfo(Path.Combine(_testDir, "file1.txt")).Length +
			new FileInfo(Path.Combine(_testDir, "file2.log")).Length +
			new FileInfo(Path.Combine(_testDir, "subdir", "file3.txt")).Length,
			command.TotalSize
		);
	}

	[Fact]
	public void FindFilesCommand_ShouldFindMatchingFiles()
	{
		var command = new FindFilesCommand(_testDir, "*.txt");
		
		command.Execute();
		
		Assert.Single(command.FoundFiles);
		Assert.Contains(Path.Combine(_testDir, "file1.txt"), command.FoundFiles);
		Assert.DoesNotContain(Path.Combine(_testDir, "file2.log"), command.FoundFiles);
	}

	public void Dispose()
	{
		if (Directory.Exists(_testDir))
		{
			Directory.Delete(_testDir, true);
		}
	}
}
