using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnTigger : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string yarnTitle;
    public bool turnOff;

    private void Start()
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        }

    private void OnTriggerEnter2D(Collider2D other) {
        dialogueRunner.StartDialogue(yarnTitle);
        if(turnOff){
            gameObject.SetActive(false);
        }
    }
}
