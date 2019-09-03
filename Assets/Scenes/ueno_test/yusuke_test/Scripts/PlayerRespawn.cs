using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject player;
    public Vector3 pos;

    public bool key;
    // Start is called before the first frame update
    void Start()
    {
        pos = (player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {


        /*if (key)        */
        TranceformUpdate(pos, player.transform.rotation);
    }

    void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 500, 300), player.transform.position.ToString());      // ラベルを表示する
        //GUI.Label(new Rect(0, 0, 500, 300),,);
    }

    void TranceformUpdate(Vector3 pos, Quaternion rot)
    {
        if (Respawn_Check()) player.transform.position = new Vector3(0, 1, 0);;
    }

    bool Respawn_Check()
    {
        if (player.transform.position.y < -3)
        {
            Debug.Log("Respawn : " + player.transform.position.y);
            return true;
        }
        Debug.Log("UnRespawn");

        return false;

    }

}
