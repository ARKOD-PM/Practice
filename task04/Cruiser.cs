public class Cruiser : ISpaceship
{
	public void MoveForward()
	{
		Console.WriteLine($"Крейсер движется со скоростью {Speed} единиц");
	}

	public void Rotate(int angle)
	{
		Console.WriteLine($"Крейсер поворачивается на {angle} градусов");
	}

	public void Fire()
	{
		Console.WriteLine($"Крейсер стреляет ракетой мощностью {FirePower} единиц");
	}

	public int Speed => 50;
	public int FirePower => 100;
}
