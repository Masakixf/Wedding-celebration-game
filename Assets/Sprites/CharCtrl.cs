using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening; //DOTweenのインポート

public class CharCtrl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private GameObject image;
    private GameObject player;
    private PlayerCtrl script;
    private int img_count;
    //public int image_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        Sound.LoadSe("getSE", "coin03"); //第一引数が再生するためのキー（つまりID）、第二引数が実際のリソース名
        player = GameObject.Find ("Player"); //Playerのゲームオブジェクトの呼び出し
        script = player.GetComponent<PlayerCtrl>(); //PlayerのゲームオブジェクトについているPlayerCtrl.csの呼び出し

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){ 
        
        

        if(col.gameObject.name == "Player"){ //文字がプレイヤーに触れたら
            Sound.PlaySe("getSE", 0); //効果音再生
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            GetComponent<CircleCollider2D>().enabled = false;

            Vector3 pos = transform.localPosition + Vector3.up * 7f; //現在位置に上方向のベクトルをプラス
            transform.DOLocalMove(pos, 0.5f); //posの場所まで0.5秒かけて移動
            transform.DORotate(new Vector3(0, 180, 0), 1.0f); //1秒かけてyを180度回転
            spriteRenderer.DOFade(0, 1.5f); //1.5秒かけてα値(透明度)が0になる

            img_count = script.image_count; //PlayerCtrl.csの中の変数image_countの値を、img_countに代入

            if(img_count < 7){
                //触れた文字を画面上に写す処理
                string spname = spriteRenderer.sprite.name; //割り当てられている画像の名前の文字列をspnameに格納
                string spname2 = spname.Substring(0, spname.Length-2); //spnameの末尾2文字を削除
                string image_str = "Image" + img_count.ToString();
                image = GameObject.Find(image_str);
                image.SendMessage("addChar", spname2); //とった文字を映し出す
                script.image_count += 1; //PlayerCtrl.csの中の変数image_countの値を+1する
            }
            
            
            Invoke("DestroyObject", 2); //2秒後にDestroyObject関数を実行
        }

        if(col.gameObject.name == "Tilemap"){
            Invoke("DestroyObject", 3); //2秒後にDestroyObject関数を実行
        }
    }

    void DestroyObject(){
        Destroy(this.gameObject); //文字が消える
    }
}
