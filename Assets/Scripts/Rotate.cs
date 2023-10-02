using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    // Update is called once per frame
    private void Update()
    {
        // transform.Translate(Vector2.right * (speed * Time.deltaTime));
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
        // transform.Rotate(Vector3.forward * speed * Time.deltaTime);

    }
}
