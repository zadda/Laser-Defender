using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    public float health = 150f;
    //public GameObject EnemyProjectile01;
    public GameObject EnemyProjectile01;
    public float EnemyProjectile01Speed = 2f;
    public float shotsPerSeconds = 1.5f;
    public int scoreValue = 135;
    public AudioClip enemyDestroyed;

    private ScoreKeeping scoreKeeper;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeping>();
    }

    void Update()
    {
        float probility = Time.deltaTime * shotsPerSeconds;

        if (Random.value < probility)
        {
            FireEnemy();
        }
    }

    void FireEnemy()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -0.1f, 0);
        GameObject laser01 = Instantiate(EnemyProjectile01, startPosition, Quaternion.identity) as GameObject;
        laser01.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -EnemyProjectile01Speed, 0);
    }

    void OnTriggerEnter2D(Collider2D collidingWith)
    {

        PlayerProjectile missile = collidingWith.gameObject.GetComponent<PlayerProjectile>(); // get the child
        // controleer of het object waarmee we botsen een "Projectile" is/heeft (de laser van de player)
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(enemyDestroyed, transform.position);
                scoreKeeper.Score(scoreValue);
            }
        }
    }

    public float GetDamage() // public om in andere scrip tte kunnen gebruiken
    {
        return 100;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
