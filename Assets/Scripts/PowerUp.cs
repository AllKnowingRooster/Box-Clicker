public abstract class PowerUp : SpawnableObject
{
    public override void OnClick()
    {
        UsePowerUp();
        base.OnClick();
    }
    public abstract void UsePowerUp();
}