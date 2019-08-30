using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;   // Playerを参照する
    private Vector3 offset;     // オフセット値を保持する

    // Start is called before the first frame update
    void Start()
    {
        // カメラとプレイヤーの差を設定
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // カメラの位置をプレイヤーから一定に離れたところに置く
        transform.position = player.transform.position + offset;
    }

    // 全てのモノがUpdate()で処理された後に実行される
    void LateUpdate()
    {
        // カメラの位置をプレイヤーから一定に離れたところに置く
        //transform.position = player.transform.position + offset;
    }
}
