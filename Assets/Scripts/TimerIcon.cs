using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerIcon : MonoBehaviour
{
    public Image uiFill;
    private float remainingTime;
    public float myDuration;

    private void OnEnable() {
        uiFill.fillAmount = 1;
        remainingTime = myDuration;
        StartCoroutine(timer(myDuration));
    }

    IEnumerator timer(float duration){
        while(remainingTime >= 0){
            uiFill.fillAmount = Mathf.InverseLerp(0, duration, remainingTime);
            remainingTime -= .1f;
            yield return new WaitForSeconds(.1f);
        }
        gameObject.SetActive(false);
    }
}
