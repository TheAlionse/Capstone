using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;

    public void takeDamge(int damage){
        health -= damage;
        Debug.Log("took damage " + damage);
        if(health <= 0){
            Destroy(gameObject);
        }
    }

}
