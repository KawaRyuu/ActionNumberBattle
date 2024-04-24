using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //プレイヤーのデータから参照
    PlayersData info;

    public float Count;

    // Start is called before the first frame update
    void Start()
    {
        info = GetComponent<PlayersData>();
        Count = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //無敵フラグがON且つもし無敵時間が0.5秒以下なら
        if (info.Invincibility_Flg && info.invincibility >= Count)
        {
            Count += Time.deltaTime;
        }
        else
        {
            //無敵解除
            info.Invincibility_Flg = false;
            Count = 0;
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //触れたとき
        if(other.collider)
        {
            //もし無敵状態じゃない且つ気絶してないときなら
            if (!info.Invincibility_Flg&&!info.SwoonFlg)
            {
                info.Hp -= info.Attack;         //体力がAttackの攻撃参照
                info.Invincibility_Flg = true;  //無敵フラグON
            }

            //もし体力が0以下になったら
            if (info.Hp >= 0)
            {
                //気絶処理
                info.SwoonFlg = true;
                info.Hp = 3;
            }
        }
    }
}
