using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine;

public class ScreenCaptureManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator OnCapturedNativeGallery(Action<Texture2D> callbackEvent)
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        callbackEvent.Invoke(texture);

/*
        byte[] bytes = texture.EncodeToJPG();
        NativeGallery.SaveImageToGallery(bytes, "AnimalGames", DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".jpg");
        DestroyImmediate(texture);*/
    }/*
    public IEnumerator StartCapture()
    {
        yield return new WaitForEndOfFrame();

        string Directorypath = Path.Combine(Application.persistentDataPath, $"Capture");
        string path = Path.Combine(Directorypath, $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}.png");
        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        byte[] bytes = texture.EncodeToJPG();
        if (!Directory.Exists(Directorypath))
            Directory.CreateDirectory(Directorypath);
        File.WriteAllBytes(path, bytes);
        DestroyImmediate(texture);
        Debug.Log(path);
    }*/

}
