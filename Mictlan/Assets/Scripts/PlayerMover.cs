using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : Move
{
    Vector3 position;
    Quaternion rotation;
    public bool morphed = false;
    public int steps = 0;
    public GameObject restoreFlower = null;
    public int powerLeft = 0;

    public PlayerMove(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }

    public IEnumerator Undo(PlayerMover player)
    {
        yield return
            player.StartCoroutine(
                player.UndoMove(
                    position,
                    rotation,
                    morphed,
                    steps,
                    restoreFlower,
                    powerLeft));
    }
}

public class MorphDog : Move
{
    public IEnumerator Undo(PlayerMover player)
    {
        player.HumanForm();
        yield return null;
    }
}

public class UsePower : Move
{
    int powerLeft;

    public UsePower(int powerLeft)
    {
        this.powerLeft = powerLeft;
    }

    public IEnumerator Undo(PlayerMover player)
    {
        player.Powerleft = powerLeft;
        player.ShowCurrentPower();
        yield return null;
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

    GameObject pickedFlower = null;

    // Start is called before the first frame update
    void Start()
    {
        var main = ps.GetComponent<ParticleSystem>().main;
        main.startColor = new Color(0, 0, 0, 72);

        movePoint.parent = null;
        
        StartCoroutine(MovCoroutine());

        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        MovementOfPlayerHorizontal = new Vector3(Mov, 0f, 0f);
        MovementOfPlayerVertical = new Vector3(0f, 0f, Mov);
    }

    public void ShowCurrentPower()
    {
        Debug.Log("power drained");
        movement.SetBool("Draining", true);
        StartCoroutine(cameraShake.Shake(.1f, .4f));
        if (Powerleft >= 4)
        {
            ps.Emit(50);
        }
        else if (Powerleft == 3)
        {
            ps.Emit(35);
        }
        else if (Powerleft == 2)
        {
            Charged.SetActive(false);
            ps.Emit(15);
        }
        else if (Powerleft == 1)
        {
            Lesscharge.SetActive(false);
            ps.Emit(5);
        }
        else
        {
            ps.Emit(10);
            LowCharge.SetActive(false);
            Glow.SetActive(false);
            var main = ps.GetComponent<ParticleSystem>().main;
            main.startColor = new Color(0, 0, 0, 72);
            Aura.SetActive(false);
        }
    }

    IEnumerator MovCoroutine()
    {
        bool playing = true;
        while (playing)
        {
            // handle player input
            if (Input.GetKeyDown("space"))
            {
                if(Powerleft >= 1)
                {
                    TL.Record(new UsePower(Powerleft));
                    Powerleft -= 1;
                    ShowCurrentPower();
                }
            }
            else if (Input.GetKeyDown("q"))
            {
                if (morphed == false)
                {
                    TL.Record(new MorphDog());
                    DogMorph();
                }
            }
            else if ((Input.GetAxisRaw("Horizontal")) == 1f)
            {
                yield return StartCoroutine(MoveCharacterTarget(MovementOfPlayerHorizontal, Quaternion.Euler(0, 90, 0)));
            }
            else if ((Input.GetAxisRaw("Vertical")) == 1f)
            {
                yield return StartCoroutine(MoveCharacterTarget(MovementOfPlayerVertical, Quaternion.Euler(0, 0, 0)));
            }
            else if ((Input.GetAxisRaw("Horizontal")) == -1f)
            {
                yield return StartCoroutine(MoveCharacterTarget(-MovementOfPlayerHorizontal, Quaternion.Euler(0, -90, 0)));
            }
            else if ((Input.GetAxisRaw("Vertical")) == -1f)
            {
                yield return StartCoroutine(MoveCharacterTarget(-MovementOfPlayerVertical, Quaternion.Euler(0, -180, 0)));
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                Move move = TL.Rewind();
                if (move != null)
                {
                    yield return StartCoroutine(move.Undo(this));
                }
            }

            yield return null;
        }
    }

    IEnumerator MoveCharacterTarget(Vector3 move, Quaternion direction)
    {
        //the code in the brackets detects what area it will be checking for the wall
        if (!Physics.CheckSphere(collidePoint.position + move, radiousSphere, whatStopsMovement ^ interactableObject))
        {
            PlayerMove undo = new PlayerMove(movePoint.position, movePoint.rotation);

            //moves the collider point, which in return moves the rock
            movePoint.position += move;
            Character.transform.rotation = direction;
            SoundManager.playSound();
            
            if (morphed)
            {
                dogmovement.SetBool("DogWalk", true);
            }
            else
            {
                movement.SetBool("Moving", true);
            }

            undo.morphed = morphed;
            if (morphed)
            {
                undo.steps = Steps;
                Steps += 1;
                if (Steps == 2)
                {
                    HumanForm();
                }
            }

            while (Vector3.Distance(transform.position, movePoint.position) > .05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime);
                yield return null;
            }

            if (pickedFlower != null)
            {
                undo.restoreFlower = pickedFlower;
                undo.powerLeft = Powerleft;
                burst.SetActive(true);
                pickedFlower.SetActive(false);
                Aura.SetActive(true);
                Glow.SetActive(true);
                Charged.SetActive(true);
                Lesscharge.SetActive(true);
                LowCharge.SetActive(true);
                Powerleft += 3;

                var main = ps.GetComponent<ParticleSystem>().main;
                main.startColor = new Color(255, 255, 255, 255);
                SoundManager.playFlower();
                StartCoroutine(effectoff());
                pickedFlower = null;
            }

            TL.Record(undo);
        }
    }

    public IEnumerator UndoMove(
        Vector3 position,
        Quaternion direction,
        bool morphed,
        int steps,
        GameObject restoreFlower,
        int powerLeft)
    {
        movePoint.position = position;
        Character.transform.rotation = direction;
        SoundManager.playSound();
        movement.SetBool("Moving", true);

        if (!this.morphed && morphed)
        {
            DogMorph();
        }
        Steps = steps;

        if (morphed)
        {
            dogmovement.SetBool("DogWalk", true);
        }
        else
        {
            movement.SetBool("Moving", true);
        }

        if (restoreFlower != null)
        {
            restoreFlower.SetActive(true);
            Powerleft = powerLeft;
        }

        while (Vector3.Distance(transform.position, movePoint.position) > .05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime);
            yield return null;
        }
    }


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collidePoint.position, radiousSphere);
    }

    // Update is called once per frame#
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            transition.SetTrigger("Start");
        }

        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Menu");
            transition.SetTrigger("Start");
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
            pickedFlower = col.gameObject;
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
        movement.SetBool("Morph", true);
        Player.SetActive(false);
        Dog.SetActive(true);
        morphed = true;
        gameObject.tag = "Xolo";
    }

    public void HumanForm()
    {
        movement.SetBool("Morph", false);
        Player.SetActive(true);
        Dog.SetActive(false);
        morphed = false;
        gameObject.tag = "Player";
        Steps = 0;
    }
}
