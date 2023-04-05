using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;
    public float scrollSpeed;

    void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in levels)
        {
            loadChildObjects(obj);
        }
    }
    void loadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;

        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);

        childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectWidth);
        GameObject clone2 = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone2) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone2);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }
    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }
    void Update()
    {

        Vector3 velocity = Vector3.zero;
        Vector3 desiredPosition = transform.position + new Vector3(scrollSpeed, 0, 0);
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f);
        transform.position = smoothPosition;

    }
    void LateUpdate()
    {
        foreach (GameObject obj in levels)
        {
            repositionChildObjects(obj);
        }
    }
}



