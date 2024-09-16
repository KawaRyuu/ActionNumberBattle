using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushRangeJudge : MonoBehaviour
{
    //参照
    TechnicalData tec;

    //親オブジェクトのTechnicalDataを取得したいので
    //[SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //tec = Player.GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CircleBuild(Vector2 circle_position,Vector2 enemy_position)
    {
        //[^]☚２乗の意味
        //(x-a)^2+(y-b)^2


        //半径の数
        float Radius = 4.0f;
        
        //合計
        var sum = 0f;


        //(x-a)^2+(y-b)^2の式
        //circle_positionが円の中心でx,yの座標を取得してくる。
        //enemy_positionが円の中にいたPlayerの座標を取得
        for (var i = 0; i < 2; i++)
            sum += Mathf.Pow(circle_position[i] - enemy_position[i], 2);

        GameObject.Find("Player");
        return sum <= Mathf.Pow(Radius, 2f);
        
    }

    /************当たった時の処理(何かの当たった時)*****************/
    private void OnTriggerEnter2D(Collider2D other)
    {
        //もしトッシン範囲のなかにPlayerのtagがあったなら
        //if (other.gameObject.tag == "Player")
        //{
        //    //tec.SetGameObjectList(other.gameObject);

        //    Debug.Log("敵発見");
        //    //target(空のobj)に入れる。
        //    tec.target = other.gameObject;
        //}
    }
}
