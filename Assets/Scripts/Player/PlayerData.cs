using System;
using System.Collections.Generic;
using UnityEngine;


/*プレイヤー（ライバルも含む）のデータを管理する。*/

//プレイヤーの基礎的なデータ
public class PlayersData
{
    private string Id = ("");     //おそらく名前
    private int Hp = 3;           //体力（耐久値）
    private int Attack = 1;       //攻撃
    private int NomberBox = 4;    //4つの数を保持する
    private float Speed = 1.0f;   //移動速度

    private float CoolTime = 4f;
    //技や奪取（交換）を含めたクールタイム全般

    private float invincibility = 0.5f;     //ダメージ喰らった際の無敵時間
    private int  RandomNomber = 0;          //自分の数を出すランダム数をここに
    private bool SwoonFlg = false;          //気絶フラグ
    private bool RecoveryTime_Flag = false; //回復タイムに入るフラグ

}
public class PlayerData : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        
    }
}
