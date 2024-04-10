using Input;
using Misc;
using Zenject;

namespace Infrastructure
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
                .Bind<IInput>()
                .To<DesktopInput>()
                .AsSingle()
                .NonLazy();
        }
    }
}