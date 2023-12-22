using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;
    private Animator anim;
    private Rigidbody2D rb;

    // Number of lives
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    //Take hearts
    [SerializeField] private HealthController _healthController;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentLives = maxLives;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Bullet"))

        {
            deathSoundEffect.Play();

            Die();
            _healthController.playerHealth--;
            _healthController.UpdateHealth();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            deathSoundEffect.Play();
            Die();
            _healthController.playerHealth--;
            _healthController.UpdateHealth();
        }
    }

    public void Die()
    {
        currentLives--;

        if(currentLives > 0)
        {

        }else
        {
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }

            anim.SetTrigger("death");

        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
