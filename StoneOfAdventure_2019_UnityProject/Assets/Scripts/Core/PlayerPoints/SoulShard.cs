using Zenject;

public class SoulShard : ObjectChasingPlayer
{
    [Inject] private Treasury treasury;
    [Inject] private LocationPointsStorage locationPoints;

    protected override void PlayerGetObject()
    {
        treasury.Refill(1);
        locationPoints.AddPoints(1);
        base.PlayerGetObject();
    }
}
