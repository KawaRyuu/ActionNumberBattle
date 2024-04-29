using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnicalData : MonoBehaviour
{
    int technicalNomber  = 0;            //選んだ時点での箱の役割
    int technicalNomber1 = 0;            //一個目の選択時に決めたわざを保存
    int technicalNomber2 = 0;            //二個目の選択時に決めたわざを保存
    bool technicalFlg = false;           //わざ発動したかのフラグ
    bool technicalFlg1 = false;          //わざ1を発動したかのフラグ
    bool technicalFlg2 = false;          //わざ2を発動したかのフラグ


    // Start is called before the first frame update
    void Start()
    {
        //初期化
        technicalFlg = false;
        technicalFlg1 = false;
        technicalFlg2 = false;
        technicalNomber  = 0;
        technicalNomber1 = 0;
        technicalNomber2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //もし技を使用したなら
        if (technicalFlg)
        {
            //もし技1がtrueなら技の番号を判別後にswitchから発動
            if (technicalFlg1)
                technicalNomber = technicalNomber1;
            else if (technicalFlg2)
                technicalNomber = technicalNomber2;
        }


            //技選択時の区別受け取り
            switch (technicalNomber)
            {
                //技1 仮称「ハネトバシ」
                case 1:
                    FeatherFlying();        //技1の関数を呼び出し
                    Reset();                //技使用時に技使用フラグをリセット
                    break;

                //技2 仮称「ツバメ返し」
                case 2:
                    SwallowReturn();
                    Reset();                //技使用時に技使用フラグをリセット
                break;

                //技3 仮称「ストライク＆back」
                case 3:
                    StrikeBack();
                    Reset();                //技使用時に技使用フラグをリセット
                break;

                //技4 仮称「トッシン」
                case 4:
                    Rush();
                    Reset();                //技使用時に技使用フラグをリセット
                break;

                //技の選択時何も選ばなかった時ランダムで処理する(仮)
                case 5:
                    Reset();                //技使用時に技使用フラグをリセット
                break;
            }

        //技1or2を使った最後にリセットする
        Rest1_2();
    }
    /*************フラグ打消しの処理***************************/
    private void Reset()
    {
        technicalFlg = false;
    }
    private void Rest1_2()
    {
        //もしtechnicalFlg1がtrueなら
        if (technicalFlg1)
        technicalFlg1 = false;
        //もしtechnicalFlg2がtrueなら
        else if (technicalFlg2)
        technicalFlg2 = false;

    }

    /*************それぞれの技の動き処理***********************/

    //ハネトバシの動き
    void FeatherFlying()
    {
    }

    //ツバメ返しの動き
    void SwallowReturn()
    {
    }

    //ストライク&backの動き
    void StrikeBack()
    {
    }

    //トッシンの動き
    void Rush()
    {
    }
}
