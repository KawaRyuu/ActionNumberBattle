using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class EntryAndExitMessages : MonoBehaviour
{
    int PlayerNumber = 0;       //プレイヤーの数
    public GameObject Original; //Playerを複製させるために使う空のオブジェクト
    private void Awake()
    {
       
            PlayerClone();
           
        
    }

    // プレイヤー入室時に受け取る通知
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        print($"プレイヤー#{playerInput.user.index}が入室");
    }

    // プレイヤー退室時に受け取る通知
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        print($"プレイヤー#{playerInput.user.index}が退室");
        PlayerNumber--;
    }

    //Playerの数をリターンで返す。
    public int  PlayerNumberRetun()
    {
        return PlayerNumber;
    }


    //Playerを複製させる。
    void PlayerClone()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject player = Instantiate(Original, new Vector3(1, 0, 0), Quaternion.identity);
            player.GetComponent<Player>().SetPlayerId((Player.PLAYER_ID)i);

            //プレイヤーが何人入ったかカウントする。
            PlayerNumber++;
            Debug.Log("カウント" + PlayerNumber);
        }
    }
}
