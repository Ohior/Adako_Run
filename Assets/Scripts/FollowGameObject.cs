using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] private Transform gameObj;
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    // Update is called once per frame
    void Update()
    {
        float x = (followX) ? gameObj.position.x : transform.position.x;
        float y = transform.position.y;
        if (followY)
        {
            if (gameObj.position.y >= 0 && gameObj.position.y <= 5)
            {
                y = gameObj.position.y;
            }

        }
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
