using MoewMerge.Managers;
using UnityEngine;

namespace MoewMerge.UI.Page.Settings
{
    public class SettingsPage : MonoBehaviour
    {

        private void OnEnable()
        {
            Time.timeScale = 0f;
        }
        private void OnDisable()
        {
            GameManager.Instance.SaveGameData();
            Time.timeScale = 1f;
        }


    }

}

