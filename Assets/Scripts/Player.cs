using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// possible states
// Idle, Moving, Jumping, Dead

public class Player : MonoBehaviour
{
    // Params
    [SerializeField] int numberOfLives = 1;
    [SerializeField] float moveTimeSeconds = 0.35f;
    [SerializeField] AudioClip[] soundsArray;
    [SerializeField] float[] soundsVolumes;
    [SerializeField] float inputBufferTimeSeconds = 0.1f;

    // Variables // Serialized only for view in inspector
    [SerializeField] string myState = "Idle";
    [SerializeField] string currentInput = "No input";

    int yTarget = 0;
    float yStart = 0;
    float timeToTarget = 0;

    float bufferTime = 0;

    bool invulnerability = false;

    // Cached References
    Animator myAnimator;
    Rigidbody2D myRigidbody;

    // Public methods
    public int GetNumberOfLives() { return numberOfLives; }
    public void SetState(string newState) { myState = newState; }
    public void SetCollisionLayer(string newLayer)
    {
        int layerIndex = LayerMask.NameToLayer(newLayer);
        gameObject.layer = layerIndex;
    }

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayClipAtPoint(
            soundsArray[soundIndex],
            Camera.main.transform.position,
            soundsVolumes[soundIndex]);
    }

    public int GetRoundedYPos()
    {
        return Mathf.RoundToInt(transform.position.y);
    }

    public void SetCurrentInput(string newInput)
    {
        currentInput = newInput;
        bufferTime = inputBufferTimeSeconds;
    }

    // Unity Engine methods
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DetectInput();
        ResetInput();

        if (myState == "Idle")
        {
            Move();
            Jump(); // Jump animation should cancel movement if pressend on exact same frame
        }
        else if (myState == "Moving")
        {
            transform.position = Vector3.Lerp(
                new Vector2(0, yStart),
                new Vector2(0, yTarget),
                timeToTarget / moveTimeSeconds);
            timeToTarget += Time.deltaTime;
            if (transform.position.y == yTarget)
            {
                myState = "Idle";
            }
        }
        CorrectOrderInLayer();
    }

    // Private methods
    private void DetectInput()
    {
        // TODO try getting touches with Fire1 and Fire2 to be able to test with mouse click

        // Currently disabled because using canvas and buttons

        // // touch screen or mouse inputs
        // foreach (Touch touch in Input.touches)
        // {
        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         Vector3 touchPosition = FindObjectOfType<Camera>().ScreenToWorldPoint(touch.position);
        //         Debug.Log(touchPosition.ToString());
        //         if (touchPosition.x > 0)
        //         {
        //             currentInput = "Jump";
        //         }
        //         else if (touchPosition.x <= 0 & touchPosition.y < 0)
        //         {
        //             currentInput = "Down";
        //         }
        //         else if (touchPosition.x <= 0 & touchPosition.y >= 0)
        //         {
        //             currentInput = "Up";
        //         }
        //     }
        // }

        // keyboard or controller inputs
        if (Input.GetButtonDown("Jump"))
        {
            currentInput = "Jump";
            bufferTime = inputBufferTimeSeconds;
        }

        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > Mathf.Epsilon)
        {
            currentInput = "Up";
            bufferTime = inputBufferTimeSeconds;
        }
        else if (verticalInput < -Mathf.Epsilon)
        {
            currentInput = "Down";
            bufferTime = inputBufferTimeSeconds;
        }
    }

    private void ResetInput()
    {
        if(bufferTime > 0)
        {
            bufferTime -= Time.deltaTime;
        }
        if(bufferTime <= 0)
        {
            currentInput = "No input";
        }
    }

    private void Jump()
    {
        if (currentInput == "Jump")
        {
            myAnimator.SetTrigger("Jump");
        }
    }

    private void Move()
    {
        // move up
        if (currentInput == "Up")
        {
            if (transform.position.y < 0.5)
            {
                myState = "Moving";

                yStart = transform.position.y;
                yTarget = Mathf.RoundToInt(yStart) + 1;
                timeToTarget = 0f;

                myAnimator.SetTrigger("Roll Up");
            }
        }

        // move down
        else if (currentInput == "Down")
        {
            if (transform.position.y > - 0.5)
            {
                myState = "Moving";

                yStart = transform.position.y;
                yTarget = Mathf.RoundToInt(yStart) - 1;
                timeToTarget = 0f;

                myAnimator.SetTrigger("Roll Down");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(invulnerability == false)
        {
            if (collision.tag == "Fire") { FindObjectOfType<SushichefManager>().TriggerSushichef("Fire"); }
            else if (collision.tag == "Cut") { FindObjectOfType<SushichefManager>().TriggerSushichef("Cut"); }

            if (numberOfLives > 0)
            {
                numberOfLives -= 1;
            }

            if (numberOfLives > 0)
            {
                Hurt();
            }
            else if (numberOfLives <= 0)
            {
                Die(collision.tag);
            }
        }
    }

    private void Hurt()
    {
        StartCoroutine(InvulnerabilityTime(1.2f));
        StartCoroutine(TurnToRed(1.2f));
        FindObjectOfType<EnemiesManager>().SetStopPattern(true);
    }

    IEnumerator InvulnerabilityTime(float timeInvulnerable)
    {
        invulnerability = true;
        yield return new WaitForSeconds(timeInvulnerable);
        invulnerability = false;
    }

    IEnumerator TurnToRed(float timeRed)
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(timeRed);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void Die(string collisionTag)
    {
        Time.timeScale = 1f;

        StartCoroutine(TurnToRed(0.1f));
        myState = "Dead";
        myAnimator.SetBool("Dead", true);
        
        if (collisionTag == "Fire") { myAnimator.SetTrigger("Death Fire"); }
        else if (collisionTag == "Cut") { myAnimator.SetTrigger("Death Cut"); }

        FindObjectOfType<LevelController>().LoseGame();
    }

    private void CorrectOrderInLayer()
    {
        int newOrder = 11;
        if (transform.position.y >= 0.5) { newOrder = 6; }
        else if (transform.position.y <= -0.5) { newOrder = 16; }

        transform.Find("Body").GetComponent<SpriteRenderer>().sortingOrder = newOrder;
    }
}