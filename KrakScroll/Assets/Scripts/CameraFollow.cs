using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 offSet;
    public float followSpeed;

    private void Update()
    {
        if (target == null) target = GameObject.Find("Player").transform;

        Vector3 relativePlayerPos = new Vector3(target.position.x + offSet.x, target.position.y + offSet.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, relativePlayerPos, followSpeed * Time.deltaTime);
    }
}
