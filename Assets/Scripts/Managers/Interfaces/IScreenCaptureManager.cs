using System.Threading.Tasks;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface IScreenCaptureManager 
    {
        public Task<Texture2D> GetScreenTexture();
    }
}