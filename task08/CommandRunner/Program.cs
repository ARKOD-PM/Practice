using System.Reflection;
using FileSystemCommands;
using CommandLib;

namespace CommandRunner
{
    class Program
    {
        static void Main()
        {
            var assembly = Assembly.LoadFrom("FileSystemCommands.dll");
            
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(ICommand).IsAssignableFrom(type) && !type.IsInterface)
                {
                    Console.WriteLine($"Вызывается следующая команда: {type.Name}");
                    
                    ICommand command = CreateCommandInstance(type);
                    
                    command.Execute();
                    
                    PrintCommandResults(command);
                }
            }
        }

        private static ICommand CreateCommandInstance(Type commandType)
        {
            var constructors = commandType.GetConstructors();
            var parameters = constructors[0].GetParameters();
            var arguments = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Console.Write($"Введите {parameters[i].Name}: ");
                arguments[i] = Console.ReadLine();
            }

            return (ICommand)Activator.CreateInstance(commandType, arguments);
        }

        private static void PrintCommandResults(ICommand command)
        {
            switch (command)
            {
                case DirectorySizeCommand sizeCmd:
                    Console.WriteLine($"Итоговый размер: {sizeCmd.TotalSize} байт");
                    break;
                
                case FindFilesCommand filesCmd:
                    Console.WriteLine("Найдены следующие файлы:");
                    foreach (var file in filesCmd.FoundFiles)
                    {
                        Console.WriteLine($"- {file}");
                    }
                    break;
            }
            Console.WriteLine();
        }
    }
}
