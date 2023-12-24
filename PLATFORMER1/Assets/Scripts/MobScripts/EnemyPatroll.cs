using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidbody;
    BoxCollider2D myboxCollider;
    private bool isMovementEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myboxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovementEnabled)
        {
            if (IsFacingRight())
            {
                myRigidbody.velocity = new Vector2(moveSpeed, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
            }
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isMovementEnabled && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
        }
    }

    // Function to stop movement for a specified duration
    public void StopMovementForSeconds(float duration)
    {
        StartCoroutine(StopMovementCoroutine(duration));
    }

    // Coroutine to stop movement for a specified duration
    private IEnumerator StopMovementCoroutine(float duration)
    {
        isMovementEnabled = false;
        yield return new WaitForSeconds(duration);
        isMovementEnabled = true;
    }
}
