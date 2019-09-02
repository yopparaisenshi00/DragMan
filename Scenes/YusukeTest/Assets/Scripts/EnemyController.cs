using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{
    private Vector3 vec;
    private Vector3 pos;
    public Vector3[] Target = { new Vector3(4, 1, 95), new Vector3(8, 1, 90), new Vector3(-4, 1, 85), new Vector3(-8, 1, 80), };

    public int no;
    public int MaxNo;

    public Text HitText;
    public Text LenText;
    public float spd;
    public float Len;
    
    // Start is called before the first frame update
    void Start()
    {
        no = 0;
        pos = transform.position;
        HitText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }


    // 敵の動き
    void MoveEnemy()
    {
        // 自分の位置代入
        pos = transform.position;

        // 方向出す
        vec = Target[no] - pos;

        if(Vector3.Distance(Target[no], pos) < Len)
        {
            no++;
        }

        if (no > MaxNo) no = 0;

        transform.position += vec * (spd * Time.deltaTime);

        // その方向に向かう
        //move(vec.x, vec.y, vec.z);
        LenText.text = "Len : " + Vector3.Distance(Target[no], pos);

    }

    void move(float x, float y, float z)
    {
        // 移動を設定
        transform.position += new Vector3(x, y, z);
    }

    // 文字の設定
    void SetText()
    {
        HitText.text = "Hit";
    }

    // 当たってる時に呼ばれる
    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.CompareTag("PLAYER"))
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
            HitText.text = "";
        }
    }


}
