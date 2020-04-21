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

    // Variables // Serialized only for view in inspector
    [SerializeField] string myState = "Idle";

    [SerializeField] int yTarget = 0;
    float yStart = 0;
    float timeToTarget = 0;

    // Cached References
    Animator myAnimator;
    Rigidbody2D myRigidbody;

    // Gets and Sets
    public int GetNumberOfLives() { return numberOfLives; }
    public void SetState(string newState) { myState = newState; }
    public void SetCollisionLayer(string newLayer)
    {
        int layerIndex = LayerMask.NameToLayer(newLayer);
        gameObject.layer = layerIndex;
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            myAnimator.SetTrigger("Jump");
        }
    }

    private void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");

        // move up
        if (verticalInput > Mathf.Epsilon)
        {
            if (transform.position.y < 0.5)
            {
                myState = "Moving";

                yStart = transform.position.y;
                yTarget = Mathf.RoundToInt(yStart) + 1;
                timeToTarget = 0f;

                myAnimator.SetTrigger("Roll Up");
            }
            else
            {
                // start animation stuck
            }
        }

        // move down
        else if (verticalInput < - Mathf.Epsilon)
        {
            if (transform.position.y > - 0.5)
            {
                myState = "Moving";

                yStart = transform.position.y;
                yTarget = Mathf.RoundToInt(yStart) - 1;
                timeToTarget = 0f;

                myAnimator.SetTrigger("Roll Down");
            }
            else
            {
                // start animation stuck
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fire") { FindObjectOfType<SushichefManager>().TriggerSushichef("Fire"); }
        else if (collision.tag == "Cut") { FindObjectOfType<SushichefManager>().TriggerSushichef("Cut"); }

        numberOfLives -= 1;
        if (numberOfLives > 0)
        {
            Hurt();
        }
        else if (numberOfLives <= 0)
        {
            Die(collision.tag);
        }
    }

    private void Hurt()
    {
        StartCoroutine(TurnToRed(0.5f));
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

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayClipAtPoint(
            soundsArray[soundIndex],
            Camera.main.transform.position,
            soundsVolumes[soundIndex]);
    }

    private void CorrectOrderInLayer()
    {
        int newOrder = 11;
        if (transform.position.y >= 0.5) { newOrder = 6; }
        else if (transform.position.y <= -0.5) { newOrder = 16; }

        transform.Find("Body").GetComponent<SpriteRenderer>().sortingOrder = newOrder;
    }

    public int GetRoundedYPos()
    {
        return Mathf.RoundToInt(transform.position.y);
    }
}