using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitText : MonoBehaviour
{
    public Text txt;
    public string str;  // 初期設定文字
    public string buf;  // 追加文字

    // Start is called before the first frame update
    void Start()
    {
        txt.text = str;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 文字の設定
    void SetText()
    {
        txt.text = str + buf;
    }

    // 当たってる時に呼ばれる
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("PLAYER"))
        {
            SetText();
        }
    }

    // 離れたときに呼ばれる
    void OnTriggerExit(Collider col)
    {
        // 当たってたものの名前がPLAYER
        if (col.gameObject.CompareTag("PLAYER"))
        {
            txt.text = str;
        }
    }


}
