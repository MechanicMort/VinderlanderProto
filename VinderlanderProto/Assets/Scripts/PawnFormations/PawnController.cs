using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using RandomNameGeneratorLibrary;

public class PawnController : MonoBehaviour
{
    [Header("Animation")]
    public Animator animController;
    [Header("Living Stats")]

    public string pawnName;
    //Male
    //Female
    public string gender = "Male";
    public float timeOfDay;
    public float closest;
    public int number;


    public GameObject resourceCarried;

    [Header("Combat Stats")]
    public float pawnMeleeRange;
    public float pawnMeleeDamage;
    public float pawnMeleePierce;
    public float pawnAttackSpeed;

    public float pawnRangedRange;
    public float pawnReloadSpeed;
    private float ammoCount;

    public float pawnShieldBonus;
    public float pawnArmour;
    public float pawnChargeDefence;

    public float hP;
    public float hPMax;
    public float stamina;
    public float staminaMax;


    public float pawnMoveSpeed;
    public float walkingSpeed;
    public float runningSpeed;
    public float chargeSpeed;

    [Header("Bools")]
    public bool isInRange;
    public bool isInCombat;
    public bool isAttacking;
    public bool isRanged;
    public bool isHalting;
    public bool isHoldingFire;
   //need to make formations on the fly using gui interfaces
    public bool inFormation;

    private bool isRunning;
    private bool isFleeing;

    private bool isCharging;

    public bool isOrderComplete;


    [Header("GameObjects")]
    public GameObject FiringPos;
    public GameObject NameGen;
    public GameObject movingTo;
    public GameObject Quiver;
    public GameObject personCard;
    private GameObject movingToInstance;
    private Vector3 storedOrder;

    private NavMeshAgent navAgent;

    private List<GameObject> nearEnemies = new List<GameObject>() ;

    // Start is called before the first frame update
    void Start()
    {
        personCard.GetComponent<PersonCard>().myPerson = this.GetComponent<PawnController>();
        NameGen = GameObject.FindGameObjectWithTag("NameGen");
        pawnName = NameGen.GetComponent<NameGenerator>().GenerateRandomNameMale();
        isAttacking = false; 
        hP = hPMax;
        stamina = staminaMax;
        isOrderComplete = false;
        movingToInstance = Instantiate(movingTo);
        movingToInstance.SetActive(false);
        navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine("AdjustFatigue");
        if (isRanged)
        {
            objectPool();
        }
        StartCoroutine("CheckInCombat");
    }
    public void objectPool()
    {
        Quiver = transform.GetChild(2).transform.GetChild(0).gameObject;
        ammoCount = Quiver.transform.childCount;
        if (ammoCount != 0)
        {
            isRanged = true;
        }
        else
        {
            isRanged = false;
        }
    }

    private void OutOfCombat()
    {
        //wander visit baths worship buy food 
    }

    // Update is called once per frame
    void Update()
    {
        if (!navAgent.isActiveAndEnabled)
        {

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.up, out hit, 100000, LayerMask.GetMask("Ground")))
            {
                transform.position = hit.point;
            }

            if (Physics.Raycast(transform.position, -transform.up, out hit, 100000, LayerMask.GetMask("Ground")))
            {
                transform.position = hit.point;
            }
            navAgent.enabled = true;
        }
        if (!CheckDead())
        {
            if (!inFormation)
            {
                OutOfCombat();
            }
            UpdateSpeed();
            CheckMarker();
        }
        else
        {
            navAgent.speed = 0;
            navAgent.isStopped = true;
        }

    }
    private void UpdateSpeed()
    {
        if (isInCombat == false)
        {
            navAgent.speed = pawnMoveSpeed;
            navAgent.stoppingDistance = 0.2f;
        }
        else
        {
            navAgent.stoppingDistance = pawnMeleeRange;
            navAgent.speed = pawnMoveSpeed/2;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
      //  float range = GetRange();
      //  Gizmos.DrawSphere(transform.position, range);
    }
    private IEnumerator CheckInCombat()
    {
        bool CombatFlip = false;
        nearEnemies.Clear();
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, pawnMeleeRange, 1 << 10);
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            nearEnemies.Add(enemyColliders[i].gameObject);
        }
        for (int i = 0; i < nearEnemies.Count; i++)
        {
            if (nearEnemies[i].CompareTag("Enemy") && this.gameObject.CompareTag("PlayerPawn"))
            {     
                isInCombat = true;
                CombatFlip = true;
            }
            else if (nearEnemies[i].CompareTag("PlayerPawn") && this.gameObject.CompareTag("Enemy"))
            {     
                isInCombat = true;
                CombatFlip = true;
            }
        }
        if (isAttacking == false && isInCombat == true && isFleeing == false)
        {

            StartCoroutine("ReadyAttack");
        }
        if (!CombatFlip)
        {
            isInCombat = false;
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("CheckInCombat");
    }
  public void  RangedAttack(GameObject enemyFormation)
    {
        StartCoroutine(RangedAttackCoroutine(enemyFormation));
    }

    public IEnumerator RangedAttackCoroutine(GameObject enemyFormation)
    {

        yield return new WaitForSeconds(pawnReloadSpeed );
        GameObject enemy = null;
        if (!isInCombat && isRanged)
        {
            nearEnemies.Clear();
            if (enemyFormation.transform.parent.GetComponent<FormationManager>().UnitManaged.Count != 0)
            {
                for (int i = 0; i < enemyFormation.transform.parent.GetComponent<FormationManager>().UnitManaged.Count; i++)
                {
                    nearEnemies.Add(enemyFormation.transform.parent.GetComponent<FormationManager>().UnitManaged[i]);
                }
                enemy  = nearEnemies[Random.Range(0, nearEnemies.Count)];
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }
              

            if (enemy == null)
            {
                // print("ErrorCaught");
            }
            else if (isRanged && Quiver.transform.childCount != 0)
            {
                GameObject temp = Quiver.transform.GetChild(0).gameObject;
                temp.transform.SetParent(null, true);
                temp.transform.position = FiringPos.transform.position;
                temp.GetComponent<Projectile>().fireAt(enemy);
            }
            objectPool();
            if (isAttacking == true && isFleeing == false)
            {
                RangedAttack(enemyFormation);
            }
        }    
    }




    public IEnumerator ReadyAttack()
    {

        isAttacking = true;
        GameObject enemy = null;
        for (int i = 0; i < nearEnemies.Count; i++)
        {
            if (nearEnemies[i].CompareTag("Enemy") && this.gameObject.CompareTag("PlayerPawn"))
            {
                enemy = nearEnemies[i];
                break;
            }
            else if (nearEnemies[i].CompareTag("PlayerPawn") && this.gameObject.CompareTag("Enemy"))
            {
                enemy = nearEnemies[i];
                break;
            }
        }

        if (enemy == null)
        {
            // print("ErrorCaught");
        }
        else if (Vector3.Distance(transform.position,enemy.transform.position) <= pawnMeleeRange)
        {
            if (enemy != null)
            {
                float heightdif = transform.position.y - enemy.transform.position.y;
                if (isCharging && enemy != null)
                {
                    isCharging = false;
                    enemy.GetComponent<PawnController>().TakeDamage(pawnMeleeDamage,  Mathf.Sqrt(navAgent.velocity.sqrMagnitude) + heightdif, pawnMeleePierce,false);
                }
                else if (enemy != null)
                {
                    enemy.GetComponent<PawnController>().TakeDamage(pawnMeleeDamage + heightdif,0, pawnMeleePierce,false);
                }
            }          
            else if (enemy == null)
            {
                // print("ErrorCaught");
            }
        }
    
        nearEnemies.Clear();
        print(1 - (stamina / 100));
        yield return new WaitForSeconds(pawnAttackSpeed * 1- (stamina / 100));
        isAttacking = false;
    }
    public void Disengage()
    {
        isFleeing = true;
        StopAllCoroutines();
    }
    public void Charge()
    {
        navAgent.stoppingDistance = pawnMeleeRange;
        pawnMoveSpeed = chargeSpeed;
        isCharging = true;
    }
    public void TakeDamage(float damagedealt, float chargeDamage, float armourPierce,bool ranged)
    {


        if (ranged)
        {
            damagedealt *= pawnShieldBonus;
        }
        chargeDamage *= pawnChargeDefence;    
        damagedealt *= (pawnArmour - armourPierce);  
        damagedealt += chargeDamage;
        if (damagedealt > 0)
        {
            hP -= damagedealt;
        }
        isInCombat = true;
    }
    public bool CheckDead()
    {    
        if (hP <= 0)
        {
            StopAllCoroutines();
            transform.tag = "Dead";
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            transform.rotation = new Quaternion(180, 0, 0, 0);
            movingTo.SetActive(false);
            this.GetComponent<Collider>().isTrigger = true;
            return true;
        }
        return false;
    }
    private IEnumerator AdjustFatigue()
    {
        yield return new WaitForSeconds(0.1f);
        if (navAgent.velocity.sqrMagnitude > 0.2)
        {
            if (stamina <= 30 )
            {
                print("slow");
            }
            else if (isInCombat == true)
            {
                stamina -= 0.3f;
            }
            else if (isRunning == true)
            {
                stamina -= 0.2f;
            }
            else 
            {
                stamina -= 0.05f;
            }
          

        }
        StartCoroutine("AdjustFatigue");
    }
    public void ToggleRun()
    {
        if (isRunning == true)
        {
            isRunning = false;
            pawnMoveSpeed = walkingSpeed;

        }
        else if (isRunning == false)
        {
            isRunning = true;
            pawnMoveSpeed = runningSpeed;
        }
    }
    public void CheckMarker()
    {
   
        if (Vector3.Distance(this.transform.position, movingToInstance.transform.position) <= 2.8f)
        {
            movingToInstance.SetActive(false);                                                                                                                                                                                         
            isOrderComplete = true;
        }
        else if (movingToInstance.activeInHierarchy == false)
        {
            isOrderComplete = true;
        }
        else
        {
            isOrderComplete = false;
        }
    }
    public void Moving()
    {
        isCharging = false;
        
    }   
    public void NewOrder(Vector3 movePos)
    {
        isOrderComplete = false;

        if (movePos != storedOrder)
        {

            if (Vector3.Distance(this.transform.position, movePos) >= 2.5f)
            {
                isOrderComplete = false;
                movingToInstance.SetActive(true);
                movingToInstance.transform.position = movePos;
            }


            if (isInCombat == false)
            {
                navAgent.speed = pawnMoveSpeed;
                if (this.gameObject.activeInHierarchy == true && navAgent.isActiveAndEnabled)
                {
                    navAgent.destination = movePos;
                }
            }
            else
            {
                navAgent.speed = pawnMoveSpeed / 2;
                if (this.gameObject.activeInHierarchy == true)
                {
                    navAgent.destination = movePos;
                }
            }
        }  
        storedOrder = movePos;
    }
    public void LookAt(Vector3 lookAt)
    {
        transform.LookAt(lookAt);

    }
}
