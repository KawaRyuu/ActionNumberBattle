using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//リザルトシーンで表示を管理しながら実行するマネージャー

public class ResultManager : MonoBehaviour
{
    [SerializeField] GameObject[] player_winner_panels = new GameObject[4]; 
    [SerializeField] GameObject   player_battle_data_canvas;
    [SerializeField] ResultScoreScriptableObject result_score;


    //PlayerのInputSystem
    PlayerInput playerInput;

    const float disp_time         = 1.0f;
    float       disp_timer        = 0.0f;
    int         disp_panel_num    = 0;
    bool        disp_ranking_flag = false;

    private void Awake()
    {
        TryGetComponent(out playerInput);

        SettingResultText();
    }
    private void Start()
    {
        disp_timer = 0.0f;
        disp_ranking_flag = true;
        disp_panel_num = player_winner_panels.Length - 1;

    }

    //毎フレーム実行
    private void Update()
    {
        DisplayRanking();
        DisplayResult();
    }

    //毎フレーム最後に実行
    private void LateUpdate()
    {
        //すべてのパネルを表示し終えたら、ランキング表示を止める
        if (disp_panel_num < 0)
            disp_ranking_flag = false;
    }

    void SettingResultText()
    {
        //ランキング
        RankingPlayer();

        for(int i = 0;i< player_winner_panels.Length;i++)
        {
            Text winner_text = player_winner_panels[i].GetComponentInChildren<Text>();
            winner_text.text = CheckPlayerId(result_score.GetComponent<Player>().PlayerId()); ;
        }

    }

    void RankingPlayer()
    {

        for(int i = 0; i < result_score.players_array.Length -1; i++)
        {
            for(int j  = i+1; j< result_score.players_array.Length; j++)
            {
                if (result_score.players_array[i].GetComponent<NumberData>().GetTotalSum() <
                    result_score.players_array[j].GetComponent<NumberData>().GetTotalSum())
                {
                    GameObject middle = result_score.players_array[i];
                    result_score.players_array[i] = result_score.players_array[j];
                    result_score.players_array[j] = middle;
                }
            }
        }
    }

    string CheckPlayerId(Player.PLAYER_ID id)
    {
        switch (id)
        {
            case Player.PLAYER_ID.P1:
                return "1Player";

            case Player.PLAYER_ID.P2:
                return "1Player";
                
            case Player.PLAYER_ID.P3:
                return "1Player";
                
            case Player.PLAYER_ID.P4:
                return "1Player";

            default:
                return "プレイやーが設定できてないぞ";
        }
    }


    //プレイヤーの順位を表示する関数
    void DisplayRanking()
    {
        //表示フラグがfalseの時は返す
        if (!disp_ranking_flag)
            return;

        //タイマーを加算
        disp_timer += Time.deltaTime;

        //タイマーが指定時間経過するまで返す
        if (disp_timer < disp_time)
            return;

        //タイマー初期化
        disp_timer = 0.0f;

        //現在の順位のプレイヤーを表示
        player_winner_panels[disp_panel_num].SetActive(true);

        //表示するパネルをずらす
        disp_panel_num--;
    }

    //各プレイヤーの成績を表示する
    void DisplayResult()
    {
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();
        bool BackButton = playerInput.actions["BackButton"].WasPressedThisFrame();

        //ランキングを表示中は返す
        if (disp_ranking_flag)
            return;

        //ボタンが押されたら、成績を表示する
        if (SelectButton)
            player_battle_data_canvas.SetActive(true);
        if (BackButton)
            player_battle_data_canvas.SetActive(false);
    }

    

}
