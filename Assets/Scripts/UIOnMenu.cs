using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class UIOnMenu : MonoBehaviour
{

    public Button startButton;
    public Button exitButton;

    void Start()
    {
        
    }

    // Update is called once per fame
    void Update()
    {
        
    }

    void Quit()
    {
        
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
