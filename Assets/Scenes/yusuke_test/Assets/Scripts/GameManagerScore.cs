using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScore : MonoBehaviour
{
    public Text score_text;
    public RectTransform rect;

    public int text_size;                       // 大きさ
    private int init_text_size;                 // 初期大きさ
    private Vector3 text_pos;                   // 位置
    private Vector3 init_text_pos;              // 初期位置
    public int add_text_pos_x, add_text_pos_y;  // 足す分
    public int sub_text_pos_x, sub_text_pos_y;  // 引く分

    public int score;                           // 総得点
	private int front_score = 0;
	public int add_score;                       // スコアをどれだけ足すか
    private bool calculation_score_fg;          // 計算をするかどうか

    private int state;                          // テキスト大きくするときの状態
    public int size_up_max;                     // サイズの上限
    public int add_text_size;                   // スコアを足すときの数
    public int sub_text_size;                   // 引くとき
    private bool size_chenge_fg;                // サイズを変える時のフラグ

	public int grab_score = 1000;				// スコア加算(引きずった後)
	public int grab_attack_score = 800;         // スコア加算(ぶつかったら時)



	// Start is called before the first frame update
	void Start()
    {
        // テキストのサイズを設定
        score_text.fontSize = init_text_size = text_size;

        // 初期位置を設定
        text_pos = init_text_pos = rect.transform.localPosition;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // スコアが入る動作（テスト）
        test_calculation_score();

        // サイズ変えてないときは初期サイズに戻す
        size_reset();

        // 位置リセット
        pos_reset();
        //pos_update();

        // 計算する
        if (calculation_score_fg) calculation_score(add_score);

        // サイズを変える
        if (size_chenge_fg)
        {
            size_chenge();
            text_move_pos(add_text_pos_x, add_text_pos_y, sub_text_pos_x, sub_text_pos_y);    // 位置も更新
        }

		// テキスト表示
		text_set();


		//前のスコアとして保存
		front_score = score;

	}


	// テキスト表示
	void text_set()
    {
        score_text.text = "SCORE: " + score;
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

	// 計算
	void test_calculation_score()
    {
		// 計算していないときに何かしらの動作があったら計算出来る
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		if (score != front_score) 
		{
			// 計算をする
			calculation_score_fg = true;

            // 新しくスコアが入ったときに大きさをリセット
            size_chenge_fg = false;

            // 小さくなってる途中やったら初期化してから大きくなる
            if (state == 1) state = 0;
        }
    }

    // スコアの計算
    void calculation_score(int add_score)
    {
        // 送られてきたスコアを代入
        score += add_score;

        // サイズを変える
        size_chenge_fg = true;

        // 計算を終わらせる
        calculation_score_fg = false;
    }

    // テキストの大きさを変える
    void size_chenge()
    {
        switch (state)
        {
            case 0:
                // 画面に大きく表示
                text_size += add_text_size;

                // 拡大終了
                if (text_size > size_up_max) state = 1;
                break;
            case 1:
                // 元の大きさに戻す
                text_size -= sub_text_size;

                // 元のサイズに戻ったら処理を終わる
                if (text_size <= init_text_size)
                {
                    state = 0;
                    size_chenge_fg = false;
                }
                break;
        }
        score_text.fontSize = text_size;
    }

    // サイズをリセット
    void size_reset()
    {
        // サイズの初期化
        if (!size_chenge_fg) text_size = init_text_size;
    }

    //位置の更新
    void text_move_pos(float add_pos_x, float add_pos_y, float sub_pos_x, float sub_pos_y)
    {
        if (state == 0) text_pos = new Vector3(text_pos.x + add_pos_x, text_pos.y - add_pos_y, 0);
        if (state == 1) text_pos = new Vector3(text_pos.x - sub_pos_x, text_pos.y + sub_pos_y, 0);
        rect.transform.localPosition = text_pos;
    }

    // 位置リセット
    void pos_reset()
    {
        if (!size_chenge_fg) text_pos = init_text_pos;
    }

    // 桁が上がったら位置を更新
    //void pos_update()
    //{
    //    rect.transform.localPosition = new Vector3(text_pos.x + (score / 10), text_pos.y, 0);
    //}

}
