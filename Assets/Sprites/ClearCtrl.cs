using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //UIのインポート
using DG.Tweening; //DOTweenのインポート
using UnityEngine.SceneManagement;
public class ClearCtrl : MonoBehaviour
{
    public GameObject serihu;
    public GameObject serihu_self;
    public GameObject seikai;
    public GameObject seikai_self;
    public GameObject shinkuro;
    public GameObject shinkuro_self;
    public GameObject final_comment;
    public GameObject final_comment_self;
    public GameObject nacchan;
    public GameObject press_enter;

    public string serihu_str;

    private RectTransform fadeRectT;
    private Image image;
    private GameObject player;
    private PlayerCtrl script;

    private bool isFinish = false;
    private bool isFinish2 = false;
    private GameObject[] tag_char;
    //private string[] tag_char_str;
    private string[] tag_char_str = new string[7];
    private int ok = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find ("Player"); //Playerのゲームオブジェクトの呼び出し
        script = player.GetComponent<PlayerCtrl>(); //PlayerのゲームオブジェクトについているPlayerCtrl.csの呼び出し

        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);//α値を0に

        fadeRectT = GetComponent<RectTransform>(); //Fadeの画像の情報を取得
        fadeRectT.DOScale(new Vector3(1, 0, 1), 1.5f).SetEase(Ease.InOutQuint);

        seikai.SetActive(false);
        seikai_self.SetActive(false);
        serihu.SetActive(false);
        serihu_self.SetActive(false);
        shinkuro.SetActive(false);
        shinkuro_self.SetActive(false);
        final_comment.SetActive(false);
        final_comment_self.SetActive(false);
        nacchan.SetActive(false);
        press_enter.SetActive(false);

        tag_char = GameObject.FindGameObjectsWithTag("char");

        Sound.LoadSe("bashiSE", "bashi");
        

    }

    // Update is called once per frame
    void Update()
    {
        if(script.isGoal & !isFinish){
            isFinish = true;
            fadeOut();

            for (int i = 0; i < tag_char.Length; i++){
                if(tag_char[i].GetComponent<Image>().sprite.name == "ketsu 1_0"){
                    tag_char_str[i] = "結";
                    if(i==0){
                        ok += 1;
                    }
                }
                if(tag_char[i].GetComponent<Image>().sprite.name == "kon_0"){
                    tag_char_str[i] = "婚";
                    if(i==1){
                        ok += 1;
                    }
                }
                if(tag_char[i].GetComponent<Image>().sprite.name == "o_0"){
                    tag_char_str[i] = "お";
                    if(i==2){
                        ok += 1;
                    }
                }
                if(tag_char[i].GetComponent<Image>().sprite.name == "me_0"){
                    tag_char_str[i] = "め";
                    if(i==3){
                        ok += 1;
                    }
                }
                if(tag_char[i].GetComponent<Image>().sprite.name == "de_0"){
                    tag_char_str[i] = "で";
                    if(i==4){
                        ok += 1;
                    }
                }
                if(tag_char[i].GetComponent<Image>().sprite.name == "to_0"){
                    tag_char_str[i] = "と";
                    if(i==5){
                        ok += 1;
                    }
                }
                if(tag_char[i].GetComponent<Image>().sprite.name == "u_0"){
                    tag_char_str[i] = "う";
                    if(i==6){
                        ok += 1;
                    }
                }
            }
            StartCoroutine(desplayText(tag_char_str, ok));
        }

        if (Input.GetKey(KeyCode.Return) & isFinish2){
            SceneManager.LoadScene("Main"); //シーンの再読み込み
        }
    }

    void fadeOut(){ //ゲーム開始直後、画面をフェードインさせる
        //Debug.Log("saiko");
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255f);//α値を255に
        //画像を1.5秒かけてVector3(1,0,1)の大きさにする。SetEase(Ease.InOutQuint)でイージングを使ってフェードインの演出を変更
        fadeRectT.DOScale(new Vector3(1, 1, 1), 1.5f).SetEase(Ease.InOutQuint); 
    }

    IEnumerator desplayText(string[] str, int ok){
        yield return new WaitForSeconds(2.0f);
        seikai.SetActive(true);
        seikai_self.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < str.Length; i++){
            if(i == 0){
                serihu_str = str[i];
            }
            else{
                serihu_str += str[i];
            }
        }
        Debug.Log(ok.ToString());
        serihu_self.GetComponent<Text>().text = serihu_str;
        float ok_pa = (float)ok / 7f * 100f;
        Debug.Log(ok_pa.ToString());
        int ok_p = (int)ok_pa;
        shinkuro_self.GetComponent<Text>().text =  ok_p.ToString() + "%";
        

        serihu.SetActive(true);
        serihu_self.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        shinkuro.SetActive(true);
        shinkuro_self.SetActive(true);

        if(ok == 0){
            final_comment_self.GetComponent<Text>().text = "ちょびっと";
        }
        if(ok == 1){
            final_comment_self.GetComponent<Text>().text = "若干";
        }
        if(ok == 2){
            final_comment_self.GetComponent<Text>().text = "まあまあ";
        }
        if(ok == 3){
            final_comment_self.GetComponent<Text>().text = "そこそこ";
        }
        if(ok == 4){
            final_comment_self.GetComponent<Text>().text = "普通に";
        }
        if(ok == 5){
            final_comment_self.GetComponent<Text>().text = "かなり";
        }
        if(ok == 6){
            final_comment_self.GetComponent<Text>().text = "めちゃめちゃ";
        }
        if(ok == 7){
            final_comment_self.GetComponent<Text>().text = "本当に";
        }
        yield return new WaitForSeconds(1.0f);
        nacchan.SetActive(true);
        Sound.PlaySe("bashiSE", 0);
        yield return new WaitForSeconds(1.0f);
        final_comment_self.SetActive(true);
        Sound.PlaySe("bashiSE", 0);
        yield return new WaitForSeconds(1.0f);
        final_comment.SetActive(true);
        Sound.PlaySe("bashiSE", 0);
        yield return new WaitForSeconds(1.5f);
        press_enter.SetActive(true);
        isFinish2 = true;


    }
}
