using MoewMerge.Cat.Model;

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

    public static CatLevel GetMoveBefore(this CatLevel source)
    {
        var array = System.Enum.GetValues(typeof(CatLevel));
        for(int i = 0; i < array.Length; i++)
        {
            if (source.Equals(array.GetValue(i)))
                return (CatLevel)array.GetValue(i - 1);
        }
        return (CatLevel)array.GetValue(array.Length - 1);
    }
}
