using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public bool isAttacking = false;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" && isAttacking)
        {
            collision.transform.GetComponent<EnemyController>().TakeDamage(4);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && isAttacking)
        {
            other.GetComponent<EnemyController>().TakeDamage(4);
        }
    }
}
