using UnityEngine;
using Zenject;

namespace Common.Infrastracture
{
    public class LocationInstaller : MonoInstaller
    {
        public UIview _uiView;
        public Transform startPoint;
        public GameObject playerPrefab;
        public GameObject tile;
        public GameObject[] ObstaclesBoostsCookies;
        public Transform[] linesInGame;

        public override void InstallBindings()
        {
            BindLines();
            BindUIview();
            BindObstaclesAndCookiesPrefabs();
            BindTiles();
            BindPlayer();
        }

        private void BindUIview()
        {
            Container
                .Bind<UIview>()
                .FromInstance(_uiView)
                .AsSingle();
        }

        private void BindPlayer()
        {
            Player player = Container
                .InstantiatePrefabForComponent<Player>(playerPrefab, startPoint.position, Quaternion.identity, null);
            Container
                .Bind<Player>()
                .FromInstance(player)
                .AsSingle();
        }

        private void BindObstaclesAndCookiesPrefabs()
        {
            Container
                .Bind<GameObject[]>()
                .FromInstance(ObstaclesBoostsCookies)
                .AsSingle();
        }

        private void BindLines()
        {
            Container
                .Bind<Transform[]>()
                .FromInstance(linesInGame)
                .AsSingle();
        }

        private void BindTiles()
        {
            Container
                .Bind<GameObject>()
                .FromInstance(tile)
                .AsSingle();
        }
    }
}

