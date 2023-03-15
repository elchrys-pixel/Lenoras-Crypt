using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutHandler : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private int destroyDistance;
    [SerializeField] private int forceFadeTimer;

    private Rigidbody2D rb;
    private bool startFade;
    private int forceFade;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        float distanceToCamera = Vector2.Distance(transform.position, Camera.main.transform.position);
        if (distanceToCamera > destroyDistance) Destroy(gameObject);
        else if (startFade)
        {
            Color colour = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            colour.a -= fadeSpeed;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = colour;

            if (colour.a <= 0) Destroy(gameObject);
        }
        else
        {
            if (rb != null && rb.velocity.x == 0) startFade = true;
            if (forceFade < forceFadeTimer) forceFade++;
            else startFade = true;
        }
    }
}
