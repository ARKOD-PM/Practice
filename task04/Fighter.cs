public class Fighter : ISpaceship
{
	public void MoveForward()
	{
		Console.WriteLine($"Истребитель движется со скоростью {Speed} единиц");
	}

	public void Rotate(int angle)
	{
		Console.WriteLine($"Истребитель поворачивается на {angle} градусов");
	}

	public void Fire()
	{
		Console.WriteLine($"Истребитель стреляет ракетой мощностью {FirePower} единиц");
	}
 
	public int Speed => 100;
	public int FirePower => 50;
}
