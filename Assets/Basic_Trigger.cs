using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Trigger : MonoBehaviour
{
    public float amount;
    public string functionCall;
    public bool destroyOnEnter;
    private void OnTriggerEnter2D(Collider2D other) {
        other.SendMessage(functionCall, amount);
        if(destroyOnEnter){
            //TODO: Destroy or setActive ?
            Destroy(gameObject);
        }
    }
}
