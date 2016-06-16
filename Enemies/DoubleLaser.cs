using UnityEngine;
using System.Collections;

public class DoubleLaser : MonoBehaviour
{

    public GameObject enemyDoubleLaser;
    public static bool doubleFire = false;

    private float maximumTijd = 20f; //start time of the timer
    private float countDown = 0;
    

	// Use this for initialization
	void Start ()
    {
        countDown = maximumTijd;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        // only start spawning the enemy Double Laser after a score of 1000
        if (ScoreKeeping.score >= 1000 && doubleFire == false)
        {
             countDown -= Time.deltaTime;
        }


       

        Vector3 powerDownStartPos = new Vector3(Random.Range(-6.3f, 6.3f), 2, 0);

        if (countDown <= 0)
        {
            // spawn shield
            GameObject powerDown = Instantiate(enemyDoubleLaser, powerDownStartPos, Quaternion.identity) as GameObject;
            //beweeg powerdown
            powerDown.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5f, 0);
            //reset timer
            countDown = maximumTijd;
        }
	}

    void OnTriggerEnter2D(Collider2D botstMet)
    {

        doubleFire = true;

        PlayerController player = botstMet.gameObject.GetComponent<PlayerController>();

        Vector3 shieldStartPosition = new Vector3(botstMet.transform.position.x, -3.72f, 0); //player.transform.position.x

       // controleer of het object waarmee we botsen de Player is
        if (player)
        {
            Destroy(gameObject);
        }
    }
}
