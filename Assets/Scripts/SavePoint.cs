using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private GameObject myPlayer;
    public GameObject textDisplay;
    private void Start()
    {
        myPlayer = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        textDisplay.SetActive(true);
        myPlayer.SendMessage("changeSave", true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        textDisplay.SetActive(false);
        myPlayer.SendMessage("changeSave", false);
    }
}
