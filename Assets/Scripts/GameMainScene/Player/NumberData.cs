using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class NumberData : MonoBehaviour
{
    //自身が持っているNumberを配列で保持
    public int []MyNumber = {0,0,0,0};
    public Text MyNnm1;                  //UIの表示
    public Text MyNnm2;                  //UIの表示

    // Start is called before the first frame update
    void Start()
    {
        //ゲーム開始時と同時に数を1～9のランダムで取得する
        for (int i = 0; i < 4; i++)
        {
                MyNumber[0 + i] = Random.Range(1, 10);
        }
        Debug.Log("現在の数は" + MyNumber[0] + MyNumber[1] + 
            MyNumber[2] + MyNumber[3]);
    }

    // Update is called once per frame
    void Update()
    {
        //交換やアイテム使用時に毎度入れ替わる。
        Sort();
        //自分の数をUnityの画面で表示
        MyNnm1.text = MyNumber[0].ToString() 
            +(" ・ ")+MyNumber[1].ToString();

        MyNnm2.text = MyNumber[2].ToString()
            + (" ・ ") + MyNumber[3].ToString();
    }

    //ソート関数
    void Sort()
    {
        int num = 0;
        for(int i=0;i<3;i++)
        {
            for(int j =i+1;j<4;j++)
            {
                //配列のi番目の数が配列番号jの数より大きいなら入れ替える
                if (MyNumber[i] < MyNumber[j])
                {
                    //いったんMyNumberi番目の数をnumに入れる。
                    num = MyNumber[i];

                    //配列i番目の中に配列番号jを入れる。
                    MyNumber[i] = MyNumber[j];

                    //配列番号jにnum(i番号の数)を入れてソート完了
                    MyNumber[j] = num; 
                }
            }
        }
    }
}