using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PassThrough : MonoBehaviour
{
    private Collider2D platformCollider;
    public bool onPlatform;

    [SerializeField] private Object player;
    private PlayerMovement2 movementScript;
    private PlatformEffector2D platEffector;
    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<TilemapCollider2D>();
        movementScript = player.GetComponent<PlayerMovement2>();
        platEffector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onPlatform && movementScript.isCrouching)
        {

            platformCollider.enabled = false;
            StartCoroutine(EnableCollider());
        }

        if (movementScript.isDashing)
        {
            platformCollider.enabled = true;
            platEffector.useOneWay = false;
        }
        else
        {
            platEffector.useOneWay = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onPlatform = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onPlatform = false;
    }

    private IEnumerator EnableCollider()
    {

        yield return new WaitForSeconds(0.2f);

        platformCollider.enabled = true;
    }
}
