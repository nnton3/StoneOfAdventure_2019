﻿using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LocationCompletedSignal>();
        Container.DeclareSignal<PlayerStartNextLevel>();
        Container.DeclareSignal<LocationPointsUpdated>();
        Container.DeclareSignal<LocationPointsTargetValueUpdated>();
    }
}
