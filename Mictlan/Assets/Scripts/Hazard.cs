using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Hazard : MonoBehaviour
{
    public ParticleSystem ps;

    public Animator transition;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("collided");
            Application.LoadLevel(Application.loadedLevel);
            transition.SetTrigger("Start");
            

        }
        else
        {
            ps.Emit(10);
            SoundManager.playGrass();
        }
    }
}
