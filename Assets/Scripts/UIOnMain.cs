using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class UIOnMain : MonoBehaviour
{

    public TextMeshProUGUI timeLeftText;
    private StageManager stageManager;


    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {
        timeLeftText.text = "Time Left: " + stageManager.timer.ToString("F1");
    }
}
