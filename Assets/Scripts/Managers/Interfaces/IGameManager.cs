using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager 
{
    public float GetLeftEndPosition(Vector2 endObjectPosition);
    public float GetRightEndPosition(Vector2 endObjectPosition);
    public void NextCats();
    public void AddGameScore(CatLevel instanceCatLevel);
}
