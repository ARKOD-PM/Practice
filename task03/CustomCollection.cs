using System.Collections;

public class CustomCollection<T> : IEnumerable<T>
{
	private List<T> _items = new();

	public void Add(T item) => _items.Add(item);
	public bool Remove(T item) => _items.Remove(item);
	public int Count => _items.Count;
	public void Clear() => _items.Clear();
	
	// Итератор для прямого обхода коллекции
	public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	
	// Итератор для обратного обхода коллекции
	public IEnumerable<T> GetReverseEnumerator()
	{
		for (int i = _items.Count - 1; i >= 0; i--)
		{
			yield return _items[i];
		}
	}
	
	// Генерация последовательности
	public static IEnumerable<int> GenerateSequence(int start, int count)
	{
		for (int i = 0; i < count; i++)
		{
			yield return start + i;
		}
	}
	
	// Фильтрация и сортировка элементов через LINQ
	public IEnumerable<T> FilterAndSort(Func<T, bool> predicate, Func<T, IComparable> keySelector)
	{
		return _items
			.Where(predicate)
			.OrderBy(keySelector);
	}
}
