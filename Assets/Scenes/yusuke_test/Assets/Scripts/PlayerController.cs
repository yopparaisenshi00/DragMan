using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    //private int count;

    //public Text countText, winText;
    public float spd;


    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyを付けてたら検出して返す
        rb = GetComponent<Rigidbody>();

        // スコアを格納する
        //count = 0;

        // Textプロパティの初期値を設定する(使う値の下に書く)
        //SetCountText();

        // 文字を空にして後から文字を入れる
        //winText.text = "";
    }

    // Update is called once per frame
    void Update() { }

    // 物理演算を実行する直前に呼び出される
    void FixedUpdate()
    {
        /*  
            物理演算のコードはこっちに書く
        */

        // 水平軸と垂直軸からの入力を記録
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 移動する値を設定
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // 物理演算を使って動かす
        rb.AddForce(movement * spd);

    }

    // 当たった瞬間に呼び出される
    void OnTriggerEnter(Collider other)
    {
        // 当たったものの名前がPickUp
        if (other.gameObject.CompareTag("PickUp"))
        {
            // アクティブ、非アクティブにする関数（チェックボックスのon,off）
            other.gameObject.SetActive(false);

            // スコアを1つ加算
            //count++;

            // 値を文字にする
            //SetCountText();
        }
    }

    // テキストを何回も書かずにまとめるためのもの
    //void SetCountText()
    //{
    //    countText.text = "Count : " + count.ToString();

    //    // 全部集めてたら文字を入れる
    //    if (count >= 12)
    //    {
    //        winText.text = "You Win!";
    //    }
    //}

}
