using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animtriggers : MonoBehaviour
{

    public Animator movement;
    public Animator dogmove;

    void Animfinish()
    {
        movement.SetBool("Moving", false);
        movement.SetBool("Charging", false);
        movement.SetBool("Draining", false);
        dogmove.SetBool("DogWalk", false);
    }
}
