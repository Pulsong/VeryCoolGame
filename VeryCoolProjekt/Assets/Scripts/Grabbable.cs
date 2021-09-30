using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public ParticleSystem DustPartiSys;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            FindObjectOfType<AudioManager>().Play("RockGround");
            Debug.Log("Collision with grabbable");
            DustPartiSys.transform.position = gameObject.transform.position;
            DustPartiSys.Play();
            

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
