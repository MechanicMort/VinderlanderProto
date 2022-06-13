using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public bool isAttacking = false;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && isAttacking)
        {
            collision.transform.GetComponent<CombatController>().TakeDamage(4);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isAttacking)
        {
            other.GetComponent<CombatController>().TakeDamage(4);
        }
    }
}
