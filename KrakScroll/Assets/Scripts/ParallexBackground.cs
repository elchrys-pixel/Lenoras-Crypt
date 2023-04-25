using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackground : MonoBehaviour
{
    private float length, startingpos;
    public GameObject Camera;
    public float ParaStrength;
    // Start is called before the first frame update
    void Start()
    {
        startingpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        float dist = Camera.transform.position.x * ParaStrength;
       

        transform.position = new Vector3(startingpos + dist, transform.position.y);
    }
}
