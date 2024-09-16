using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//このスクリプトは各プレイヤーに数を割り当てるマネジメントである
public class PlayerAssingnment : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] Text[] texts = new Text[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //プレイヤーが何pかを受け渡しする
    public void PlayerAssingnmentProcess()
    {
        //Playerのindexの数によりPlayerが何Pかを判別させるものを置く
        switch (playerInput.user.index)
        {
            //1p
            case 0:
                
                break;

            //2p
            case 1:
                break;

            //3p
            case 2:
                break;

            //4p
            case 3:
                break;
        }
    }
   
}
