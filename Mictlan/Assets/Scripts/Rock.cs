using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public class RockMove : Move
//{
//    Vector3 position;
//    Quaternion rotation;
//    Rock rock;

//    public RockMove(Vector3 position, Quaternion rotation, Rock rock)
//    {
//        this.position = position;
//        this.rotation = rotation;
//        this.rock = rock;
//    }
//    public void Undo()
//    {
//        rock.rock.transform.position = position;
//        rock.rock.transform.rotation = rotation;
//    }

//}
public class Rock : MonoBehaviour
{
    public Transform collidePoint;

    public float movespeed = 15f;

    public LayerMask whatStopsMovement;
    public LayerMask PlayerMask;
    public LayerMask interactableObject;
    public LayerMask interactableObject2;

    public float radiousSphere;
    private float Mov = 5f;

    public GameObject player;
    public GameObject rock;

    public PlayerMover playerMov;

    public ParticleSystem ps;

    public Animator anim;
    public timelord TL;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //moves towards the collider point


        //the code in the brackets detects what area it will be checking for the player
        if (Physics.CheckSphere(collidePoint.position + new Vector3(0f, 0f, 5f), radiousSphere, PlayerMask))
            {
                if (player.tag == "Player")
                {
                    if (!Physics.CheckSphere(collidePoint.position - new Vector3(0f, 0f, Mov), radiousSphere, whatStopsMovement ^ interactableObject ^ interactableObject2))
                    {
                        anim.SetBool("Shake", false);
                        if (Input.GetKeyDown("space") && playerMov.Powerleft >= 1)
                        {
                            print("space key was pressed");
                            rock.transform.position -= new Vector3(0f, 0f, Mov);
                            SoundManager.playRock();
                            ps.Emit(10);

                        }
                        else
                        {
                        if(playerMov.Powerleft <= 0)
                        {
                            anim.SetBool("Shake", true);
                        }
                        }                   
                    }
                }
            else
            {

            }
            }
            
        if (Physics.CheckSphere(collidePoint.position + new Vector3(5f, 0f, 0f), radiousSphere, PlayerMask))
        {
            if (player.tag == "Player")
            {
                if (!Physics.CheckSphere(collidePoint.position - new Vector3(Mov, 0f, 0f), radiousSphere, whatStopsMovement ^ interactableObject ^ interactableObject2))
                {
                    anim.SetBool("Shake", false);
                    if (Input.GetKeyDown("space") && playerMov.Powerleft >= 1)
                    {

                        print("space key was pressed");
                        rock.transform.position -= new Vector3(Mov, 0f, 0f);
                        SoundManager.playRock();
                        ps.Emit(10);
                    }
                    else
                    {
                        if (Input.GetKeyDown("space") && playerMov.Powerleft <= 0)
                        {
                            anim.SetBool("Shake", true);
                        }
                    }
                }
            }
            else
            {

            }

        }
        if (Physics.CheckSphere(collidePoint.position + new Vector3(-5f, 0f, 0f), radiousSphere, PlayerMask))
        {
            if (player.tag == "Player")
            {
                if (!Physics.CheckSphere(collidePoint.position + new Vector3(Mov, 0f, 0f), radiousSphere, whatStopsMovement ^ interactableObject ^ interactableObject2))
                {
                    anim.SetBool("Shake", false);
                    if (Input.GetKeyDown("space") && playerMov.Powerleft >= 1)
                    {

                        print("space key was pressed");
                        rock.transform.position += new Vector3(Mov, 0f, 0f);
                        SoundManager.playRock();
                        ps.Emit(10);
                    }
                    else
                    {
                        if (Input.GetKeyDown("space") && playerMov.Powerleft <= 0)
                        {
                            anim.SetBool("Shake", true);
                        }
                    }
                }
            }
            else
            {

            }
        }
        if (Physics.CheckSphere(collidePoint.position + new Vector3(0f, 0f, -5f), radiousSphere, PlayerMask))
        {
            if (player.tag == "Player")
            {
                if (!Physics.CheckSphere(collidePoint.position + new Vector3(0f, 0f, Mov), radiousSphere, whatStopsMovement ^ interactableObject ^ interactableObject2))
                {
                    anim.SetBool("Shake", false);
                    if (Input.GetKeyDown("space") && playerMov.Powerleft >= 1)
                    {

                        print("space key was pressed");
                        rock.transform.position += new Vector3(0f, 0f, Mov);
                        SoundManager.playRock();
                        ps.Emit(10);
                    }
                    else
                    {
                        if (Input.GetKeyDown("space") && playerMov.Powerleft <= 0)
                        {
                            anim.SetBool("Shake", true);
                        }
                    }
                }
            }
            else
            {

            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collidePoint.position, radiousSphere);
    }
}
