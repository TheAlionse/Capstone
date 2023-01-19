using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int maxHealth;

    private void Start() {
        health = maxHealth;
    }

    public void takeDamage(int dmg){
        health -= dmg;
        if(health <= 0){
            Debug.Log("dead");
        }
    }

    public void healing(int heal){
        health += heal;
        if(health > maxHealth){
            health = maxHealth;
        }
    }
}
