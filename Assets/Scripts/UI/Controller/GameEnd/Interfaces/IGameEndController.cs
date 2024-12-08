using UnityEngine;

namespace MoewMerge.UI.Controller.GameEnd.Interfaces
{
    public interface IGameEndController
    {
        public void SetResultTexture(Texture2D texture);
        public void Show();
    }
}