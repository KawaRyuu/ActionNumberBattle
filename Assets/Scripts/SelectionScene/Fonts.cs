using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Fonts : MonoBehaviour
{
    TechnicalData tec;

    //文字を表示
    public Text SelectionFonts;                 //指示文字
    public Text OnePlayer_Tec_Select_F;         //1Playerの技1の選択文字



    // Start is called before the first frame update
    void Start()
    {
        tec = GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectionFonts.text = "4つの中から2つずつ技を選んで下さい！！";

        //選んだ技によって文字表記が分岐する
        //if(1Player&&OneTecFlg)
        //{
        OnePlayer_Tec_Select_F.text = "1Pの技は選んでません。";
        //switch (tec.technicalNumber)
        //{
        //    case 1:
        //        OnePlayer_Tec_Select_F.text = "1Pの技"+"数"+":ハネトバシ(仮称)";
        //        break;

        //    case 2:
        //        OnePlayer_Tec_Select_F.text = "1Pの技" + "数" + ":ツバメ返し(仮称)";
        //        break;
        //    case 3:
        //        OnePlayer_Tec_Select_F.text = "1Pの技" + "数" + ":ストライク&back(仮称)";
        //        break;
        //    case 4:
        //        OnePlayer_Tec_Select_F.text = "1Pの技" + "数" + ":トッシン(仮称)";
        //        break;
        //}
        //}
    }
}
