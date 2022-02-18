using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleCtrl : MonoBehaviour
{
    private int screen = 0;
    GameObject[] tag_Screen1;
    GameObject[] tag_Screen2;
    // Start is called before the first frame update
    void Start()
    {
        //text2.enabled = false;
        tag_Screen1 = GameObject.FindGameObjectsWithTag("Screen1");
        tag_Screen2 = GameObject.FindGameObjectsWithTag("Screen2");
        foreach(GameObject i in tag_Screen2){
            i.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){ //Enterを押したら
            screen += 1;
        }

        if(screen == 1){
            foreach (GameObject i in tag_Screen1)
            {
                i.SetActive(false);
            }
            foreach(GameObject i in tag_Screen2){
                i.SetActive(true);
            }
        
        }

        if(screen == 2){
            SceneManager.LoadScene("Main"); //シーンの再読み込み
        }
        
    }
}
