using Xunit;
using Moq;

public class SpaceshipTests
{
	[Fact]
	public void Cruiser_ShouldHaveCorrectStats()
	{
		ISpaceship cruiser = new Cruiser();
		
		Assert.Equal(50, cruiser.Speed);
		Assert.Equal(100, cruiser.FirePower);
	}

	[Fact]
	public void Fighter_ShouldBeFasterThanCruiser()
	{
		var fighter = new Fighter();
		var cruiser = new Cruiser();
		
		Assert.True(fighter.Speed > cruiser.Speed);
	}

	[Fact]
	public void Fighter_ShouldHaveWeakerFirepower()
	{
		var fighter = new Fighter();
		var cruiser = new Cruiser();
		
		Assert.True(fighter.FirePower < cruiser.FirePower);
	}

	[Fact]
	public void Cruiser_MethodsShouldBeCallable()
	{
		var cruiser = new Cruiser();
		
		cruiser.MoveForward();
		cruiser.Rotate(45);
		cruiser.Fire();
		
		Assert.True(true);
	}

	[Fact]
	public void Fighter_MethodsShouldBeCallable()
	{
		var fighter = new Fighter();
		
		fighter.MoveForward();
		fighter.Rotate(-30);
		fighter.Fire();
		
		Assert.True(true);
	}

	[Fact]
	public void Spaceship_InterfaceShouldWorkPolymorphically()
	{
		var ships = new ISpaceship[] { new Cruiser(), new Fighter() };
		var moveCount = 0;
		var rotateCount = 0;
		var fireCount = 0;

		foreach (var ship in ships)
		{
			ship.MoveForward();
			moveCount++;
			
			ship.Rotate(90);
			rotateCount++;
			
			ship.Fire();
			fireCount++;
		}

		Assert.Equal(2, moveCount);
		Assert.Equal(2, rotateCount);
		Assert.Equal(2, fireCount);
	}

	[Fact]
	public void MockSpaceship_ShouldImplementInterface()
	{
		var mockShip = new Mock<ISpaceship>();
		mockShip.Setup(s => s.Speed).Returns(75);
		mockShip.Setup(s => s.FirePower).Returns(80);

		var speed = mockShip.Object.Speed;
		var firePower = mockShip.Object.FirePower;

		Assert.Equal(75, speed);
		Assert.Equal(80, firePower);
	}
}
