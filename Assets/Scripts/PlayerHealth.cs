using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;

    public int maxHealth;
    public Vector3 respawnPoint;
    public GameObject respawnUI;
    public float invulnTime;

    private GameObject healthBarParent;
    private GameObject curHealthBar;
    private SpriteRenderer mySpriteRender;
    private Color defaultColor;

    private void Start() {
        health = maxHealth;
        healthBarParent = GameObject.FindGameObjectWithTag("HealthBar");
        curHealthBar = healthBarParent.transform.GetChild(0).gameObject;
        mySpriteRender = gameObject.GetComponent<SpriteRenderer>();
        defaultColor = mySpriteRender.color;

        healthBarParent.transform.localScale = new Vector3(maxHealth * 12, 50,0);
        curHealthBar.transform.localScale = new Vector3(health/maxHealth, 1, 0);
    }

    public void takeDamage(int dmg){
        health -= dmg;
        updateCurHpBar();
        if(health <= 0){
            health = 0;
            Time.timeScale = 0;
            StopCoroutine("playerGotHit");
            hitEffects(true);
            Debug.Log("dead");
            PlayerController.readInput = false;
            respawnUI.SetActive(true);
        }
        else{
            StartCoroutine("playerGotHit");
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
        Time.timeScale = 1;
        healing(maxHealth);
        gameObject.transform.position = respawnPoint;
        PlayerController.readInput = true;
        respawnUI.SetActive(false);
        //make sure things called in StopCoroutine are reset
        hitEffects(false);
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

    public void setRespawn(Vector3 newRespawnPos){
        Debug.Log("called");
        respawnPoint = newRespawnPos;
        healing(maxHealth);
    }

    IEnumerator playerGotHit(){
        hitEffects(true);
        yield return new WaitForSeconds(invulnTime);
        hitEffects(false);
        yield return false;
    }

    private void hitEffects(bool gotHit){
        if(gotHit){
            mySpriteRender.color = Color.red;
            gameObject.layer = 10;
        }
        else{
            mySpriteRender.color = defaultColor;
            gameObject.layer = 8;
        }
    }
}
