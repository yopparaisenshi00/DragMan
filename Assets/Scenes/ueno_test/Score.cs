using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public Text score_text;
	private int score;
	public int grab_score = 1000;           //スコア加算(引きずった後)
	public int grab_attack_score = 800;     //スコア加算(ぶつかったら時)

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//文字設定
		Set_Text();
	}

	//文字設定
	void Set_Text() {
		score_text.text = "SCORE:" + score;
	}



	//スコア加算
	public void Score_Add(int add) {
		score += add;
	}

	//スコア加算(引きずった後)
	public void Score_Add_Grab() {
		score += grab_score;
	}

	//スコア加算(ぶつかった時)
	public void Score_Add_Grab_Attack() {
		score += grab_attack_score;
	}
}
