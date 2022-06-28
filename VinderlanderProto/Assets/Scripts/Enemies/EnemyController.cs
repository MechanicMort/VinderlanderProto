using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float hp;
    public GameObject player;
    public GameObject sword;
    private NavMeshAgent nav;
    public string attackType = "none";
    public bool readyingAttack = false;
    public GameObject swordPosUp;
    public GameObject swordPosDown;
    public GameObject swordPosLeft;
    public GameObject swordPosRight;
    public GameObject swordPosCentre;
    public GameObject swordAttackPos;
    public EnemySword swordController;
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

    private IEnumerator GetRandomAttack()
    {
        attackType = "Waiting";
        yield return new WaitForSeconds(0.2f);
        switch (Random.Range(0, 3))
        {
            case 0:
                attackType = "up";
                break;
            case 1:
                attackType = "right";
                break;
            case 2:
                attackType = "left";
                break;
            case 3:
                attackType = "down";
                break;
            case 4:
                attackType = "centre";
                break;
        }
    }
    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(1.4f);
        if (Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            attackType = "attack";
            readyingAttack = false;
            swordController.isAttacking = true;
        }
        else
        {
            StartCoroutine("DoAttack");
        }

    }

    private void CombatControllerEnmy()
    {
        if (attackType == "attack" && Vector3.Distance(sword.transform.position, swordAttackPos.transform.position) <= 0.05f)
        {
            swordController.isAttacking = false;
            attackType = "none";
        }
        switch (attackType)
            {
                case "none":          
                    StartCoroutine("GetRandomAttack");
                    attackType = "Waiting";
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosCentre.transform.position, 0.1f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosCentre.transform.rotation, 0.1f);
                    //sword.transform.SetParent(swordPosRight.transform, false);
                    break;
                case "Waiting":
                    attackType = "Waiting";
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosCentre.transform.position, 0.1f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosCentre.transform.rotation, 0.1f);
                    //sword.transform.SetParent(swordPosRight.transform, false);
                    break;
                case "right":
                    if (!readyingAttack)
                    {
                        StartCoroutine("DoAttack");
                        readyingAttack = true;
                    }
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosRight.transform.position, 0.1f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosRight.transform.rotation, 0.1f);
                    //sword.transform.SetParent(swordPosRight.transform, false);
                    break;
                case "left":
                    if (!readyingAttack)
                    {
                        StartCoroutine("DoAttack");
                        readyingAttack = true;
                    }
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosLeft.transform.position, 0.1f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosLeft.transform.rotation, 0.1f);
                    //  sword.transform.SetParent(swordPosLeft.transform, false);
                    break;
                case "up":
                    if (!readyingAttack)
                    {
                        StartCoroutine("DoAttack");
                        readyingAttack = true;
                    }
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosUp.transform.position, 0.1f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosUp.transform.rotation, 0.1f);
                    //   sword.transform.SetParent(swordPosUp.transform,false);
                    break;
                case "down":
                    if (!readyingAttack)
                    {
                        StartCoroutine("DoAttack");
                        readyingAttack = true;
                    }
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosDown.transform.position, 0.1f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosDown.transform.rotation, 0.1f);
                    // sword.transform.SetParent(swordPosDown.transform, false);
                    break;
                case "centre":
                    if (!readyingAttack)
                    {
                        StartCoroutine("DoAttack");
                        readyingAttack = true;
                    }
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosCentre.transform.position, 0.1f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosCentre.transform.rotation, 0.1f);
                    //  sword.transform.SetParent(swordPosCentre.transform, false);
                    break;
                case "block":
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosCentre.transform.position, 0.1f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosCentre.transform.rotation, 0.1f);
                    // sword.transform.SetParent(swordPosCentre.transform, false);
                    break;
                case "attack":
                    sword.transform.position = Vector3.Lerp(sword.transform.position, swordAttackPos.transform.position, 0.2f);
                    sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordAttackPos.transform.rotation, 0.4f);
                    // sword.transform.SetParent(swordPosCentre.transform, false);
                    break;
            }

        
    }

    // Update is called once per frame
    void Update()
    {

        CombatControllerEnmy();
        nav.destination = player.transform.position;
        if (hp <= 0)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
