using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TimeCtrl : MonoBehaviour
{
    public Text timerText;
	public float totalTime;
	private int seconds;
    private bool timerZero = false;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!timerZero){ //カウントが0でないとき
            totalTime -= Time.deltaTime;
            seconds = (int)totalTime;
            timerText.text= seconds.ToString();
        }
        
        if(seconds == 0 & !isDead){ //カウントが0のとき
            timerZero = true;
            isDead = true;
            GameObject player = GameObject.Find ("Player"); //Playerのゲームオブジェクトの呼び出し
            PlayerCtrl playerScript = player.GetComponent<PlayerCtrl>(); //PlayerのゲームオブジェクトについているPlayerCtrl.csの呼び出し
            playerScript.StartCoroutine("Dead"); //プレイヤーが死亡
        }
    }
}
