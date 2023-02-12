using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int maxHealth;
    public Vector3 respawnPoint;
    public GameObject respawnUI;
    private GameObject healthBarParent;
    private GameObject curHealthBar;

    private void Start() {
        health = maxHealth;
        healthBarParent = GameObject.FindGameObjectWithTag("HealthBar");
        curHealthBar = healthBarParent.transform.GetChild(0).gameObject;

        healthBarParent.transform.localScale = new Vector3(maxHealth * 12, 50,0);
        curHealthBar.transform.localScale = new Vector3(health/maxHealth, 1, 0);
    }

    public void takeDamage(int dmg){
        health -= dmg;
        updateCurHpBar();
        if(health <= 0){
            Debug.Log("dead");
            PlayerController.readInput = false;
            respawnUI.SetActive(true);
        }
    }

    public void healing(int heal){
        health += heal;
        if(health > maxHealth){
            health = maxHealth;
        }
        updateCurHpBar();
    }

    //TODO: What else needs to happen when player respawns?
    //TODO: Maybe those should happen in the button
    public void respawnPlayer(){
        healing(maxHealth);
        gameObject.transform.position = respawnPoint;
        PlayerController.readInput = true;
        respawnUI.SetActive(false);
    }

    public void maxHealthUp(int increase){
        maxHealth += increase;
        healing(increase);
        updateMaxHPBar();
    }

    private void updateCurHpBar(){
        curHealthBar.transform.localScale = new Vector3(Mathf.Max(health / (float)maxHealth, 0), 1, 0);
    }

    private void updateMaxHPBar(){
        healthBarParent.transform.localScale = new Vector3(maxHealth * 12, 50,0);
    }
}
