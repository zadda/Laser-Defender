/* bevat aantal damage dat EnemyProjectile doet
 * target geraakt, destroy laser01
*/

using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{

    public float damage = 100f;

    public float EnemyGetDamage() // public om in andere script te kunnen gebruiken
    {
        return damage;
    }

   public void EnemyHit()
    {
        Destroy(gameObject);
    }

}
