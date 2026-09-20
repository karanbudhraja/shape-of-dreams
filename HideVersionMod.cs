using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HideVersionMod
{
    public class ModEntry
    {
        public void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            HideVersionUI();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            HideVersionUI();
        }

        private void HideVersionUI()
        {
            // This searches for the component by text name after the game boots up
            Type targetType = Type.GetType("UI_ApplicationVersion, Assembly-CSharp");
            
            if (targetType != null)
            {
                var versionComponent = UnityEngine.Object.FindAnyObjectByType(targetType) as Component;
                
                if (versionComponent != null)
                {
                    versionComponent.gameObject.SetActive(false);
                }
            }
        }
    }
}
