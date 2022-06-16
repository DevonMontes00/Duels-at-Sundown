using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private GameObject [] characters;

    //Class constructor
    public PlayerTeam(GameObject[] characters)
    {
        this.characters = characters;
    }

    //Getter for playerteam
    public GameObject[] GetCharacters()
    {
        return characters;
    }

}
