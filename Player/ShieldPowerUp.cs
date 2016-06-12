using UnityEngine;
using System.Collections;

public class ShieldPowerUp : MonoBehaviour
{

    public GameObject playerShieldPowerUp;
    public GameObject playerShield;

    private float maximumTijd = 10f;
    private float countDown = 0;
    

	// Use this for initialization
	void Start ()
    {
        countDown = maximumTijd;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        countDown -= Time.deltaTime;

        Vector3 shieldStartPosition = new Vector3(Random.Range(-6.3f, 6.3f), 2, 0);

        if (countDown <= 0)
        {
            // spawn shield
            GameObject shield = Instantiate(playerShieldPowerUp, shieldStartPosition, Quaternion.identity) as GameObject;
            //beweeg shield
            shield.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5f, 0);
            //shield.transform.parent = shieldStartPosition; // transform;// transform of the formation
            //reset timer
            countDown = maximumTijd;
        }
	}

    void OnTriggerEnter2D(Collider2D botstMet)
    {
      
        // nieuw object missile als gelijk is aan EnemyProjectile
        PlayerController player = botstMet.gameObject.GetComponent<PlayerController>();

        Vector3 shieldStartPosition = new Vector3(botstMet.transform.position.x, -3.72f, 0); //player.transform.position.x

        // controleer of het object waarmee we botsen de Player is
        if (player)
        {
            GameObject shield = Instantiate(playerShield, shieldStartPosition, Quaternion.identity) as GameObject;
        }
    }
}
