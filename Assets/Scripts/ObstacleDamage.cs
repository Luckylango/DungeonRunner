using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        { 
            playerHealth.TakeDamage(1);
        }
    }

}
