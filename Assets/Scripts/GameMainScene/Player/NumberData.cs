using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class NumberData : MonoBehaviour
{
   
    Player player;

    //自身が持っているNumberを配列で保持
    public int []MyNumber = {0,0,0,0};

    // Start is called before the first frame update
    void Start()
    {
       
        player = GetComponent<Player>();
       
        //ゲーム開始時と同時に数を1～9のランダムで取得する
        for (int i = 0; i < 4; i++)
        {
                MyNumber[i] = Random.Range(1, 10);
        }
        Debug.Log("現在の数は" + MyNumber[0] + MyNumber[1] + 
            MyNumber[2] + MyNumber[3]);
    }

    // Update is called once per frame
    void Update()
    {
        //交換やアイテム使用時に毎度入れ替わる。
        //Sort();
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

    //受け渡し関数
    public int GetData(int num)
    {
        return MyNumber[num];
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("おんこりじょん");

        if (collision.gameObject.tag != "Player")
            return;

        Player     hit_player      = collision.gameObject.GetComponent<Player>();
        PlayerData hit_player_data = collision.gameObject.GetComponent<PlayerData>();

        if ( player.PlayerId() != hit_player.PlayerId() && hit_player_data.Swaps_Flg)
        {
            Debug.Log("交換");

            //触れた相手のNumberDataを取得
            NumberData num_data    = collision.gameObject.GetComponent<NumberData>();

            //相手が気絶したなら
            if (hit_player_data.GetPlayerState() == PlayerData.PLAYER_STATE.SWOON)
            {
                //1～10を100%換算する
                int RandomNumber = Random.Range(1, 11);
                int tmp = 0;                            //交換をする際の箱

                //60%の確立なら
                if (RandomNumber < 7)
                {
                    tmp = num_data.MyNumber[0];           //相手のMyNumberの大きい数をtmpに入れる
                    num_data.MyNumber[0] = MyNumber[3];   //相手の大きい数に自分の最も小さい数を渡す。（交換）
                }

                else //30%の確立なら
                if (RandomNumber < 9)
                {
                    //数字は4つあるけど2つは引かれるから残りの数字をランダムで選ぶ
                    int num = Random.Range(1, 3);

                    tmp = num_data.MyNumber[num];           //相手のMyNumberの1,2いずれかの数をtmpに入れる
                    num_data.MyNumber[num] = MyNumber[3];   /*相手のランダムで選ばれた配列番号に
                                                            自分の最も小さい数を渡す。（交換）*/
                }

                //低確率で
                if (RandomNumber == 10)
                {
                    tmp = num_data.MyNumber[3];           //相手のMyNumberの小さい数をtmpに入れる
                    num_data.MyNumber[0] = MyNumber[3];   //相手の小さい数に自分の最も小さい数を渡す。（交換）
                }
                
                MyNumber[3] = tmp;                      //相手の数字を自分の所へ入れる。
                hit_player_data.Swaps_Flg = false;                   //交換フラグをOFFにする。
            }
        }
    }
}