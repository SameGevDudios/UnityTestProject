using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private float bulletSpeed;
    private Vector3 previousFramePosition;

    private void Start()
    {
        DeathOverTime(); 
        StartCoroutine(CheckingForSurface());
    }
    private void FixedUpdate()
    {
        //Move();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            Debug.Log(collision.gameObject.name);
            Death();
        }
    }

    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
    }

    private void DeathOverTime()
    {
        Destroy(gameObject, lifetime);
    }
    
    private void Move()
    {
        transform.Translate(Vector3.forward * bulletSpeed, Space.Self);
    }

    private IEnumerator CheckingForSurface()
    {
        while (true)
        {
            previousFramePosition = transform.position; // remember object position in current frame
            yield return new WaitForEndOfFrame();
            float rayLength = Vector3.Distance(transform.position, previousFramePosition); // calculate distance object passed through
            Vector3 direction = previousFramePosition - transform.position; 
            Debug.DrawRay(transform.position, direction, Color.green);
            RaycastHit hit;
            if (Physics.Raycast(previousFramePosition, Vector3.forward, out hit, rayLength)) // cast a ray forward from object poition in previous frame.
            {
                if(hit.collider.gameObject != gameObject)
                {
                    Debug.Log(hit.collider.gameObject.name); // do things
                    Death();
                }
            }
        }
    }

    private void Death()
    {
        Debug.Log("Impacted rigid surface");
        Destroy(gameObject);
    }
}
