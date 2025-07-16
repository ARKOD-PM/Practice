using CommandLib;

namespace FileSystemCommands;

public class FindFilesCommand : ICommand
{
	public string[] FoundFiles { get; private set; }
	private readonly string _directoryPath;
	private readonly string _searchPattern;

	public FindFilesCommand(string directoryPath, string searchPattern)
	{
		_directoryPath = directoryPath;
		_searchPattern = searchPattern;
	}

	public void Execute()
	{
		if (!Directory.Exists(_directoryPath))
			throw new DirectoryNotFoundException($"Следующая директория не работает: {_directoryPath}");

		FoundFiles = Directory.GetFiles(_directoryPath, _searchPattern, SearchOption.TopDirectoryOnly);
	}
}
