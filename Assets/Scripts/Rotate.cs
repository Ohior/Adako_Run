using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotateY = 0f;
    [SerializeField] private float rotateX = 0f;
    // Update is called once per frame
    private void Update()
    {
        // transform.Translate(Vector2.right * (speed * Time.deltaTime));
        transform.Rotate(
            360 * Rotating(rotateX),
            360 * Rotating(rotateY),
             360 * Rotating(speed)
             );
        // transform.Rotate(Vector3.forward * speed * Time.deltaTime);

    }

    private float Rotating(float rotate) => rotate * Time.deltaTime;
}
