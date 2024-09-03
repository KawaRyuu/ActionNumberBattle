using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    Player.PLAYER_ID playerID;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(new Vector3(0, 0, 90));
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトが移動したとき
        this.transform.position += new Vector3(0.02f, 0, 0);
       
    }
}
