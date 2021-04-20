using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Animator anim;
    public Animator anim2;
    public Animator anim3;

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Rock" || col.gameObject.tag == "Player")
        {
            SoundManager.playRumble();
            anim.SetBool("ExitOpen", true);
            anim3.SetBool("Pressed", true);
        }
    }
    void OnTriggerExit(Collider coll)
    {
        SoundManager.playRumble();
        anim.SetBool("ExitOpen", false);
        anim3.SetBool("Pressed", false);
    }
}
