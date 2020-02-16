using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject followObject;
    [Range(-10f, 10f)]
    public float xCameraCenter, yCameraCenter;
    public GameObject screenLimitLeft, screenLimitRight;

    bool won;

    public void Won()
    {
        won = true;
    }

    float xLimitLeft, xLimitRight;
    // Start is called before the first frame update
    void Awake()
    {
        if (followObject == null)
        {
            followObject = GameObject.FindGameObjectWithTag("Player");
        }

        xLimitLeft = screenLimitLeft.transform.position.x;
        xLimitRight = screenLimitRight.transform.position.x;
        won = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (won) return;
        float newX = followObject.transform.position.x + xCameraCenter,
              newY = yCameraCenter;
        newX = Mathf.Clamp(newX, xLimitLeft, xLimitRight);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
