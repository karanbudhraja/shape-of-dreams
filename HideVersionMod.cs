using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HideVersionMod
{
    // Inheriting from ModBehaviour automatically attaches this script to the game engine layout
    public class ModEntry : ModBehaviour
    {
        private void Awake()
        {
            Debug.Log("Hiding version number watermark overlay: " + mod.metadata.id);
            
            // Listen for scene switches to continuously verify the watermark stays hidden
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            // Run the hide logic immediately on boot layout
            HideVersionUI();
        }

        private void OnDestroy()
        {
            // Clean up scene references to support live reload cleanly
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Debug.Log("Restoring version number layout hooks: " + mod.metadata.id);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            HideVersionUI();
        }

        private void HideVersionUI()
        {
            // Your excellent reflection code targeting the application component
            Type targetType = Type.GetType("UI_ApplicationVersion, Assembly-CSharp");
            
            if (targetType != null)
            {
                // Unity 2021 uses FindObjectOfType (FindAnyObjectByType requires Unity 2022+)
                var versionComponent = UnityEngine.Object.FindObjectOfType(targetType) as Component;
                
                if (versionComponent != null)
                {
                    versionComponent.gameObject.SetActive(false);
                }
            }
        }
    }
}
