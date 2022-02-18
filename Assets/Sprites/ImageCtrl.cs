using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ImageCtrl : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addChar(string pic_name){ //とった文字を映し出す
        //Debug.Log(pic_name);
        Sprite[] sprites = Resources.LoadAll<Sprite>(pic_name); //spriteにResourcesのところから引数で求められた画像(スライス済み)を格納
        GetComponent<Image>().sprite = sprites[0]; //スライスした画像の中の、先頭の画像をImageに反映
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);//α値を255に
        
    }
    void deleteChar(){
        image.sprite = null; //画像を空白に
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3176f); //α値を81に(81/255=0.3176)
    }
        
}
