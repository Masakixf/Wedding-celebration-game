using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //UIのインポート
using DG.Tweening; //DOTweenのインポート
public class DFinishCtrl : MonoBehaviour
{
    private GameObject textObject;
    private Text textComponent;

    private GameObject textObject2;
    private Text textComponent2;
    private RectTransform fadeRectT;
    // Start is called before the first frame update
    void Start()
    {
        textObject = GameObject.Find("DeadMessage"); //GameOverの文字の画像を見つける
        textComponent = textObject.GetComponent<Text>();　//GameOverの文字の画像の情報を取得
        textComponent.enabled = false;　//最初は表示を消しておく

        textObject2 = GameObject.Find("RetryMessage"); //GameOverの文字の画像を見つける
        textComponent2 = textObject2.GetComponent<Text>();　//GameOverの文字の画像の情報を取得
        textComponent2.enabled = false;　//最初は表示を消しておく

        fadeRectT = GetComponent<RectTransform>(); //Fadeの画像の情報を取得

        fadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void fadeIn(){ //ゲーム開始直後、画面をフェードインさせる
        //画像を1.5秒かけてVector3(1,0,1)の大きさにする。SetEase(Ease.InOutQuint)でイージングを使ってフェードインの演出を変更
        fadeRectT.DOScale(new Vector3(1, 0, 1), 1.5f).SetEase(Ease.InOutQuint); 
    }
    void fadeOut(){ //ゲームクリア直後、画面をフェードアウトさせる
        fadeRectT.DOScale(new Vector3(1, 1, 1), 1.5f).SetEase(Ease.InOutQuint);
    }

    void displayTelop(){
        textComponent.enabled = true; //GameOverの文字を表示する
        textComponent2.enabled = true; //リトライの文字を表示する
    }

}
