using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject exit;
    public Transform target;
    public Transform target2;
    public float speed;
    bool activated = false;

    public Animator ExitOpen;

    private void FixedUpdate()
    {
        Vector3 a = exit.transform.position;
        Vector3 b = target.position;
        Vector3 c = target2.position;
        if (activated == true)
        {
            exit.transform.position = Vector3.MoveTowards(a, b, speed);
            ExitOpen.SetBool("ExitOpen", true);
        }
        else if(activated == false)
        {
            exit.transform.position = Vector3.MoveTowards(a, c, speed);
            ExitOpen.SetBool("ExitOpen", false);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Rock" || col.gameObject.tag == "Player")
        {
            SoundManager.playRumble();
            activated = true;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        SoundManager.playRumble();
        activated = false;
    }
}
