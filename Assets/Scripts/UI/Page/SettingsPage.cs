using MoewMerge.Managers.Interfaces;
using UnityEngine;
using Zenject;

namespace MoewMerge.UI.Page.Settings
{
    public class SettingsPage : MonoBehaviour
    {
        [Inject] public IGameManager GameManager;

        private void OnEnable()
        {
            Time.timeScale = 0f;
        }
        private void OnDisable()
        {
            GameManager.SaveGameData();
            Time.timeScale = 1f;
        }


    }

}

