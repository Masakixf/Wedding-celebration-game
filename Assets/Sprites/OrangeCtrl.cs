using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening; //DOTweenのインポート
public class OrangeCtrl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private GameObject image;
    //public int image_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        Sound.LoadSe("appleSE", "goku"); //第一引数が再生するためのキー（つまりID）、第二引数が実際のリソース名
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){ 
        if(col.gameObject.name == "Player"){ //Appleがプレイヤーに触れたら
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            GetComponent<BoxCollider2D>().enabled = false;

            Vector3 pos = transform.localPosition + Vector3.up * 7f; //現在位置に上方向のベクトルをプラス
            transform.DOLocalMove(pos, 0.5f); //posの場所まで0.5秒かけて移動
            transform.DORotate(new Vector3(0, 180, 0), 1.0f); //1秒かけてyを180度回転
            spriteRenderer.DOFade(0, 1.5f); //1.5秒かけてα値(透明度)が0になる

            
            GameObject player = GameObject.Find ("Player"); //Playerのゲームオブジェクトの呼び出し
            PlayerCtrl script = player.GetComponent<PlayerCtrl>(); //PlayerのゲームオブジェクトについているPlayerCtrl.csの呼び出し

            if(script.image_count >= 1){ //1つ以上右上に格納されている文字がある場合
                Sound.PlaySe("appleSE", 0); //効果音再生
                for(int i = script.image_count; i > 0; --i){
                    //Debug.Log(i.ToString());
                    //Debug.Log("aaaa");
                    string image_str = "Image" + (i-1).ToString();
                    image = GameObject.Find(image_str);
                    image.SendMessage("deleteChar"); //末尾の文字を消す
                }
                script.image_count = 0; //PlayerCtrl.csの中の変数image_countの値を0にする  
            }
            Invoke("DestroyObject", 2); //2秒後にDestroyObject関数を実行
        }

        if(col.gameObject.name == "Tilemap"){
            Invoke("DestroyObject", 3); //2秒後にDestroyObject関数を実行
        }
    }

    void DestroyObject(){
        Destroy(this.gameObject); //Appleが消える
    }
}