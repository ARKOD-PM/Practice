public static class StringExtensions
{
	public static bool IsPalindrome(this string input)
	{
		var modifiedInput = input
			.Where(x => !char.IsWhiteSpace(x) && !char.IsPunctuation(x))
			.Select(char.ToLowerInvariant)
			.ToArray();

		return modifiedInput.Length > 0 && modifiedInput.SequenceEqual(modifiedInput.Reverse());
	}
}
