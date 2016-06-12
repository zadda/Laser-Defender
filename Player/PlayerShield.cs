using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    public Sprite[] hitSprites; // creërt optie om grootte van array te zetten in Editor
    public int timesHit = 0;

    // als er een enemy laser op het schild komt -> destroy laser object
    void OnTriggerEnter2D(Collider2D col) //col is what we are colliding with, which is the laser beam
    {
        //misschien nog check doen wat voor type laser we mee botsen
        Destroy(col.gameObject);
        timesHit += 1;
        LoadSprites(); // bij hit, laad volgende sprite in de reeks om schade te tonen
        if (timesHit == 6)
        {
            Destroy(gameObject); // destroy schield na x hits
        }
    }

    void LoadSprites()
    {
        int spriteIndex = timesHit - 1; 

        // in case we don't have a 2nd and 3d sprite set
        if (hitSprites[spriteIndex] != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex]; // change the shield so that it shows that it's been hit
        }
        else
        {
            Debug.LogError("Sprite missing", gameObject); //prevent out of range
        }
    }

}
