using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    public int maxBullets = 3; // Set your maximum number of bullets here
    private int currentBullets;

    public Text bulletText; // Reference to the Text UI element

    // Start is called before the first frame update
    void Start()
    {
        currentBullets = maxBullets;
        UpdateBulletText();

    }

    // Update is called once per frame
    void UpdateBulletText()
    {
        if (bulletText != null)
        {
            bulletText.text = currentBullets.ToString() + "/" + maxBullets.ToString();
            Debug.Log("Updated bullet text: " + bulletText.text);
        }
    }

    public void ShootBullet()
    {
        Debug.Log(currentBullets);
        if (currentBullets > 0)
        {
            currentBullets--;
            Debug.Log("Current Bullets: " + currentBullets);
            UpdateBulletText();
            // Add your bullet shooting logic here
        }
    }
}
