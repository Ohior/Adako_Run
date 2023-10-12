using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBG : MonoBehaviour
{
    private float scrollSpeed = 0.05f;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private new Rigidbody2D rigidbody2D;


    // Update is called once per frame
    private void Update()
    {
        if (rigidbody2D != null && rigidbody2D.velocity != Vector2.zero)
        {
            Vector2 vector2 = new Vector2(scrollSpeed * Time.deltaTime, 0f);
            renderer.material.mainTextureOffset += vector2;
        }
    }
}
