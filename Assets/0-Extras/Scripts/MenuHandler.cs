using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Menu
{
    public class MenuHandler : MonoBehaviour
    {
        public void LoadScene(int menuID)
        {
            SceneManager.LoadScene(menuID);
        }
        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    } 
}
