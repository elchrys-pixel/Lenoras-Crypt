using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleBats : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Transform startingPoint;
    public bool chase = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if (chase == true)
            Chase();
        else
            ReturnStartPos();

        trigger();
    }

    public void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void ReturnStartPos()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    private void trigger()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
