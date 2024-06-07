using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBreak : MonoBehaviour
{
    //Playerスクリプトを参照する
    Player player;

    //10秒後に削除をする設定
    public float deleteTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        //破壊(このスクリプトがついているobをdeleteTime後に発動)
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        //もしストライク&backが終わったらマーク破壊
        if (player.stBackFlg)
        {
            Debug.Log("破壊");
            Destroy(gameObject);
        }
    }
   
}
