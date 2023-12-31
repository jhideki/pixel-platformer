using System.Collections;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private Camera mainCamera;
    public float choke;
    public float scrollSpeed;

    private float objectWidth;

    void Start()
    {
        objectWidth = levels[0].GetComponent<SpriteRenderer>().bounds.size.x - choke;
        foreach (GameObject obj in levels)
        {
            LoadChildObjects(obj);
        }
    }

    void LoadChildObjects(GameObject obj)
    {
        int childsNeeded = (int)Mathf.Ceil(mainCamera.orthographicSize * 2 / objectWidth);
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(obj, new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z), Quaternion.identity, obj.transform);
            c.name = obj.name + i;
        }
    }

    void RepositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        Transform firstChild = children[1];
        Transform lastChild = children[children.Length - 1];
        float halfObjectWidth = objectWidth / 2;

        if (transform.position.x + mainCamera.orthographicSize > lastChild.position.x + halfObjectWidth)
        {
            firstChild.SetAsLastSibling();
            firstChild.position = new Vector3(lastChild.position.x + objectWidth, lastChild.position.y, lastChild.position.z);
        }
        else if (transform.position.x - mainCamera.orthographicSize < firstChild.position.x - halfObjectWidth)
        {
            lastChild.SetAsFirstSibling();
            lastChild.position = new Vector3(firstChild.position.x - objectWidth, firstChild.position.y, firstChild.position.z);
        }
    }

    void Update()
    {
        transform.Translate(new Vector3(scrollSpeed * Time.deltaTime, 0, 0));
    }

    void LateUpdate()
    {
        foreach (GameObject obj in levels)
        {
            RepositionChildObjects(obj);
        }
    }
}
