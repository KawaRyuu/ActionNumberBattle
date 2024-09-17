using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NumberData :MonoBehaviour
{
   
    Player player;

    Player hit_player;
    PlayerData hit_player_data;
    NumberData num_data;

    
    int number_sum;
    int total_sum;
    int change_number_count = 0;      //交換回数
    int total_bonus_point = 0;        //合計のボーナスポイント
    const int bonus_point = 3;              //一回で追加されるボーナスポイント


    bool can_change_flag = false;

    //自身が持っているNumberを配列で保持
    public int []MyNumber = {0,0,0,0};

    private void Awake()
    {
        //ゲーム開始時と同時に数を1～9のランダムで取得する
        for (int i = 0; i < 4; i++)
        {
            MyNumber[i] = Random.Range(1, 10);
        }
        Debug.Log("現在の数は" + MyNumber[0] + MyNumber[1] +
            MyNumber[2] + MyNumber[3]);

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        can_change_flag = false;
        change_number_count = 0;
        total_bonus_point = 0;

        SumCulc();
      
        Sort();
    }

    // Update is called once per frame
    void Update()
    {
        //交換やアイテム使用時に毎度入れ替わる。
        //Sort();

        ChangeNumber();

        SumCulc();

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

    void ChangeNumber()
    {
        Debug.Log(can_change_flag);

        if (Input.GetKeyDown(KeyCode.E) && can_change_flag)
        {
            Debug.Log("交換ボタンを押した");

            //1～10を100%換算する
            int RandomNumber = Random.Range(1, 11);
            int tmp = 0;                            //交換をする際の箱

           
            if (RandomNumber < 7)
            {
                //60%の確立なら
                tmp = num_data.MyNumber[0];           //相手のMyNumberの大きい数をtmpに入れる
                num_data.MyNumber[0] = MyNumber[3];   //相手の大きい数に自分の最も小さい数を渡す。（交換）
            }
            else 
            if (RandomNumber < 9)
            {
                //30%の確立なら
                //数字は4つあるけど2つは引かれるから残りの数字をランダムで選ぶ
                int num = Random.Range(1, 3);

                tmp = num_data.MyNumber[num];           //相手のMyNumberの1,2いずれかの数をtmpに入れる
                num_data.MyNumber[num] = MyNumber[3];   /*相手のランダムで選ばれた配列番号に
                                                            自分の最も小さい数を渡す。（交換）*/
            }
            else if (RandomNumber == 10)
            {
               //低確率で
                tmp = num_data.MyNumber[3];           //相手のMyNumberの小さい数をtmpに入れる
                num_data.MyNumber[0] = MyNumber[3];   //相手の小さい数に自分の最も小さい数を渡す。（交換）
            }

            MyNumber[3] = tmp;                      //相手の数字を自分の所へ入れる。
            hit_player_data.Swaps_Flg = false;                   //交換フラグをOFFにする。
            can_change_flag = false;

            total_bonus_point += bonus_point;
            change_number_count++;

            //SumCulc();
            //num_data.SumCulc();

            Debug.Log("交換した！");
        }

    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("おんこりじょん");

        if (collision.gameObject.tag != "Player")
            return;

         hit_player       = collision.gameObject.GetComponent<Player>();
         hit_player_data  = collision.gameObject.GetComponent<PlayerData>();

        if ( player.PlayerId() != hit_player.PlayerId() && hit_player_data.Swaps_Flg)
        {
            Debug.Log("交換");

            //触れた相手のNumberDataを取得
            num_data = collision.gameObject.GetComponent<NumberData>();

            can_change_flag = true;
            //相手が気絶したなら
          
        }
       
    }

    public void SumCulc()
    {
        number_sum = 0;

        for(int i = 0;i<MyNumber.Length;i++)
        {
            number_sum += MyNumber[i];
        }

        total_sum = number_sum + total_bonus_point;

        Debug.Log("NumberData" + number_sum);
    }

    public int GetChangeNumberCount()
    {
        return change_number_count;
    }

    public int GetTotalBonusPoint()
    {
        return total_bonus_point;
    }

    public int GetNumberSum()
    {
        return number_sum;
    }

    public int GetTotalSum()
    {
        return total_sum;
    }
}