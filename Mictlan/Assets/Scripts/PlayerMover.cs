using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMove : Move
{
    Vector3 position;
    Quaternion rotation;
    PlayerMover player;

    public PlayerMove(Vector3 position, Quaternion rotation, PlayerMover player)
    {
        this.position = position;
        this.rotation = rotation;
        this.player = player;
    }
    public void Undo()
    {
        player.movePoint.transform.position = position;
        player.movePoint.transform.rotation = rotation;
        player.Steps -= 1;
        if(player.Steps < 0)
        {
            player.Steps = 0;
        }
    }

}
public class MorphDog : Move
{
    
    PlayerMover player;

    public MorphDog(PlayerMover player)
    {       
        this.player = player;
    }
    public void Undo()
    {
        player.movement.SetBool("Morph", false);
        player.Player.SetActive(true);
        player.Dog.SetActive(false);
        player.morphed = false;
        player.gameObject.tag = "Player";
        player.Steps = 0;
    }

}
public class MorphHuman : Move
{

    PlayerMover player;
    Move move;

    public MorphHuman(PlayerMover player, Move move)
    {
        this.player = player;
        this.move = move;
    }
    public void Undo()
    {
        player.movement.SetBool("Morph", true);
        player.Player.SetActive(false);
        player.Dog.SetActive(true);
        player.morphed = true;
        player.gameObject.tag = "Xolo";
        player.Steps = 2;
        move.Undo();
    }

}
public class Flower : Move
{

    PlayerMover player;
    GameObject flower;

    public Flower(PlayerMover player, GameObject flower)
    {
        this.player = player;
        this.flower = flower;
    }
    public void Undo()
    {
        flower.SetActive(true);
        player.hasPower = false;
        player.Aura.SetActive(false);
        player.Glow.SetActive(false);

        player.Charged.SetActive(false);
        player.Lesscharge.SetActive(false);
        player.LowCharge.SetActive(false);
        player.Powerleft -= 3;
       
    }

}
public class PlayerMover : MonoBehaviour
{

    public CameraShake cameraShake;

    public float movespeed = 5f;
    private float Mov = 5f;
    public float radiousSphere;

    public Transform movePoint;
    public Transform collidePoint;

    public bool morphed = false;
    private bool psShoot = false;

    private Vector3 MovementOfPlayerHorizontal;
    private Vector3 MovementOfPlayerVertical;

    public LayerMask whatStopsMovement;
    public LayerMask interactableObject;

    private int nextSceneToLoad;

    public GameObject Player;
    public GameObject Dog;
    public GameObject Glow;
    public GameObject Aura;
    public GameObject Character;

    public GameObject burst;

    //Charges
    public GameObject Charged;
    public GameObject Lesscharge;
    public GameObject LowCharge;

    public int Powerleft = 0;
    public bool hasPower = false;

    public int Steps = 0;

    public Animator transition;
    public Animator movement;
    public Animator dogmovement;

    public ParticleSystem ps;
    public timelord TL;
    public CamaraController CC;

    void Recordposition()
    {
        Move move = new PlayerMove(movePoint.position, movePoint.rotation, this);
        TL.Record(move);
    }

    // Start is called before the first frame update
    void Start()
    {

        ps.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, 72);
        movePoint.parent = null;
        //StartCoroutine(MovCoroutine());

        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        MovementOfPlayerHorizontal = new Vector3(Mov, 0f, 0f);
        MovementOfPlayerVertical = new Vector3(0f, 0f, Mov);

    }

    //IEnumerator MovCoroutine()
    //{
    //}

    // Update is called once per frame#
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Application.LoadLevel(Application.loadedLevel);
            transition.SetTrigger("Start");
        }
        if (Input.GetKeyDown("space"))
        {
            if(Powerleft >= 1)
            {
                Debug.Log("power drained");
                movement.SetBool("Draining", true);
                Powerleft -= 1;
                psShoot = true;
                StartCoroutine(cameraShake.Shake(.1f, .4f));
            }
            if (Powerleft == 0)
            {
                ps.Emit(10);
            }

        }

        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Menu");
            transition.SetTrigger("Start");

        }
        else
        {

        }

        if (Steps == 2)
        {
            morphed = false;
            HumanForm();
           //SoundManager.playmorph();
        }

        //--------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown("q"))
        {
            DogMorph();
            //SoundManager.playmorph();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        //moves towards the collider point
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if ((Input.GetAxisRaw("Horizontal")) == 1f)
            {
                //the code in the brackets detects what area it will be checking for the wall
                if (!Physics.CheckSphere(collidePoint.position + MovementOfPlayerHorizontal, radiousSphere, whatStopsMovement ^ interactableObject))
                {
                    Recordposition();
                    //moves the collider point, which in return moves the rock
                    movePoint.position += MovementOfPlayerHorizontal;
                    Character.transform.rotation = Quaternion.Euler(0, 90, 0);
                    //Dog.transform.rotation = Quaternion.Euler(0, 90, 0);
                    SoundManager.playSound();
                    movement.SetBool("Moving", true);
                    if (morphed == true)
                    {
                        Steps += 1;
                        dogmovement.SetBool("DogWalk", true);
                    }
                }
            }
            else if ((Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics.CheckSphere(collidePoint.position + MovementOfPlayerVertical, radiousSphere, whatStopsMovement ^ interactableObject))
                {
                    Recordposition();
                    movePoint.position += MovementOfPlayerVertical;
                    Character.transform.rotation = Quaternion.Euler(0, 0, 0);
                    //Dog.transform.rotation = Quaternion.Euler(0, 0, 0);
                    SoundManager.playSound();
                    movement.SetBool("Moving", true);
                    if (morphed == true)
                    {
                        Steps += 1;
                        dogmovement.SetBool("DogWalk", true);
                    }
                }
            }

            if ((Input.GetAxisRaw("Horizontal")) == -1f)
            {
                if (!Physics.CheckSphere(collidePoint.position - MovementOfPlayerHorizontal, radiousSphere, whatStopsMovement ^ interactableObject))
                {
                    Recordposition();
                    movePoint.position -= MovementOfPlayerHorizontal;
                    Character.transform.rotation = Quaternion.Euler(0, -90, 0);
                    //Dog.transform.rotation = Quaternion.Euler(0, -90, 0);
                    SoundManager.playSound();
                    movement.SetBool("Moving", true);
                    if (morphed == true)
                    {
                        Steps += 1;
                        dogmovement.SetBool("DogWalk", true);
                    }
                }
            }
            else if ((Input.GetAxisRaw("Vertical")) == -1f)
                if (!Physics.CheckSphere(collidePoint.position - MovementOfPlayerVertical, radiousSphere, whatStopsMovement ^ interactableObject))
                {
                    Recordposition();
                    movePoint.position -= MovementOfPlayerVertical;
                    Character.transform.rotation = Quaternion.Euler(0, -180, 0);
                    //Dog.transform.rotation = Quaternion.Euler(0, -180, 0);
                    SoundManager.playSound();
                    movement.SetBool("Moving", true);
                    if (morphed == true)
                    {
                        Steps += 1;
                        dogmovement.SetBool("DogWalk", true);
                    }
                }
            //--------------------------------------------------------------------------------------------------------------------------
            if (hasPower == true)
            {
                Aura.SetActive(true);
                Glow.SetActive(true);

                Charged.SetActive(true);
                Lesscharge.SetActive(true);
                LowCharge.SetActive(true);
                Powerleft += 3;
                hasPower = false;
                ps.GetComponent<ParticleSystem>().startColor = new Color(255, 255, 255, 255);
            }
            //--------------------------------------------------------------------------------------------------------------------------

            if (Powerleft >= 4)
            {

                if (psShoot== true)
                {
                    PowerEmit();
                }

            }
            if (Powerleft == 3)
            {

                if (psShoot == true)
                {
                    PowerEmit();
                }
   
            }
            if (Powerleft == 2)
            {
                Charged.SetActive(false);
                if (psShoot == true)
                {
                    PowerEmit();
                }
            }
            if (Powerleft == 1)
            {
                Lesscharge.SetActive(false);
                if (psShoot == true)
                {
                    PowerEmit();
                }
            }
            if (Powerleft == 0)
            {
                LowCharge.SetActive(false);
                Glow.SetActive(false);
                ps.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, 72);
                Aura.SetActive(false);
            }


        }

        //--------------------------------------------------------------------------------------------------------------------------

        void OnDrawGizmosSelected()
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(collidePoint.position, radiousSphere);
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Exit")
        {
           
            StartCoroutine(endingcam());
        }

        if (col.gameObject.tag == "Flower")
        {
           
            burst.SetActive(true);
            //Destroy(col.gameObject);
            col.gameObject.SetActive(false);
            hasPower = true;
            //movement.SetBool("Charging", true);
            SoundManager.playFlower();
            StartCoroutine(effectoff());

        }

    }
    IEnumerator effectoff()
    {
        yield return new WaitForSeconds(4.0f);
        burst.SetActive(false);
    }

    IEnumerator endingcam()
    {
        movement.SetBool("Finish", true);
        CC.view1();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(nextSceneToLoad);
        Debug.Log("Reached Exit");
        transition.SetTrigger("Start");
    }
    
    void DogMorph()
    {       

        if (morphed == false)
        {
            movement.SetBool("Morph", true);
            Move move = new MorphDog(this);
            TL.Record(move);
            Player.SetActive(false);
            Dog.SetActive(true);
            morphed = true;
            gameObject.tag = "Xolo";
        }
    }
    void HumanForm()
    {
        movement.SetBool("Morph", false);
        Move move = new MorphHuman(this, TL.LastMove());
        TL.Record(move);
        Player.SetActive(true);
        Dog.SetActive(false);
        morphed = false;
        gameObject.tag = "Player";
        Steps = 0;
    }

    void PowerEmit()
    {
        if (Powerleft >= 4)
        {
            ps.Emit(50);
            psShoot = false;
        }
        if (Powerleft == 3)
        {
            ps.Emit(35);
            psShoot = false;
        }
        if (Powerleft == 2)
        {
            ps.Emit(15);
            psShoot = false;
        }
        if (Powerleft == 1)
        {
            ps.Emit(5);
            psShoot = false;
        }
    }


}
