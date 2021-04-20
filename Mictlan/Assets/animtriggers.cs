using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animtriggers : MonoBehaviour
{

    public Animator movement;

    void Animfinish()
    {
        movement.SetBool("Moving", false);
        movement.SetBool("Charging", false);
        movement.SetBool("Draining", false);
    }
}
