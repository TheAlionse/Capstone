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
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("interacted");
            myPlayer.SendMessage("setRespawn", gameObject.transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        textDisplay.SetActive(false);
    }
}
