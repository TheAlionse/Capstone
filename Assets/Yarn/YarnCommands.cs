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
    }

    public static void changeTime(float scale){
        Time.timeScale = scale;
    }
        
}
