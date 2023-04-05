using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 offSet;
    public float followSpeed;

    private void Update()
    {
        Vector3 relativePlayerPos = new Vector3(player.position.x + offSet.x, player.position.y + offSet.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, relativePlayerPos, followSpeed * Time.deltaTime);
    }
}
