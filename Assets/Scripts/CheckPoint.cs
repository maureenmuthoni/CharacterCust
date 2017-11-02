using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//CheckPoint
[AddComponentMenu("Character Set Up/CheckPoint")]
public class CheckPoint : MonoBehaviour
{
    #region Variables
    [Header("Check Point Elements")]
    //GameObject for our currentCheck
    [Header("Character Handler")]
    //character handler script that holds the players health
    public CharacterHandler charH;
    #endregion
    #region Start
    void Start()
    {
        //the character handler is the component attached to our player
        charH = this.GetComponent<CharacterHandler>();

        #region Check if we have Key
        //if we have a save key called SpawnPoint
        if (PlayerPrefs.HasKey("SpawnPoint"))
        {
            //then our checkpoint is equal to the game object that is named after our save file
            curCheckPoint = GameObject.Find(PlayerPrefs.GetString("SpawnPoint"));
            //our transform.position is equal to that of the checkpoint
            this.transform.position = curCheckPoint.transform.position;
        }


        #endregion
        #endregion

        #region Update
            Void Update()
            {
            //if our characters health is less than or equal to 0
            if (charH.curHealth == 0)
            {
                //our transform.position is equal to that of the checkpoint
                this.transform.position = curCheckPoint.transform.position;
                //our characters health is equal to full health
                charH.curHealth = charH.maxHealth;
                //character is alive
                charH.alive = true;
                //characters controller is active
                CharacterController controller = this.GetComponent<CharacterController>();
                controller.enabled = true;
            }
        }
    }
    #endregion
    #region OnTriggerEnter
    //Collider other
    Void OnTriggerEnter(collider other)
    {
        //if our other objects tag when compared is CheckPoint
        if (other.CompareTag("CheckPoint"))
        {

            //our checkpoint is equal to the other object
            curCheckPoint = other.gameObject;
            //save our SpawnPoint as the name of that object
            playerPrefs.SetString("SpawnPoint", curCheckPoint.name);
        }
    }

    #endregion







}