using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public GameObject[] activeEnemies;
    //public int checksPerFrame;

    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    //loop through all enemies that are all active then call move function
    void Update()
    {
        foreach(GameObject ene in activeEnemies){
            //call movement function
        }
    }
}
