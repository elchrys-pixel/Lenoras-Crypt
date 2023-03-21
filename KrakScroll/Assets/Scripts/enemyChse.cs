using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChse : MonoBehaviour
{
    public obstacleBats[] enemyArray;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (obstacleBats enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (obstacleBats enemy in enemyArray)
            {
                enemy.chase = false;
            }
        }
    }

}