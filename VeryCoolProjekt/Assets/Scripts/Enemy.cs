using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float reset = 5f;
    private bool UpTimer;
    [SerializeField]
    private bool GetUp;
    public ParticleSystem PartiSys;
    private NavMeshAgent navmeshagent;
    private GameObject Player;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grabbable"))
        {
            FindObjectOfType<AudioManager>().Play("BloodSplash");
            DoRagDoll();
            UpTimer = true;
            FindObjectOfType<AudioManager>().Play("Collision_cat");
            Debug.Log("Collision with grabbable");
            PartiSys.Play();
 

        }
    }
    void Awake()
    {
        UpTimer = false;
        GetUp = false;
        
    }
    private void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void DoRagDoll()
    {
        GetComponent<Animator>().enabled = false;
    }
    private void Update()
    {
        navmeshagent.destination = Player.transform.position;
        if (UpTimer == true)
        {
        reset -= Time.deltaTime;
        if (reset < 0)
        {
                reset = 5;
                UpTimer = false;
                GetUp = true;
        }

        }


        if (GetUp == true)
        {
            GetComponent<Animator>().enabled = true;
            Quaternion target = Quaternion.Euler(0, 0, 0);
            GetUp = false;
        }

    }


}
