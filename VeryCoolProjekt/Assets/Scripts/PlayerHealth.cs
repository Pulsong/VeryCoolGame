using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField] Animator anim;
    PlayerMovement playerMov;


    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        playerMov = GetComponent<PlayerMovement>();



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
            FindObjectOfType<AudioManager>().Play("Player_hurt");
            if (healthBar.slider.value <= 0)
            {
                PlayerDeath();
            }
        }

        void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        void PlayerDeath()
        {

            anim.SetTrigger("Death");
            playerMov.enabled = false;

        }

    }
}
