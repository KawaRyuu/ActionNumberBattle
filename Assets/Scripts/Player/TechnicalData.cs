using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnicalData : MonoBehaviour
{
    int technicalNomber1 = 0;            //一個目の技の区別するNumber
    int technicalNomber2 = 0;            //二個目の技の区別するNumber



    // Start is called before the first frame update
    void Start()
    {
        //初期化
        technicalNomber1 = 0;
        technicalNomber2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //技選択時の区別受け取り
        switch (technicalNomber1)
        {
            //技1 仮称「ハネトバシ」
            case 1:
                FeatherFlying();        //技1の関数を呼び出し
                break;

                //技2 仮称「ツバメ返し」
            case 2:
                SwallowReturn();
                break;

                //技3 仮称「ストライク＆back」
            case 3:
                break;

                //技4 仮称「トッシン」
            case 4:
                break;

                //技の選択時何も選ばなかった時ランダムで処理する(仮)
            case 0:
                break;
            }
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
}
