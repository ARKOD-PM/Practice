using System.Threading;

public class DefiniteIntegral
{
	public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
	{
		if (threadsnumber <= 0)
			throw new ArgumentException("Количество потоков должно быть положительно.", nameof(threadsnumber));
		if (step <= 0)
			throw new ArgumentException("Количесво шагов должно быть положительно.", nameof(step));
		if (a >= b)
			return 0.0;

		double total = 0.0;
		double segmentLength = (b - a) / threadsnumber;

		using (Barrier barrier = new Barrier(threadsnumber + 1))
		{
			Thread[] threads = new Thread[threadsnumber];
			for (int i = 0; i < threadsnumber; i++)
			{
				double startSeg = a + i * segmentLength;
				double endSeg = (i == threadsnumber - 1) ? b : startSeg + segmentLength;
				
				threads[i] = new Thread(() =>
				{
					double localSum = CalculateSegment(startSeg, endSeg, function, step);
					
					double initialValue, newValue;
					do
					{
						initialValue = total;
						newValue = initialValue + localSum;
					} 
					while (initialValue != Interlocked.CompareExchange(ref total, newValue, initialValue));
					
					barrier.SignalAndWait();
				});
				threads[i].Start();
			}
			
			barrier.SignalAndWait();
			return total;
		}
	}

	private static double CalculateSegment(double a, double b, Func<double, double> function, double step)
	{
		double sum = 0.0;
		double current = a;
		while (current < b)
		{
			double next = Math.Min(current + step, b);
			double delta = next - current;
			sum += (function(current) + function(next)) * delta / 2.0;
			current = next;
		}
		return sum;
	}
}
