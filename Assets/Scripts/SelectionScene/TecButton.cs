using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TecButton : MonoBehaviour
{
    //参照
    TechnicalData tec;

    public GameObject Tec1;
    public GameObject Tec2;
    public GameObject Tec3;
    public GameObject Tec4;

    // Start is called before the first frame update
    void Start()
    {
        tec.GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ボタンを押したら実行する関数　実行するためにはボタンへ関数登録が必要
    public void Push_Button()
    {
        //もし技1なら技1のデータを入れる
        if (Tec1)
            tec.technicalNumber = 1;

        //もし技2なら技2のデータを入れる
        if (Tec2)
            tec.technicalNumber = 2;

        //もし技3なら技3のデータを入れる
        if (Tec3)
            tec.technicalNumber = 3;

        //もし技4なら技4のデータを入れる
        if (Tec1)
            tec.technicalNumber = 4;
    }
}
