using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip rockSound;
    public static AudioClip stepSound;
    public static AudioClip flowerSound;
    public static AudioClip grassSound;
    public static AudioClip rumbleSound;
    //public static AudioClip morphSound;
    static AudioSource audioSrc;


    void Start()
    {
        stepSound = Resources.Load<AudioClip>("Steps");
        rockSound = Resources.Load<AudioClip>("Rock");
        flowerSound = Resources.Load<AudioClip>("PullSound");
        grassSound = Resources.Load<AudioClip>("GrassSound");
        rumbleSound = Resources.Load<AudioClip>("Rumble"); 
         //morphSound = Resources.Load<AudioClip>("Morph");
         audioSrc = GetComponent<AudioSource>();
    }

    public static void playSound()
    {
        audioSrc.PlayOneShot (stepSound);
    }
    public static void playRock()
    {
        audioSrc.PlayOneShot(rockSound);
    }
    public static void playFlower()
    {
        audioSrc.PlayOneShot(flowerSound);
    }
    public static void playGrass()
    {
        audioSrc.PlayOneShot(grassSound);
    }
    public static void playRumble()
    {
        audioSrc.PlayOneShot(rumbleSound);
    }
    //public static void playmorph()
    //{
    //    audioSrc.PlayOneShot(morphSound);
    //}
}
