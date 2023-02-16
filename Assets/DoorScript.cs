using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public List<GameObject> enemyList;
    
    public void CheckToOpen(GameObject killedEnemy){
        if(enemyList.Contains(killedEnemy)){
                enemyList.Remove(killedEnemy);
            }
        if(enemyList.Count == 0){
            gameObject.SetActive(false);
        }
    }
}
