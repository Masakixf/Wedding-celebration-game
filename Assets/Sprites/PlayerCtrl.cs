using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 500;
    private float jumpForce = 1000;
    public LayerMask groundLayer;
    public GameObject fxhit;
    public int image_count = 0; //これが現在何個文字をとったかを表す変数

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spRenderer;
    private bool isGround;
    public bool isDead = false;
    public bool isGoal = false;
    private bool oneDead = false;
    private GameObject deadScreen;
    //private AudioSource audioSource;

    private GameObject audioSource;
    private GameObject LAST_audioSource;
    private bool one_jump;
    private bool last_bgm = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.spRenderer = GetComponent<SpriteRenderer>();
        Sound.LoadSe("deadSE", "bomb"); //第一引数が再生するためのキー（つまりID）、第二引数が実際のリソース名
        Sound.LoadSe("jumpSE", "jump");

        audioSource = GameObject.Find("BGM"); //BGMのゲームオブジェクトを取得
        LAST_audioSource = GameObject.Find("LAST_BGM"); //BGMのゲームオブジェクトを取得
        LAST_audioSource.GetComponent<AudioSource>().Stop(); //BGMを止める
        //deadScreen = GameObject.Find("DeadScreen"); //Fadeの画像を見つける
        //deadScreen.SendMessage("fadeIn"); //死亡画面へ
    }

    // Update is called once per frame
    void Update()
    {
        //移動-----------------------------------------------------------------------
        if (!isGoal)
        {
            float x = Input.GetAxisRaw("Horizontal"); //左-1, なにもしない0, 右1

            //キャラの向きを変える
            if (x < 0)
            {
                spRenderer.flipX = true;
            }
            else if (x > 0)
            {
                spRenderer.flipX = false;
            }

            rb2d.AddForce(Vector2.right * speed * x); //横方向に力を加える
            anim.SetFloat("Speed", Mathf.Abs(x * speed)); //歩くアニメ
        }

        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;
        //横方向への速度制限
        if(Mathf.Abs(velX) > 5){
            if(velX > 5.0f){ //右に5以上の速度があれば5で止める
                rb2d.velocity = new Vector2(5.0f, velY);
            }
            if(velX < -5.0f){ //左に5以上の速度があれば5で止める
                rb2d.velocity = new Vector2(-5.0f, velY);
            }
        }
        //移動-----------------------------------------------------------------------


        //ジャンプ-----------------------------------------------------------------------
        //if (Input.GetButtonDown("Jump") & isGround & !isGoal){
        if (Input.GetButtonDown("Jump") & !one_jump & !isGoal){
            Sound.PlaySe("jumpSE", 1); //ジャンプ音
            anim.SetBool("isJump", true);
            rb2d.AddForce(Vector2.up * jumpForce);
            jumpForce = 600;
            one_jump = true;
        }
        if(isGround){ //地面にいるときはジャンプモーションOFF
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }

        velX = rb2d.velocity.x;
        velY = rb2d.velocity.y;
        if(velY > 3.0f){ //velocityが上向きに働いていたらジャンプ
            anim.SetBool("isJump", true);
        }
        if(velY < -3.0f){　//velocityが下向きに働いていたら落ちる
            anim.SetBool("isFall", true);
        }
        //ジャンプ-----------------------------------------------------------------------

        if(transform.position.y < -30 & !isDead & !oneDead){ //xが-30を下回ると死ぬ
            StartCoroutine("Dead");
            oneDead = true;
        }

        if(isGoal){ //ゴール時処理
            GetComponent<CircleCollider2D>().enabled = false; //playerの当たり判定を消す
            GameObject.Find("Time").GetComponent<TimeCtrl>().isDead = true; //時間経過で爆死しないようにする
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; //XY座標を固定
            //Destroy(this.gameObject);
            this.gameObject.GetComponent<Renderer>().enabled = false;
            if(!last_bgm){
                last_bgm = true;
                LAST_audioSource.GetComponent<AudioSource>().Play(); //BGMを始める
            }
            audioSource.GetComponent<AudioSource>().Stop(); 
        }

        if(isDead){ //死亡時時処理
            if(Input.GetKey(KeyCode.Return)){ //Enterを押したら
                Debug.Log("dead");
                SceneManager.LoadScene("Main"); //シーンの再読み込み
            }
        }        
    }


    private void FixedUpdate(){
        //キャラの接地点の当たり判定-----------------------------------------------------------------------        
        isGround = false;
        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y-2); //プレイヤーの立っている場所を保存
        Vector2 groundArea = new Vector2(0.5f, 0.5f);　//地面への当たり判定の大きさを決める
        isGround = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, groundLayer); //指定のエリアに触れたらisGroundがtrueになる、groundLayerで地面のレイヤーと設置したことを判定
        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red); //当たり判定の描画
        //Debug.Log(isGround);
        if(isGround == true){
            one_jump = false;
            jumpForce = 1000;
        }
        //キャラの接地点の当たり判定-----------------------------------------------------------------------    
    }
    private void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.name == "Goal"){
            if(image_count == 7){
                isGoal = true;
                anim.SetFloat("Speed", 0);
            }
            else{
                Debug.Log("文字を集めろ！");
            }  
        }
    }

    IEnumerator Dead(){ //死んだとき
        isDead = true;
        Sound.PlaySe("deadSE", 0); //爆発音
        Instantiate(fxhit, transform.position, transform.rotation); //爆発エフェクト
        GetComponent<CircleCollider2D>().enabled = false; //playerの当たり判定を消す
        spRenderer.enabled = false;

        
        audioSource.GetComponent<AudioSource>().Stop(); //BGMを止める

        yield return new WaitForSeconds(1.5f);
        deadScreen = GameObject.Find("DeadScreen"); //Fadeの画像を見つける
        deadScreen.SendMessage("fadeOut"); //死亡画面へ
        yield return new WaitForSeconds(1.5f);
        deadScreen.SendMessage("displayTelop"); //死亡画面へ

    }
}
