﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("NPC/Dialogue")]
public class Dialogue : MonoBehaviour
{
    [Header("Dialogue")]
    public int index, optionIndex;
    public bool showDlg;
    public GameObject player;
    public MouseLook mainCam;
    [Header("Dialogue")]
    public string npcName;
    public string[] text;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectsWithTag("MainCamera").GetComponent<MouseLook>));
    }
    #region OnGUI
    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        GUI.Box(new Rect(0, 6 * scrH, Screen.width, 3 * scrH), text[index]);
        if (!(index + 1 >= text.Length || index == optionIndex))
        {
            if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Next"))
            {
                index++;
            }
            else if (index == optionIndex)
            {
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Accept"))
                {
                    index++;
                }
                if (GUI.Button(new Rect(14 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Decline"))
                {
                    index = text.Length-1;
                }
            }
            else
            {
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Bye"))
                {
                    index = 0;
                    showDlg = false;
                    player.GetComponent<Movement>().enabled = true;
                    mainCam.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
}

#endregion
