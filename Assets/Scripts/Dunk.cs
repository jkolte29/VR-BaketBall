using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Dunk : MonoBehaviour
{
    public UnityEvent OnDunk;
    public float count=0;
    public int bonus;
    public int timer=6;
    float t;
    public List<GameObject> balls;
    public TextMeshPro countTxt, timerTxt,bonusTxt;
    bool gameStarted=false;

    // Start is called before the first frame update
    private void Awake()
    {
        t = 0;
        count = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Grabable")
        {
            bonus =(t> (1/10)*timer)? Mathf.RoundToInt(Mathf.RoundToInt(t)*4/ timer) :0;
            count+=(1+(1 * bonus));
            gameStarted = true;
            OnDunk?.Invoke();
            bonusTxt.text = "Time Bonus: +" + bonus + "pts";
            // Destroy(other.gameObject);
            t = timer;
        }
    }
    private void Update()
    {
        timerTxt.text = string.Format("Count down: {0:#.00}", t);
        countTxt.text = string.Format("Score: {0:#.00}", count);
        
        if(!gameStarted )
        {
            return;
        }
        if (t <= 0&& gameStarted)
        {
            bonusTxt.text = "Time Bonus: -1pt"; ;
            count = count - 1;
            t = timer;
            return;
        }
            
        t -= Time.deltaTime;
        
    }
}
