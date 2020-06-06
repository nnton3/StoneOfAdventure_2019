using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LocationMissionComplete>();
        Container.DeclareSignal<PlayerStartNextLevel>();
        Container.DeclareSignal<LocationPointsTargetValueUpdated>();
        Container.DeclareSignal<PlayerStartWalk>();
        Container.DeclareSignal<PlayerStopWalk>();
        Container.DeclareSignal<BrokenClockTriggered>();
        Container.DeclareSignal<ArtifactSelected>();
        Container.DeclareSignal<LevelUp>();
    }
}
