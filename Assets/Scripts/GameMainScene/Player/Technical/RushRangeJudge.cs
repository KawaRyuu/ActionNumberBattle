using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushRangeJudge : MonoBehaviour
{
    TechnicalData tec;

    [SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        tec = Player.GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /************当たった時の処理(何かの当たった時)*****************/
    private void OnTriggerEnter2D(Collider2D other)
    {
        //もしトッシン範囲のなかにPlayerのtagがあったなら
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("敵発見");
            //target(空のobj)に入れる。
            tec.target = other.gameObject;
        }
    }
}
