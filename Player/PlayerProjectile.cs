using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour
{

    public float damage = 100f;

    public float GetDamage() // public om in andere scrip tte kunnen gebruiken
    {
        return damage;
    }

   public void Hit()
    {
        Destroy(gameObject);
    }

}
