using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
	public long TotalSize { get; private set; }
	private readonly string _directoryPath;

	public DirectorySizeCommand(string directoryPath)
	{
		_directoryPath = directoryPath;
	}

	public void Execute()
	{
		if (!Directory.Exists(_directoryPath))
			throw new DirectoryNotFoundException($"Следующая директория не найдена: {_directoryPath}");

		TotalSize = CalculateDirectorySize(_directoryPath);
	}

	private long CalculateDirectorySize(string path)
	{
		long size = 0;
		foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
		{
			size += new FileInfo(file).Length;
		}
		return size;
	}
}
