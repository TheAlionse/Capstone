using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public List<GameObject> activeEnemies;
    public int checksPerFrame;

    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = new List<GameObject>();
    }

    //loop through all enemies that are all active then call move function
    void Update()
    {
        
    }
}
