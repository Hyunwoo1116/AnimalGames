using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundManager 
{
    void SetEffectVolume(float volume);
    void SetBackgroundVolume(float volume);
    float GetEffectVolume();
    float GetBackgroundVolume();

}
