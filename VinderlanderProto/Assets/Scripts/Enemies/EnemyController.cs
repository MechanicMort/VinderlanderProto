using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float hp;
    public GameObject player;
    private NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        nav.destination = player.transform.position;
        if (hp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
