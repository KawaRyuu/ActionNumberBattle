using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappears : MonoBehaviour
{
    //3秒後に削除をする設定
    public float deleteTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //破壊(このスクリプトがついているobをdeleteTime後に発動)
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
