using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class EnumExtension 
{
    public static CatLevel GetMoveNext(this CatLevel source) 
    {
        var array = System.Enum.GetValues(typeof(CatLevel));
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (source.Equals(array.GetValue(i)))
                return (CatLevel)array.GetValue(i + 1);
        }
        return (CatLevel)array.GetValue(0);
    }
}
