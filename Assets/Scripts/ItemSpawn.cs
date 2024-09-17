using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject Item_r;
    [SerializeField] int num;
    //
    bool[] ItemSpawnflg = { false,false,false,false };
    // 経過時間
    public float time;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 前フレームからの時間を加算していく
        time = time + Time.deltaTime;
        // 約5秒置きにランダムに生成されるようにする。
        if (time > 5.0f)
        {
            if (ItemSpawnflg[num] == false) 
            {
                Instantiate(Item_r, this.transform.position, Quaternion.identity);
                ItemSpawnflg[num] = true;

            }

                
            // 経過時間リセット
            time = 0f;
        }
    }
    
}
