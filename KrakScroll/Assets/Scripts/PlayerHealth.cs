using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public bool canSurviveAcid;
    public bool isInAcid;

    private void Update()
    {
        if (canSurviveAcid && isInAcid) GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        else GetComponent<Rigidbody2D>().gravityScale = 2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Acid"))
        {
            if (canSurviveAcid) isInAcid = true;
            else OnDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Kill"))
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        GameManager.ResetLevel();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Acid")) isInAcid = false;
    }
}
