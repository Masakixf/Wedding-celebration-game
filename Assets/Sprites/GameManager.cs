using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CharPrefabs; //オブジェクトを格納する配列変数
    //private float time; //出現する間隔を制御するための変数
    private int prefab_number; //ランダム情報を入れるための変数
    private int x_pos; //ランダムに落ちてくる文字を、生成する場所(x座標)
    private int y_pos; //ランダムに落ちてくる文字を、生成する場所(y座標)
    private int rotate; //ランダムに落ちてくる文字の向き
    private GameObject player;
    private PlayerCtrl script;
    private Transform playerTrans;
    private int player_x;

    // Start is called before the first frame update
    void Start()
    {
        //time = 1.0f; //時間を待たず、最初の1回を出現
        player = GameObject.Find ("Player");
        playerTrans = player.GetComponent<RectTransform>();
        script = player.GetComponent<PlayerCtrl>();
        //InvokeRepeating("AppearChar", 0.0f, 1.5f);
        InvokeRepeating("AppearChar", 0.0f, 0.3f);
        //Debug.Log(CharPrefabs.Length.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CharPrefabs.Length.ToString());
        //time -= Time.deltaTime; //timeから時間を減らす
        //if (time <= 0.0f){ //0秒になれば
        //    time = 1.0f; //1秒にする
        //    AppearChar();
        //}
    }
    void AppearChar(){
        player_x = (int)player.transform.position.x;
        prefab_number = Random.Range(0, CharPrefabs.Length + 6); //Random.Range (最小値, 最大値) 整数の場合は最大値は除外
        
        //if(prefab_number == CharPrefabs.Length ||prefab_number == CharPrefabs.Length+1 || prefab_number == CharPrefabs.Length+2 || prefab_number == CharPrefabs.Length+3 || prefab_number == CharPrefabs.Length+4 || prefab_number == CharPrefabs.Length+5 || prefab_number == CharPrefabs.Length+6 || prefab_number == CharPrefabs.Length+7){
        if(prefab_number == CharPrefabs.Length ||prefab_number == CharPrefabs.Length+1 || prefab_number == CharPrefabs.Length+2 || prefab_number == CharPrefabs.Length+3 || prefab_number == CharPrefabs.Length+4 || prefab_number == CharPrefabs.Length+5){

            prefab_number = script.image_count;
        }
        x_pos = Random.Range(-5, 90); //生成する場所(x座標)
        //x_pos = Random.Range(player_x+5, player_x+5); //生成する場所(x座標)
        y_pos = Random.Range(5, 10); //生成する場所(y座標)
        rotate = Random.Range(0, 360);; //回転する角度
        Instantiate(CharPrefabs[prefab_number], new Vector3(x_pos, y_pos, 0f), Quaternion.Euler(0, 0, rotate)); 
        //座標(x_pos, y_pos)にランダム出現、向きは左にrotate度傾く(固定の時はQuaternion.identity)
    }

}
