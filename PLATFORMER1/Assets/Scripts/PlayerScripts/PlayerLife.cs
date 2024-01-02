using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;
    private Animator anim;
    private Animator bloodAnim;
    private Rigidbody2D rb;

    // Number of lives
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float resetLevelTime = 0.5f;
    private int currentLives;

    //Take hearts
    [SerializeField] private HealthController _healthController;

    public UnityEvent <GameObject> OnDamageReference, OnDeathReference;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        Transform bloodObject = transform.Find("Blood");
        bloodAnim = bloodObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentLives = maxLives;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Bullet"))
        {
            GameObject other = collision.gameObject;
            takeDamage(other);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            takeDamage(other);
        }
    }

    public void Die()
    {
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        anim.SetTrigger("death");
        StartCoroutine(restartLevel());
    }

    private void takeDamage(GameObject other)
    {
        _healthController.playerHealth--;
        _healthController.UpdateHealth();
        currentLives--;
        if(currentLives <= 0)
        {
            Die();
        }
        OnDamageReference?.Invoke(other);
        bloodAnim.SetTrigger("PlayBlood");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator restartLevel()
    {
        yield return new WaitForSeconds(resetLevelTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
