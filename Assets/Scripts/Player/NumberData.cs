using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class NumberData : MonoBehaviour
{
    //自身が持っているNumber
    public int []MyNumber = {0,0,0,0};
    public Text MyNnm1;                  //UIの表示
    public Text MyNnm2;                  //UIの表示

    // Start is called before the first frame update
    void Start()
    {
        //ゲーム開始時と同時に数をランダムで取得する
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
        //自分の数をUnityで表示
        MyNnm1.text = MyNumber[0].ToString() 
            +(" ・ ")+MyNumber[1].ToString();

        MyNnm2.text = MyNumber[2].ToString()
            + (" ・ ") + MyNumber[3].ToString();
    }
}
