using System;
using System.Collections.Generic;
using UnityEngine;


/*プレイヤー（ライバルも含む）のデータを管理する。*/

//プレイヤーの基礎的なデータ
public class PlayersData
{
    public string Id = ("");     //おそらく名前
    public int Hp = 3;           //体力（耐久値）
    public int Attack = 1;       //攻撃
    public int NomberBox = 4;    //4つの数を保持する
    public float Speed = 1.0f;   //移動速度

    public float CoolTime = 4f;
    //技や奪取（交換）を含めたクールタイム全般

    public float invincibility = 0.5f;     //ダメージ喰らった際の無敵時間
    public int RandomNomber = 0;           //自分の数を出すランダム数をここに
    public bool SwoonFlg = false;          //気絶フラグ
    public bool RecoveryTime_Flg = false;  //回復タイムに入るフラグ
    public bool Invincibility_Flg = false; //無敵フラグ

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
