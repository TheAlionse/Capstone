using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Probably put this into an interface if we continue to expand

public class EnemyHealth : MonoBehaviour
{
    public int health;
    //if death of enemy relates to door or something else
    public List<GameObject> tiedObject;

    public bool respawnMe;
    public float respawnTimer;

    private SpriteRenderer mySprite;
    private Color defaultColor;
    private EnemyTracker myET;
    private int curHealth;
    
    private Color myRed = new Color(.5f, .0f, .0f);

    private void Start() {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        defaultColor = mySprite.color;
        myET = FindObjectOfType<EnemyTracker>();
        curHealth = health;
    }

    private void Awake() {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        defaultColor = mySprite.color;
        myET = FindObjectOfType<EnemyTracker>();
        curHealth = health;
    }

    private void OnEnable(){
        curHealth = health;
        mySprite.color = defaultColor;
    }

    public void takeDamge(int damage){
        curHealth -= damage;
        Debug.Log("took damage " + damage);
        StartCoroutine(damageEffect());
        if(curHealth <= 0){
            if(tiedObject != null){
                foreach(GameObject tObj in tiedObject)
                    tObj.SendMessage("CheckToOpen", gameObject);
            }
            //Destroy(gameObject);
            myET.activeEnemies.Remove(gameObject);
            if(respawnMe){
                myET.respawnWait(gameObject, respawnTimer);
            }     
            gameObject.SetActive(false);   
        }
    }

    IEnumerator damageEffect(){
        mySprite.color = myRed;
        yield return new WaitForSeconds(.2f);
        mySprite.color = defaultColor;
        yield return null;
    }

}
