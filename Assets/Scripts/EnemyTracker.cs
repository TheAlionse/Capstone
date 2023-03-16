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

    //Have a coroutine that loops through all the enemies and has then move
    //then it is not bound by frames
}
