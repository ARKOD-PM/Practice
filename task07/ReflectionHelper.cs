using System.Reflection;

public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var displayNameAttr = type.GetCustomAttribute<DisplayNameAttribute>();
        if (displayNameAttr != null)
        {
            Console.WriteLine($"Отображаемое имя класса: {displayNameAttr.DisplayName}");
        }

        var versionAttr = type.GetCustomAttribute<VersionAttribute>();
        if (versionAttr != null)
        {
            Console.WriteLine($"Версия класса: {versionAttr.Major}.{versionAttr.Minor}");
        }

        Console.WriteLine("Методы:");
        foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            var attr = method.GetCustomAttribute<DisplayNameAttribute>();
            Console.WriteLine($"  {attr?.DisplayName ?? method.Name}");
        }

        Console.WriteLine("Свойства:");
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            var attr = prop.GetCustomAttribute<DisplayNameAttribute>();
            Console.WriteLine($"  {attr?.DisplayName ?? prop.Name}");
        }
    }
}
