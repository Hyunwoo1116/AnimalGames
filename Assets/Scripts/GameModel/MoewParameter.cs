using MoewMerge.Localization.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoewMerge.GameModel
{
    [Serializable]
    public class MoewParameter 
    {
        public int BestScore = 0;
        public string Locale = "ko";
        public bool BackgroundSound = true;
        public bool EffectSound = true;
        public bool Vibrate = true;

        [field:NonSerialized]
        public Locale GetLoacle
        {
            get
            {
                return (Locale)Enum.Parse(typeof(Locale), Locale);
            }
        }
    }

}

