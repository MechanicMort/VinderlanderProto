using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float projectileDamage;
    public float projectilePierce;
    public float projectileAccuracy;
    public float LaunchAngle;
    public bool isFlying = false;
    public Rigidbody rgb;
    public TrailRenderer Trail;
    // Start is called before the first frame update
    void Start()
    {
        Trail.emitting = false;
    }

    public void fireAt(GameObject targetLocation)
    {
        StartCoroutine(MoveTo(targetLocation));
    }

    private IEnumerator MoveTo(GameObject targetLocation)
    {

            rgb.isKinematic = false;
            rgb.useGravity = true;
            Trail.emitting = true;

            print("in");

            Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Vector3 targetXZPos = new Vector3(targetLocation.transform.position.x, 0.0f, targetLocation.transform.position.z);

            // rotate the object to face the target
            transform.LookAt(targetXZPos);

            // shorthands for the formula
            float R = Vector3.Distance(projectileXZPos, targetXZPos);
            float G = Physics.gravity.y;
            float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
            float H = targetLocation.transform.position.y - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
            float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H -R * tanAlpha)));
            float Vy = tanAlpha * Vz;

            // create the velocity vector in local space and get it in global space
            Vector3 localVelocity = new Vector3(0f, Vy, Vz);
            Vector3 globalVelocity = transform.TransformDirection(localVelocity);

            if (float.IsNaN(globalVelocity.x) || float.IsNaN(globalVelocity.z) || float.IsNaN(globalVelocity.y))
            {
                print("error caught");

            }
            else
            {
                rgb.velocity = globalVelocity;
            }
        yield return new WaitForSeconds(0.1f);
        
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "PlayerPawn" )
        {
            transform.SetParent(collision.transform, true);
            collision.gameObject.GetComponent<PawnController>().TakeDamage(projectileDamage,0, projectilePierce,true);
            rgb.isKinematic = true;
            this.StopAllCoroutines();
            Trail.emitting = false;
            this.GetComponent<Collider>().isTrigger = true; 
        }
        else if (collision.gameObject.tag == "Terrain")
        {
            rgb.isKinematic = true;
            this.StopAllCoroutines();
            Trail.emitting = false;
            this.GetComponent<Collider>().isTrigger = true;
        }
    }
}
