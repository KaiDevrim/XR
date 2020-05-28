using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    void FixedUpdate()
    { 
        // only let the local player control the racket.
        // don't control other player's rackets
        if (isLocalPlayer)
        {
            Debug.Log("You are the local player!");
        }
        else
        {
            Debug.Log("What are you even doing?");
        }
            
    }
}
