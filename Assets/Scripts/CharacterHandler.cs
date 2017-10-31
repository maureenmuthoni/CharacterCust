﻿using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Character Handler
[AddComponentMenu("Character Set Up/Character Handler")]
public class CharacterHandler : MonoBehaviour 
{
    [Header("Character")]
    #region Character 
    //bool to tell if the player is alive
    public bool alive;
    //connection to players character controller
    public characterController controller;
    #endregion
    [Header("Health")]
    #region Health
    //max and min health
    public float maxHealth, curHealth;
    #endregion
    //[Header("Levels and Exp")]
    #region Level and Exp
    //players current level
    public int level;
    //max and min experience 
    public int maxExp, curExp;
    #endregion
    //[Header("Camera Connection")]
    #region MiniMap
    //render texture for the mini map that we need to connect to a camera
    public RenderTexture miniMap;
    #endregion
    #region Start
    void Start()
    {
        //set max health to 100
        maxHealth = 100F;
        //set current health to max
        curHealth = maxHealth;
        //make sure player is alive
        alive = true;
        //max exp starts at 60
        maxExp = 60;
        //connect the Character Controller to the controller variable
        controller = this.GetComponent<CharacterController>();
    }
    #endregion
    #region Update
    void Update()
    {
        //if our current experience is greater or equal to the maximum experience
        if (curExp >= maxExp)
        {
            //then the current experience is equal to our experience minus the maximum amount of experience
            curExp -= maxExp;
            //our level goes up by one
            level++;
            //the maximum amount of experience is increased by 50
            maxExp += 50;
        }
    }

  
    #endregion
    #region LateUpdate
    void LateUpdate()
    {
        //if our current health is greater than our maximum amount of health
        if (curHealth > maxHealth)
        {
            //then our current health is equal to the max health
            curHealth = maxHealth;
        }

        //if our current health is less than 0 or we are not alive
        if (curHealth < 0 || !alive)
        {
            //current health equals 0
            curHealth = 0;
        }
            //if the player is alive
        if(alive)
        {
            //and our health is less than or equal to 0
            if (curHealth <= 0)
            {
                //alive is false
                alive = false;
                //controller is turned off
                controller.enabled = false;
            }

       
        }
       
    }

    #endregion
    #region OnGUI
    void OnGUI()
    {
        //set up our aspect ratio for the GUI elements
        //scrW - 16
        float scrW = screen.Width /16;
        //scrH - 9
        float scrH = screen.Width /9;
        //GUI Box on screen for the healthbar background
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, 4 * scrW, 0.5f * scrH), "");//background
        //GUI Box for current health that moves in same place as the background bar
       //current Health divided by the posistion on screen and timesed by the total max health                                                                                                                                                                                        
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "");//moving health bar
                                                                                                       //GUI Box on screen for the experience background
        GUI.Box(new Rect(6 * scrW, 0.75f * scrH, 4 * scrW, 0.25f * scrH), "");//background
        //GUI Box for current experience that moves in same place as the background bar
        //current experience divided by the posistion on screen and timesed by the total max experience
        GUI.Box(new Rect(6*scrW))
        //GUI Draw Texture on the screen that has the mini map render texture attached
    }

    #endregion
}









