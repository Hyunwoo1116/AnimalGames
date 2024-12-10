using MoewMerge.Managers;
using MoewMerge.Managers.Interfaces;
using UnityEngine;
using Zenject;

namespace MoewMerge.Zenject
{
    public class MoewMergeManagerMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISoundConfigManager>().To<GameManager>().FromComponentInHierarchy().AsCached();
            Container.Bind<ILanguageManager>().To<LanguageManager>().FromComponentInHierarchy().AsCached();
            Container.Bind<IScreenCaptureManager>().To<ScreenCaptureManager>().FromComponentInHierarchy().AsCached();
            Container.Bind<IGameManager>().To<GameManager>().FromComponentInHierarchy().AsCached();
        }
    }
}
