using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Trigger : MonoBehaviour
{
    public GameObject textDisplay;
    public string diaStart;
    private GameObject myPlayer;

     private void Start()
    {
        myPlayer = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        textDisplay.SetActive(true);
        myPlayer.SendMessage("onDialogue", diaStart);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        textDisplay.SetActive(false);
        myPlayer.SendMessage("onDialogue", "");
    }
}
