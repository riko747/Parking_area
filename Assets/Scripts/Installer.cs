using Assets.Scripts.ParkingLogic;
using Zenject;

namespace Assets.Scripts
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings() => Container.Bind<IParking>().To<Parking>().FromComponentInHierarchy().AsSingle();
    }
}