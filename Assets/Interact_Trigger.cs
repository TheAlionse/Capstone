using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Trigger : MonoBehaviour
{
    public GameObject textDisplay;
    void OnTriggerEnter2D(Collider2D other)
    {
        textDisplay.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        textDisplay.SetActive(false);
    }
}
