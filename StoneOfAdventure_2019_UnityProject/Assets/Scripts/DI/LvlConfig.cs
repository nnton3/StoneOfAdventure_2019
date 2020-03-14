using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

[CreateAssetMenu(fileName = "LvlConfig", menuName = "Installers/LvlConfig")]
public class LvlConfig : ScriptableObjectInstaller
{
    [SerializeField] private EnemieSpawnerConfig spawnerConfig;
    [SerializeField] private TileBase groundTile;

    public override void InstallBindings()
    {
        Container.BindInstance(spawnerConfig);
        Container.BindInstance(groundTile);
    }
}