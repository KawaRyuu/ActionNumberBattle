using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//アイテムの種類と処理のクラス
//プレイヤーのステータス参照
public class Item
{
    PlayerData Pl;

    /*アイテムメモ：同じ種類のアイテムは所持不可能*/

    /*どのアイテムを所持しているかの変数*/
    public bool Change_min_Flg      = false;//自動的にランダム奪取が低い数へ適用される。
    public bool Num_Random_Flg      = false;//4枚の数のうち、一番低い数が１～９にランダムで変わる。
    public bool Nummin_one_Flg      = false;//4枚の数の中で1以外の小さい数が1枚1に変わる。
    public bool Change_random_Flg   = false;//自身の数字をランダムな人と交換（交換している人同士は見える）
    public bool Speed_up_Flg        = false;//自身移動速度UP（3秒間）
    public bool Spdown_Flg          = false;//相手に投げ飛ばして移動速度Down（2秒間）
    public bool Critical_Flg        = false;//一撃気絶のアイテム

    /*アイテム処理時に使う変数*/
    public float speed_up_timer = 0.0f;//関数Speed_Upで使用するタイマー

    /*アイテムの処理をする関数*/

    //自動的にランダム奪取が低い数へ適用される。
    public int Change_min()
    {
        int min = 9;
        //数字が入っている配列の中で一番小さい数字を取らせる
        //一番小さい数字を算出


        return min;
        /*
         * メモ：このアイテム使用した後に
         * 自身の数字を変更するアイテムを使用したら
         * 返した配列番号が最小ではない値になる可能性がある
         * 
         *→解決案：数字変更するアイテム使用時にもう一度この関数を呼び出す
         */

    }

    //4枚の数のうち、一番低い数が１～９にランダムで変わる。
    public void NUM_Random()
    {
        
    }

    //4枚の数の中で1以外の小さい数が1枚1に変わる。
    public void NumMin_One()
    {
        
    }

    //自身の数字をランダムな人と交換（交換している人同士は見える）
    public void Change_Random()
    {
        
    }

    //自身移動速度UP（3秒間）
    /*アイテム使ったときに移動速度アップの関数を持ってくる*/
    public void Speed_Up()
    {
        Pl.Speed = 300.0f;
        speed_up_timer += Time.deltaTime;
        //別のアイテム使用時にバグ起きる可能性あり
        if (speed_up_timer > 3.0f)
            Pl.Speed = 5.0f;

    }

    //相手に投げ飛ばして移動速度Down（2秒間）
    /*PlayerDataに鈍足処理あり→鈍足タグついたオブジェクト召喚プログラムへ*/
    public void SpDown()
    {
        //plefab召喚→プレイヤーが向いている向きに発射(3マス分進んだら消える)
        //plefab召喚

        //召喚したplefabを一定方向に発射
        //３マス進んだらdeleteする：if文
    }

    //一撃気絶のアイテム
    public void Critical()
    {
        Pl.Attack = 3;
    }

}

public class ItemSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
