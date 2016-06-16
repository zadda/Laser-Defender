/* verantwoordelijk voor het spawnen van enemies
 * het bewegen van enemies
 * de grenzen waarin ze bewegen
 * formatie
 * respawn tijd
 */


using UnityEngine;
using System.Collections;

public class Formation2Controller : MonoBehaviour
{

    public GameObject enemyPrefab;

    public float width = 10f;
    public float height = 5f;
    public float padding = 0.5f;

    public float enemySpeed = 5.5f;
    public float spawnDelay = 0.5f;

    private float xMin;
    private float xMax;

    public bool movingRight = true;

    // Use this for initialization
    void Start ()
    {
        //initialize fixed borders
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftBorder.x;// + padding;
        xMax = rightBorder.x;// - padding;

        SpawnUntilFull();
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height)); // tekent een kader rond het enemyFormation GameObject
                                                                            //  zodat we de randenzien
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (movingRight)
        {
          transform.position += Vector3.right * enemySpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * enemySpeed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xMin)
        {
            movingRight = true;
            //transform.position -= new Vector3(0f,0.099f); //beweeg naar beneden
        }
        else if (rightEdgeOfFormation > xMax)
        {
            movingRight = false;
            //transform.position -= new Vector3(0f, 0.099f);
        }

        if(AllMembersDead())
        {
            //Invoke("SpawnUntilFull", 1.5f);
            SpawnUntilFull();
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition && FormationController.numberOfRounds == 2)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition; // transform of the formation
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        return null;
    }

    bool AllMembersDead()
    {
        //transform.childCount;
        foreach(Transform childPositionGameObject in transform)
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        return true;
    }
}