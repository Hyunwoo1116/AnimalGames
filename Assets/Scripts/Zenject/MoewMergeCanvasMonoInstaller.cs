using MoewMerge.Managers;
using MoewMerge.Managers.Interfaces;
using MoewMerge.UI.Canvas;
using MoewMerge.UI.Canvas.Interfaces;
using MoewMerge.UI.Controller.GameEnd;
using MoewMerge.UI.Controller.GameEnd.Interfaces;
using UnityEngine;
using Zenject;

namespace MoewMerge.Zenject
{
    public class MoewMergeCanvasMonoInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameCanvas>().To<GameCanvas>().FromComponentInHierarchy().AsCached();
        }
    }
}
