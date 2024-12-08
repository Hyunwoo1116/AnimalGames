using MoewMerge.Managers.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;

namespace MoewMerge.Managers
{
    public class ScreenCaptureManager : MonoBehaviour, IScreenCaptureManager
    {

        bool isCompleted;
        Texture2D currentTexture;
        // Start is called before the first frame update
        

        public IEnumerator OnCapturedNativeGallery()
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture = new Texture2D(Screen.width, Screen.height);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            currentTexture = texture;
            isCompleted = true;
        }

        public async Task<Texture2D> GetScreenTexture()
        {
            isCompleted = false;
            StartCoroutine(OnCapturedNativeGallery());

            while(!isCompleted)
            {
                await Task.Yield();
            }

            return currentTexture;
        }
    }
}
