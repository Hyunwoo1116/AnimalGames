
using MoewMerge.Cats.Model;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface IGameManager
    {
        public float GetLeftEndPosition(Vector2 endObjectPosition);
        public float GetRightEndPosition(Vector2 endObjectPosition);
        public void NextCats();
        public void AddGameScore(CatLevel instanceCatLevel);
        public float GetTopPosition();
    }
}

