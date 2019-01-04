using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public ParticleSystem expl;
    private TrailRenderer trail;

    private GameManager gameManager;

    public AudioClip[] randomClip;

    private AudioSource audioSource;

    private int previousNumber;
    public float maxTimeInMilli;


    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.trail = GetComponent<TrailRenderer>();
        this.audioSource = GetComponent<AudioSource>();
        Invoke("GoBall", 2);
    }

    // Make the ball start to move in a random direction
    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(30, -25));
        }
        else
        {
            rb2d.AddForce(new Vector2(-30, -25));
        }
        this.trail.enabled = true;
    }

    //rest the ball to the middle
    public void ResetBall()
    {

        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;

    }

    //reset the ball and call GoBall to start again
    public void RestartGame()
    {
        this.trail.enabled = false;
        ResetBall();
        Invoke("GoBall", 1);
    }

    //reset the ball and stop the animation
    public void EndGame()
    {
        this.trail.enabled = false;
        ResetBall();
    }

    /*
    * Each time there's a collision "playRandomSound" function is called
    * if the ball hit a player then the velocity is increased to 1.1 (maximum 6 velocity)
    * acording to the collider a different particle effect is played (red for left and right wall white for the others)
    */
    void OnCollisionEnter2D(Collision2D coll)
    {

        playRandomSound();
        
        if (coll.collider.CompareTag("Player"))
        {
            if (rb2d.velocity.x < 6 && rb2d.velocity.y < 6)
            {
                rb2d.velocity *= 1.1f;
            }

            this.PlayExplParticuleEffect(coll, Color.white);
        }
        else if (coll.collider.gameObject.name.Equals("LeftWall")
           || coll.collider.gameObject.name.Equals("RightWall"))
        {
            this.gameManager.Score(coll.collider.gameObject.name);
            this.PlayExplParticuleEffect(coll, Color.red);
            this.RestartGame();
        }
        else
        {
            this.PlayExplParticuleEffect(coll, Color.white);
        }
    }

    /*
    *return the impact force of a collision
    */
    float GetForce(Collision2D c)
    {
        float impulse = 0f;
        foreach (ContactPoint2D cp in c.contacts)
        {
            impulse += cp.relativeVelocity.x;
        }

        return impulse;
    }

    /*
    *Play a particle effect with the given
    *color 
    */
    void PlayExplParticuleEffect(Collision2D coll, Color color)
    {
        var main = expl.main;
        main.startColor = color;
        main.startSpeed = GetForce(coll);
        var test = Instantiate(expl, coll.contacts[0].point, Quaternion.identity);
        test.Play();
    }

    public void setGameManager(GameManager gm)
    {
        this.gameManager = gm;
    }

    /* Play a random sound between a given array of 5 sounds
    * a sound can't be played 2 in a row
    */
    private void playRandomSound()
    {

        int newNumber = Random.Range(0, this.randomClip.Length);
        if (newNumber == previousNumber)
        {
            playRandomSound();
        }
        else
        {
            audioSource.clip = this.randomClip[newNumber];
            audioSource.Play();

            previousNumber = newNumber;
        }
    }
}