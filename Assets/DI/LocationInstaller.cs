using UnityEngine;
using Zenject;

namespace Common.Infrastracture
{
    public class LocationInstaller : MonoInstaller
    {
        public MovementParameters MovementSettings;
        public WorldParameters WorldParameters;
        public Transform StartPoint;
        public GameObject PlayerPrefab;
        public Tile Tile;
        public InteractableObject[] ObstaclesBoostsCookies;
        public RoadLine[] LinesInGame;

        public override void InstallBindings()
        {
            BindMovementSettings();
            BindEnvironmentSettings();
            BindLines();
            BindObstaclesAndCookiesPrefabs();
            BindTiles();
            BindPlayer();
        }

        private void BindEnvironmentSettings()
        {
            Container
                .Bind<WorldParameters>()
                .FromInstance(WorldParameters)
                .AsSingle();
        }
        private void BindMovementSettings()
        {
            Container
                .Bind<MovementParameters>()
                .FromInstance(MovementSettings)
                .AsSingle();
        }

        private void BindPlayer()
        {
            Player player = Container
                .InstantiatePrefabForComponent<Player>(PlayerPrefab, StartPoint.position, Quaternion.identity, null);
            Container
                .Bind<Player>()
                .FromInstance(player)
                .AsSingle();
        }

        private void BindObstaclesAndCookiesPrefabs()
        {
            Container
                .Bind<InteractableObject[]>()
                .FromInstance(ObstaclesBoostsCookies)
                .AsSingle();
        }

        private void BindLines()
        {
            Container
                .Bind<RoadLine[]>()
                .FromInstance(LinesInGame)
                .AsSingle();
        }

        private void BindTiles()
        {
            Container
                .Bind<Tile>()
                .FromInstance(Tile)
                .AsSingle();
        }
    }
}

