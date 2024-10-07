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
            Container.Bind<ISoundConfigManager>().To<SoundManager>().FromComponentInHierarchy().AsCached();
        }
    }
}
