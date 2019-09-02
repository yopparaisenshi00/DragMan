using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.UIElements;


public class wd_scene_test : MonoBehaviour
{

    int SceneTestGuiWindowID;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnGUI()
    {
                
        // ラベルを表示する
        GUI.Label(new Rect(10, 10, 100, 100), "MenuWindow");
    }

    void Update()
    {
        
    }

}
