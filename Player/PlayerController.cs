/* handles control of PlayerShip
 * collision with enemy laser type 1
 * sets firing speed, ship speed, projectile speed
 * handles health when hit
 * sets borders the ship can go * 
 */

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // make the Options available in the Unity Editor
    public float ShipSpeed;
    public float laserSpeed;
    public float firingRate;
    public float padding = 0.5f;
    public float playerHealth = 625f;

    //need to add a Projectile and sound clip in the editor
    public GameObject PlayerProjectile; 
    public AudioClip playerFire;

    private HealthBar health;

    float xMin;
    float xMax;

	// on start of game
	void Start ()
    {
        ShipSpeed = 5f;
        // zet de grenzen waarin het schip zig mag bewegen, aan de hand van de camera
        float distanceZ = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceZ));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceZ));
        xMin = leftMost.x + padding;
        xMax = rightMost.x -padding;

        //zoek de healtbar
        health = GameObject.FindObjectOfType<HealthBar>();
    }
	
    void Fire()
    {
        // offset is zodat laser niet over het schip komt, niet meer nodig door sorting layers te gebruiken
        //Vector3 offset = new Vector3(0, 0.5f, 0);
        //lanceert nieuwe laser  ▼
        GameObject laser = Instantiate(PlayerProjectile, transform.position, Quaternion.identity) as GameObject; // position+ offset

        // zorgt ervoor dat de laser beweegt  ▼
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
    }

	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.LeftArrow)) // GetKey ipv GetKeyDown omdat we dan blijven bewegen eens de knop is ingeduwd bij keydown beweeg je alleen telkens je
                                            // knop indrukt
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        
        //instantiate player laser blijft schieten met interval zolang knop ingedrukt
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire",0.000001f,firingRate); // 0.000001 want 0 geeft problemen
            AudioSource.PlayClipAtPoint(playerFire, transform.position);
        }
        //stop met schieten
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke();
        }

    }

    void MoveLeft()
    {
       // Vector3 shipPos = new Vector3(-ShipSpeed * Time.deltaTime, 0f, 0f); //deltaTime zodat snelheid relatief blijft ook met hogere frames
        //transform.position += shipPos;
        transform.position += Vector3.left * ShipSpeed * Time.deltaTime;
    }

    void MoveRight()
    {
        //Vector3 shipPos = new Vector3(ShipSpeed * Time.deltaTime, 0f, 0f);
        //transform.position += shipPos;
        transform.position += Vector3.right * ShipSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collidingWith)
    {
        // nieuw object missile als gelijk is aan EnemyProjectile
        EnemyProjectile missile = collidingWith.gameObject.GetComponent<EnemyProjectile>();
        // controleer of het object waarmee we botsen een "Projectile" is (de laser)
        if (missile)
        {
            playerHealth -= missile.EnemyGetDamage();

            //beweeg healthbar x-positie negatief
            health.transform.position += new Vector3(-0.20f, 0f, 0f);

            missile.EnemyHit();
            if (playerHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        man.LoadLevel("Win Screen");
        Destroy(gameObject);
    }

}
