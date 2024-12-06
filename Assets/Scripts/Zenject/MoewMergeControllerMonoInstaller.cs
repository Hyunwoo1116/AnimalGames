using MoewMerge.Managers;
using MoewMerge.Managers.Interfaces;
using MoewMerge.UI.Controller.GameEnd;
using MoewMerge.UI.Controller.GameEnd.Interfaces;
using UnityEngine;
using Zenject;

namespace MoewMerge.Zenject
{
    public class MoewMergeControllerMonoInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameEndController>().To<GameEndController>().FromComponentInHierarchy().AsCached();
        }
    }
}
