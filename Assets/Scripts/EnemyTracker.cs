using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public List<GameObject> activeEnemies;
    //public int checksPerFrame;

    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = new List<GameObject>();
        activeEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

    }

    //loop through all enemies that are all active then call move function
    void FixedUpdate()
    {
        foreach(GameObject ene in activeEnemies){
            //check if need to move
            //these enemies should all be in a class
            ene.SendMessage("eneMovement");
        }
    }

    public void respawnWait(GameObject respawner, float respawnTimer){
        StartCoroutine(respawnWaiter(respawner, respawnTimer));
    }

    public IEnumerator respawnWaiter(GameObject respawner, float respawnTimer){
        //Debug.Log("respawning " + respawner + respawnTimer);
        yield return new WaitForSeconds(respawnTimer);
        //Debug.Log("respawned");
        respawner.SetActive(true);
        activeEnemies.Add(respawner);
        //Debug.Log("end");
        yield return null;
    }
}
