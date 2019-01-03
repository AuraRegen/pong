using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

    private Rigidbody2D rb2d;
    public ParticleSystem expl;
    private TrailRenderer trail;
    
    private GameManager gameManager;

    public AudioClip[] randomClip;

    private AudioSource audioSource;

    private int previousNumber;
    public float maxTimeInMilli;
    // Use this for initialization
    void Start () {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.trail = GetComponent<TrailRenderer>();
        this.audioSource = GetComponent<AudioSource>();
        Invoke("GoBall", 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
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

    public void ResetBall()
    {

        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
       
    }

    public void RestartGame()
    {
        this.trail.enabled = false;
        ResetBall();
        Invoke("GoBall", 1);
    }

    void EndGame()
    {
        
    }


    void OnCollisionEnter2D(Collision2D coll)
    {

        playRandomSound();
        if (coll.collider.CompareTag("Player"))
        {
            rb2d.velocity *= 1.1f;

            this.playExplParticuleEffect(coll,Color.white);
        }else if (coll.collider.gameObject.name.Equals("LeftWall") 
            || coll.collider.gameObject.name.Equals("RightWall"))
        {   
            this.gameManager.Score(coll.collider.gameObject.name);
            this.playExplParticuleEffect(coll, Color.red);
        } else
        {
            this.playExplParticuleEffect(coll,Color.white);
        }
    }

    float getForce(Collision2D c)
    {
        float impulse = 0f;
        foreach (ContactPoint2D cp in c.contacts)
        {
            impulse += cp.relativeVelocity.x;
        }

        return impulse;
    }

    void playExplParticuleEffect(Collision2D coll,Color color)
    {
        var main = expl.main;
        main.startColor = color;
        main.startSpeed = getForce(coll);
        var test = Instantiate(expl, coll.contacts[0].point, Quaternion.identity);
        test.Play();
    }

    public void setGameManager(GameManager gm)
    {
        this.gameManager = gm;
    }

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
