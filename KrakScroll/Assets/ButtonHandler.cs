using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private int maxPanTime;
    [SerializeField] private DoorHandler linkedDoor;
    [SerializeField] private SpriteRenderer button;
    [SerializeField] private InteractionUI interactionUI;

    private bool playerInRange;
    private bool activateDoor;
    private int panTimer;

    private void Update()
    {
        if (playerInRange && !activateDoor)
        {
            interactionUI.canUse = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                button.transform.position += new Vector3(0.1f, 0, 0);
                button.color = Color.green;
                activateDoor = true;
            }
        }
        else interactionUI.canUse = false;

        if (activateDoor) ActivateDoor();
    }

    private void ActivateDoor()
    {
        if (panTimer < maxPanTime)
        {
            panTimer++;
            Camera.main.GetComponent<CameraFollow>().target = linkedDoor.transform;
            if (panTimer > maxPanTime / 3 && !linkedDoor.isOpen) linkedDoor.isOpen = true;
        }
        else
        {
            Camera.main.GetComponent<CameraFollow>().target = null;
            activateDoor = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            playerInRange = false;
        }
    }
}
