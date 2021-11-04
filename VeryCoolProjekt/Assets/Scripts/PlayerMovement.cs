using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float Speed = 3;
    [SerializeField] float RotationSpeed = 1f;
    [SerializeField] float jumpForce = 4f;
    Rigidbody rb;
    public Animator anim;
    public float distanceToGround = 4f;

    public Text debugtext;
    float mouseX;

    private bool isGrounded = false;


    private void Start()
    {
        // Lukitsee ja piilottaa hiiren play tilassa.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
    }
    private void Update()
    {

        Movement();
        Jump();
        if (isGrounded == false)
        {
            rb.velocity = new Vector3(0, -2f, 0);
        }

    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f))
        {
            isGrounded = true;
            debugtext.text = "Grounded";
        }
        else
        {
            isGrounded = false;
            debugtext.text = "not Grounded";

        }
    }

    void Movement()
    {
        mouseX = Input.GetAxis("Mouse X");
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playermove = new Vector3(hor, 0f, ver).normalized * Speed * Time.deltaTime; //Z AND X AXIS MOVEMENT NORMALIZED SO THEY ARE ALLWAYS 1 AND DONT MOVE OVER IT. 
        
        transform.Translate(playermove, Space.Self);
        


        transform.Rotate(Vector3.up, mouseX * RotationSpeed * RotationSpeed * Time.deltaTime); // Player can look and rotate with mouse sideways.
        if (playermove.x != 0 || playermove.z != 0)
        {
            //Running
            anim.SetTrigger("Running");

        }
        else
        {
            anim.SetTrigger("Idle");
            //idle
        }

    }
    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, jumpForce, 0);

            anim.SetTrigger("Jumpping");
            FindObjectOfType<AudioManager>().Play("jumpSound");
        }
    }
    


}
