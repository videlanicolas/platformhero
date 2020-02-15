using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject followObject;
    [Range(-10f, 10f)]
    public float xCameraCenter, yCameraCenter;
    public GameObject screenLimit;

    float xLimit;
    // Start is called before the first frame update
    void Awake()
    {
        if (followObject == null)
        {
            followObject = GameObject.FindGameObjectWithTag("Player");
        }

        xLimit = screenLimit.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = followObject.transform.position.x + xCameraCenter,
              newY = yCameraCenter;
        newX = Mathf.Clamp(newX, xLimit, Mathf.Infinity);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
