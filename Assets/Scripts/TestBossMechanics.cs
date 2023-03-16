using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossMechanics : MonoBehaviour
{
    public float upTpDistance;
    public float leftTPDistance;

    private int curPos; //between 0-3
                        //3 2
                        //1 0
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        curPos = 0; 
        startPos = gameObject.transform.position;
        StartCoroutine(testBossCycle());
    }

    IEnumerator testBossCycle(){
        yield return null;
    }

    //move boss instantly to one of the 4 corners
    IEnumerator testBossMove(){
        int newPos = Random.Range(0,3);
        //TODO: Is there a better way to do this?
        switch (newPos){
            case 0:
                gameObject.transform.position = startPos;
                break;
            case 1:
                gameObject.transform.position = new Vector2(startPos.x - leftTPDistance, startPos.y);
                break;
            case 2:
                gameObject.transform.position = new Vector2(startPos.x, startPos.y + upTpDistance);
                break;
            case 3:
                gameObject.transform.position = new Vector2(startPos.x - leftTPDistance, startPos.y + upTpDistance);
                break;
        }
        yield return null;
    }

    IEnumerator testBossVerticalLaser(){
        yield return null;
    }
}
