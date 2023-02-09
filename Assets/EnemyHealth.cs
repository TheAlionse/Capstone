using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;

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
