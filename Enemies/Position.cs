using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour
{
    // Zorgt ervoor dat in de editor de positie van de enemy altijd zichtbaar is ook wanneer niet geselecteerd
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
