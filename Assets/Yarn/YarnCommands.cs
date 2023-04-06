using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    
    public void Awake() {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.AddCommandHandler<float>("change_time", changeTime);
        dialogueRunner.AddCommandHandler<string, bool>("set_hitbox", setHitbox);
    }

    public static void changeTime(float scale){
        Time.timeScale = scale;
    }
    public static void setHitbox(string objName, bool active){
        GameObject.Find(objName).GetComponent<Collider2D>().enabled = active;
    }
}
