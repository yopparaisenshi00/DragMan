using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotRoom : MonoBehaviour
{
    public Text HotText;
    private float HotCount;
    private bool hit;

    public float count;
    public float sub;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetText();
        Hit();
    }

    void Hit()
    {
        if (hit)                HotCount += (count * Time.deltaTime);  // 熱さ加算
        else if (HotCount >= 0) HotCount -= (sub * Time.deltaTime);    // 熱さ減算

        if (HotCount <= 0) HotCount = 0;
    }

    void SetText()
    {
        HotText.text = "熱い : " + HotCount.ToString();
    }

    // 当たってるとき呼ばれる
    void OnTriggerStay(Collider col)
    {
        // 当たったものの名前がPLAYER
        if (col.gameObject.CompareTag("PLAYER"))
        {
            hit = true;            
        }
    }

    // 離れたときに呼ばれる
    void OnTriggerExit(Collider col)
    {
        // 当たってたものの名前がPLAYER
        if (col.gameObject.CompareTag("PLAYER"))
        {
            hit = false;
        }
    }

}
