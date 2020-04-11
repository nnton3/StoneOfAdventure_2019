using StoneOfAdventure.Combat;
using Zenject;

public class BloodShard : ObjectChasingPlayer
{
    [Inject(Id = "Player")] private Health health;

    protected override void PlayerGetObject()
    {
        health.Heal(1);
        base.PlayerGetObject();
    }
}
