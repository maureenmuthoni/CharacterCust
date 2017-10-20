using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CustomizationGet : MonoBehaviour
{

    //[Header("character")]
    //public variable for the skinned Mesh Renderer which is our character reference
    public Renderer character;

    #region Start

    // Use this for initialization
    void Start()
    {
        //our chaeracter reference connected to the Skinneds Mesh Renderer via finding the Mesh
        //Run the function LoadTexture
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        LoadTexture();
    }
    public void LoadTexture()
    {
        //check to see if PlayerPrefs (our save location) Haskey (has a save file...you will need to reference the name of a file)
        if (!PlayerPrefs.HasKey("CharacterName"))
        {
            //if it doesnt then load custom set level
            SceneManager.LoadScene(0);
        }
        //if it does have a save file then load and setTexture Skin,Hair, Mouth and Eyes from PlayerPrefs

        SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
        //Grab the game object in scene that is our character and set its Object name to the character name
        gameObject.name = PlayerPrefs.GetString("CharacterName");
    }

    //create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing

    public void SetTexture(string type, int dir)

    {
        //we need variables that exist only within this function
        //these are int material index and Texture2D array of textures
        Texture2D tex = null;
        int matIndex = 0;

        //inside a  switch statement that is swapped by the string name of our material
        //case skin
        switch (type)
        {
            case"Skin":

                // textures is our Resource.Load character Skin save index we loaded in set our Texture2D
                tex = Resources.Load("Character/Skin_" + dir.ToString()) as Texture2D;
                //material index element number is 1
                matIndex = 1;
                //break
                break;
            //hair is 2 
            case "Hair":
                tex = Resources.Load("Character/Hair_" + dir.ToString()) as Texture2D;
                //material index element number is 1
                matIndex = 1;
                //break
                break;

            //mouth is 3

            case "Mouth":
                tex = Resources.Load("Character/Mouth_" + dir.ToString()) as Texture2D;
                //material index element number is 3
                matIndex = 3;
                //break
                break;
            case "Eyes":
                tex = Resources.Load("Character/Eyes_" + dir.ToString()) as Texture2D;
                //material index element number is 4
                matIndex = 4;
                //break
                break;
            case "Armour":
                tex = Resources.Load("Character/Armour_" + dir.ToString()) as Texture2D;
                //material index element number is 4
                matIndex = 5;
                //break
                break;

            case "Clothes":
                tex = Resources.Load("Character/Clothes_" + dir.ToString()) as Texture2D;
                //material index element number is 4
                matIndex = 6;
                //break
                break;


        }

        //Material array is equal to our characters material list
        Material[] mats = character.materials;
        //our materials arrays current material index's main texture is equal to our texture arrays current index
        mats[matIndex].mainTexture = tex;
        //our characters materials are equal to the material array
        character.materials = mats;
    }
}

	
	


#endregion