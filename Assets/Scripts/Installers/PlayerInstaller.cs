using PathCreation;
using Player;
using UnityEngine;
using Utilities;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInteraction _player;
        [SerializeField] private PathCreator _pathCreator;

        public override void InstallBindings()
        {
            Container.Bind<PathCreator>().FromInstance(_pathCreator);
            var playerInstance = Container.InstantiatePrefabForComponent<PlayerInteraction>(_player, _pathCreator.path.GetPointAtTime(0), Quaternion.identity, null);
            Container.Bind<PlayerInteraction>().FromInstance(playerInstance).AsSingle();
            Container.Bind<IWallet>().FromInstance(playerInstance.Wallet).AsSingle();
        }
    }
}