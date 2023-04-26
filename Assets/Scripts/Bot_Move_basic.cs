using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Move_basic : MonoBehaviour
{
    public float moveTime;
    public float moveSpeed;
    //starting left
    //going up/down

    private SpriteRenderer myRend;
    private Rigidbody2D myRigid;
    public bool goingRight = false;
    private float curTime;

    private void Start() {
        myRend = gameObject.GetComponent<SpriteRenderer>();
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        myRend.flipX = goingRight;
        if(!goingRight){
            moveSpeed = moveSpeed * -1;
        }
    }
    public void eneMovement(){
        //0 might change if we want them to move up and down
        curTime += Time.deltaTime;
        if (curTime >= moveTime){
            curTime -= moveTime;
            goingRight = !goingRight;
            myRend.flipX = goingRight;
            moveSpeed = moveSpeed * -1;
        }
        myRigid.velocity = new Vector2(moveSpeed, 0);
    }
}
