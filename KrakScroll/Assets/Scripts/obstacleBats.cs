using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBats : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Transform startingPoint;
    public bool chase = false;

    private Vector3 targetPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        else
        {
            if (chase == true) Chase();
            else ReturnStartPos();

            if (Vector3.Distance(transform.position, targetPos) > 0.2f)
            {
                RotateToFace();

                Vector3 velocity = targetPos - transform.position;
                velocity.z = 0;
                GetComponent<Rigidbody2D>().velocity = velocity.normalized * speed * Time.deltaTime;
            }
            else GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    public void Chase()
    {
        targetPos = player.transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void ReturnStartPos()
    {
        targetPos = startingPoint.position;
        //transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    private void RotateToFace()
    {
        if (transform.position.x > targetPos.x) transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
