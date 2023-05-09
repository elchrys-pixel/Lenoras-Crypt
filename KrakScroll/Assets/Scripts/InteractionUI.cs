using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public bool canUse;
    public GameObject interactionBox;

    private void Update()
    {
        if (interactionBox != null)
        {
            if (canUse)
            {
                if (!interactionBox.activeSelf) interactionBox.SetActive(true);
            }
            else if (interactionBox.activeSelf) interactionBox.SetActive(false);
        }
    }
}
