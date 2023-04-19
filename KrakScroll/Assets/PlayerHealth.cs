using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public bool isInAcid;
    public int acidDamage;

    private int acidTimer, maxAcidTime = 50;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (isInAcid)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            if (acidTimer < maxAcidTime) acidTimer++;
            else
            {
                TakeDamage(acidDamage);
                acidTimer = 0;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 2f;
            acidTimer = 0;
        }
    }

    public void AddHealth(int healAmount)
    {
        if (health + healAmount > maxHealth) health = maxHealth;
        else health += healAmount;
    }

    public void TakeDamage(int damageAmount)
    {
        if (health - damageAmount > 0) health -= damageAmount;
        else
        {
            health = 0;
            OnDeath();
        }
    }

    private void OnDeath()
    {
        GameManager.ResetLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello Acid!");
        if (collision.gameObject.name.Contains("Acid")) isInAcid = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Acid")) isInAcid = false;
    }
}
