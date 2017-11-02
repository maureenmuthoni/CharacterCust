using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Interact
[AddComponentMenu("Character Set Up/Interact")]
public class PickUp : MonoBehaviour 
{
    #region Variables
    //We are setting up these variable and the tags in update for week 3,4 and 5
    [Header("Player and Camera connection")]
    //create two gameobject variables one called player and the other mainCam
    public GameObject Player;
    public GameObject mainCam;
        #endregion
	#region Start
    void Start()
    {
        //connect our player to the player variable via tag
        Player = GameObject.FindGameObjectWithTag("Player");
        //connect our Camera to the mainCam variable via tag
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    #endregion
    #region Update
    void update()
    {
        //if our interact key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            //create a ray
            Ray Interact;
            //this ray is shooting out from the main cameras screen point center of screen
            Interact = Camera.main.screenPointToRay(new vector2(Sreen.width / 2, screen.height / 2));
            //create hit info
            RaycastHit hitinfo;
            //if this physics raycast hits something within 10 units
            if(Physics.Raycast(Interact,out hitinfo, 10))
            {
                #region NPC tag
                //and that hits info is tagged NPC
                if (hitinfo.collider.compareTag("NPC"))
                {
                    //Debug that we hit a NPC
                    Debug.Log("Hitthe NPC");
                }
                #endregion
                #region Item
                //and that hits info is tagged Item
                if (hitinfo.collider.CompareTag("Item"))
                {

                    //Debug that we hit an Item
                    Debug.Log("Hit an Item");

                }
                #endregion

            }

        }
    }

    
    #endregion
}






