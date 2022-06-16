using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int health;
    public int movement;

    public bool moving = false;
    
    public void ReduceMovement()
    {
        movement -= 1;
    }
}
