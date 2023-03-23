using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    
    public void Awake() {

        dialogueRunner.AddCommandHandler<float>(
            "change_time",       // the name of the command
            changeTime          // the method to run
        );
    }

    public static void changeTime(float scale){
        Time.timeScale = scale;
    }
        
}
