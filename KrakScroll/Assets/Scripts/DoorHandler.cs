using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public bool isOpen;

    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject door;
    [SerializeField] private Transform openPos;
    [SerializeField] private Transform closedPos;

    private void Update()
    {
        Vector3 targetPos = new Vector3();
        if (isOpen) targetPos = openPos.position;
        else targetPos = closedPos.position;
        door.transform.position = Vector3.Lerp(door.transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
