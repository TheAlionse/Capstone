using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Probably put this into an interface if we continue to expand

public class EnemyHealth : MonoBehaviour
{
    public int health;
    //if death of enemy relates to door or something else
    public GameObject tiedObject;

    private SpriteRenderer mySprite;
    private Color defaultColor;
    private void Start() {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        defaultColor = mySprite.color;
    }

    public void takeDamge(int damage){
        health -= damage;
        Debug.Log("took damage " + damage);
        StartCoroutine(damageEffect());
        if(health <= 0){
            if(tiedObject != null){
                tiedObject.SendMessage("CheckToOpen", gameObject);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator damageEffect(){
        mySprite.color = Color.red;
        yield return new WaitForSeconds(.2f);
        mySprite.color = defaultColor;
        yield return null;
    }

}
