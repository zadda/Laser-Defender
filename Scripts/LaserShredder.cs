using UnityEngine;
using System.Collections;

public class LaserShredder : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) //col is what we are colliding with, which is the laser beam
    {
        Destroy(col.gameObject);
    }
}
