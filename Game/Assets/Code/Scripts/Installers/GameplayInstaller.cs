using Input;
using Zenject;

namespace Installers
{
    public class GameEssentialsInstaller  : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputManager();
        }
        
        private void BindInputManager()
        {
            Container
                .BindInterfacesTo<DesktopInput>()
                .AsSingle()
                .NonLazy();
        }
    }
}