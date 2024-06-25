using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBreak : MonoBehaviour
{
    //Playerスクリプトを参照する
    Player player;

    //10秒後に削除をする設定
    public float deleteTime = 10.0f;
    //破壊フラグ
    bool destroyFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        destroyFlg = false;
       
        //破壊(このスクリプトがついているobをdeleteTime後に発動)
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        //バックフラグがtrueなら
        if (player.stBackFlg)
        {
            destroyFlg = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //もしマークとPlayerが触れたなら
        if(collision.gameObject.tag=="Player")
        {
            //破壊フラグを取得済みなら
            if (destroyFlg)
            {
                Debug.Log("破壊");
                //マーク破壊
                Destroy(gameObject);
            }
        }
    }
}
