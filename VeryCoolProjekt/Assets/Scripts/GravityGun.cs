using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;
    Rigidbody grabbedRB;

    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;


    private void Start()
    {
        
    }
    void Update()
    {

  
        var _ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, 100))
        {
            lastHit = _hit.transform.gameObject;
            collision = _hit.point;
        }


        if (grabbedRB)
        {
            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));

            if (Input.GetMouseButtonDown(1))
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                grabbedRB = null;
                FindObjectOfType<AudioManager>().Play("Swoosh");
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedRB)
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
   
                Debug.Log("RB arvo"+ grabbedRB);
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit,maxGrabDistance))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if (grabbedRB)
                    {
                    grabbedRB.isKinematic = true;

                    }
                }
            }
        }

    }
}
