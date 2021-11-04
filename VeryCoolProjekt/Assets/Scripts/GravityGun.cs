using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;
    [SerializeField]
    private float positionMovementSpeed;
    Rigidbody grabbedRB;

    public GameObject lastHit;  // for getting the name of latest object that the ray hit
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;
    


    void Update()
    {

    //Test Ray
        var _ray = new Ray(cam.transform.position, cam.transform.forward);    // Takes camera position, and sends ray from it's Z location
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, 100))
        {
            lastHit = _hit.transform.gameObject;
            collision = _hit.point;
        }

        
        if (grabbedRB == true)
        {
                Debug.Log("Saattaa olla, että ei tapahdu");

                grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position, Time.deltaTime * positionMovementSpeed * lerpSpeed));  // Moves grabbed object to objectholders position
            

            if (Input.GetMouseButtonDown(1))    // Throws the grabbed object when pressed
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                grabbedRB = null;
                FindObjectOfType<AudioManager>().Play("Swoosh");
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) && tag == "Grabbable")     
        {
            if (grabbedRB == true)      // If Raycast hits Rigidbody collider set kinematic to false and all grabbedRB 
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
   
                Debug.Log("RB arvo"+ grabbedRB);
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

                if (Physics.Raycast(ray, out hit,maxGrabDistance) && hit.collider. gameObject.tag != "Player")
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();  // Gain grabbedRB:s Rigidbody by raycast:s collision.
                    if (grabbedRB == true)                                          // If Raycast hits Rigidbody collider set kinematic true
                    {
                    grabbedRB.isKinematic = true;

                    }
                }
            }
        }

    }//Update ends
}
