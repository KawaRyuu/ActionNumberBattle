using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //プレイヤーの操作
    //private float playerSpeed = 5.0f;

    //プレイヤーのデータクラスから参照
    PlayerData info;

    // 初期化
    void Start()
    {
        info = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        //Playerの基本の動き
        //もし気絶中なら行動不可
        if (!info.Swoon_Flg)
        {
            if (Input.GetKey("left"))
            {
                position.x -= info.Speed * Time.deltaTime;          //左方向
            }
            else if (Input.GetKey("right"))
            {
                position.x += info.Speed * Time.deltaTime;          //右方向
            }
            else if (Input.GetKey("up"))
            {
                position.y += info.Speed * Time.deltaTime;          //上方向
            }
            else if (Input.GetKey("down"))
            {
                position.y -= info.Speed * Time.deltaTime;          //下方向
            }
            transform.position = position;
        }
        //Debug.Log(("今のスピードは") + info.Speed);
        Debug.Log("今の体力は" + info.Hp);
        //Debug.Log("無敵フラグは:" + info.Invincibility_Flg);
    }
}
