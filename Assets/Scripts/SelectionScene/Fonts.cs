using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class Fonts : MonoBehaviour
{
    TecButton tecButton;
    TechnicalData tecD;

    //文字を表示
    public Text SelectionFonts;                 //指示文字
    public Text OnePlayer_Tec_Select_F;         //1Playerの技1の選択文字
    public Text OnePlayer_Tec_Select_F2;         //1Playerの技2の選択文字

    bool oneTecFlg = false;
    bool twoTecFlg = false;
    int TecSelectionCoverNum = 0;               //技を選んだ際1週目のnumberを保持
    float Pre_start_time = 6.0f;                //試合(mainScene)に飛ばすカウントダウン

    public  int Tec1 = 0;                       //技選択枠1の技の情報保存のNum
    public  int Tec2 = 0;                       //技選択枠2の技の情報保存のNum

    // Start is called before the first frame update
    void Start()
    {
        tecButton = GetComponent<TecButton>();
        tecD = GetComponent<TechnicalData>();
        oneTecFlg = false;
        twoTecFlg = false;
        TecSelectionCoverNum = 0;
        Pre_start_time = 6.0f;
        Tec1 = 0;
        Tec2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //選んだ技によって文字表記が分岐する
        //if(1Player&&OneTecFlg)
        //{

        //もし2週していないなら
        if (!twoTecFlg)
        {
            SelectionFonts.text = "4つの中から2つずつ技を選んで下さい！！";

            switch (tecButton.public_number)
            {
                case 0:
                    OnePlayer_Tec_Select_F.text = "1Pの技1は選んでません。";
                    OnePlayer_Tec_Select_F2.text = "1Pの技2は選んでません。";
                    break;
                case 1:
                    //もし1週すらしてないなら
                    if (!oneTecFlg)
                    {
                        OnePlayer_Tec_Select_F.text = "1Pの技" + 1 + ":ハネトバシ(仮称)";
                        TecSelectionCoverNum = tecButton.public_number;//保持
                        Tec1 = tecButton.public_number;                //技1選択保持
                        OneTec();
                    }
                    //もし１週目且つボタンが押されたら
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1Pの技" + 2 + ":ハネトバシ(仮称)";
                        Tec2 = tecButton.public_number;                //技2選択保持
                        TwoTec();
                    }
                    
                    break;

                case 2:
                    //もし1週すらしてないなら
                    if (!oneTecFlg)
                    {
                        OnePlayer_Tec_Select_F.text = "1Pの技" + 1 + ":ツバメ返し(仮称)";
                        TecSelectionCoverNum = tecButton.public_number;//保持
                        OneTec();
                    }
                    //もし１週目且つボタンが押されたら
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1Pの技" + 2 + ":ツバメ返し(仮称)";
                        TwoTec();
                    }
                    tecButton.Tec2Flg = true;
                    break;

                case 3:
                    //もし1週すらしてないなら
                    if (!oneTecFlg)
                    {
                        OnePlayer_Tec_Select_F.text = "1Pの技" + 1 +
                            ":ストライク&back(仮称)";
                        TecSelectionCoverNum = tecButton.public_number;//保持
                        OneTec();
                    }
                    //もし１週目且つボタンが押されたら
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1Pの技" + 2 +
                            ":ストライク&back(仮称)";
                        TwoTec();
                    }
                    tecButton.Tec3Flg = true;
                    break;

                case 4:
                    //もし1週すらしてないなら
                    if (!oneTecFlg)
                    {
                        OnePlayer_Tec_Select_F.text = "1Pの技" + 1 + ":トッシン(仮称)";
                        TecSelectionCoverNum = tecButton.public_number;//保持
                        OneTec();
                    }
                    //もし１週目且つボタンが押されたら
                    else if (oneTecFlg && tecButton.PushButtonFlg)
                    {
                        OnePlayer_Tec_Select_F2.text = "1Pの技" + 2 + ":トッシン(仮称)";
                        TwoTec();
                    }
                    tecButton.Tec4Flg = true;
                    break;
            }
        }
        else
        {
            //カウントダウン
            Pre_start_time -= Time.deltaTime;
            SelectionFonts.text = "まもなく試合を開始します。                          "+ (int)Pre_start_time;
            Invoke("GotoGameMainScene", 6.0f);
        }
    }

    //技選択1週目のフラグ関数
    void OneTec()
    {
        oneTecFlg = true;                   //1週のフラグON
        tecButton.PushButtonFlg = false;    //処理戻す
    }
    //技選択2週目のフラグ関数
    void TwoTec()
    {
        //もし2週目を選択した際被ったら
        if (TecSelectionCoverNum == tecButton.public_number)
        {
            Cover();       //被った際の処理関数へ
        }
        else
        {
            //2つ技選択完了
            twoTecFlg = true;
            UnityEngine.Debug.Log("技選択終了");
        }
    }
    //技が被った際の関数
    void Cover()
    {
        OnePlayer_Tec_Select_F2.text = "技が被りました。                                   もう一度選択してください。";
    }

    //ゲームメインの画面へシーン切り替え
    void GotoGameMainScene()
    {
        SceneManager.LoadScene("GameMain");
    }
}
