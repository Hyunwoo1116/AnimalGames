using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCreateModel 
{
    public GameObject source;
    public GameObject collisionObject;
    public Vector3 createPosition;
    public CatLevel catLevel;
    public CatCreateModel(GameObject source, GameObject collisionObject, Vector3 createPosition, CatLevel catLevel)
    {
        this.source = source;
        this.collisionObject = collisionObject;
        this.createPosition = createPosition;
        this.catLevel = catLevel;
    }
    

}
