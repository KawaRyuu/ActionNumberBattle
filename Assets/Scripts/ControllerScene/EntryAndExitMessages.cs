using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntryAndExitMessages : MonoBehaviour
{
    int PlayerNumber = 0;


    // プレイヤー入室時に受け取る通知
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        print($"プレイヤー#{playerInput.user.index}が入室");
        //プレイヤーが何人入ったかカウントする。
        PlayerNumber++;
    }

    // プレイヤー退室時に受け取る通知
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        print($"プレイヤー#{playerInput.user.index}が退室");
    }

    //Playerの数をリターンで返す。
    public int  PlayerNumberRetun()
    {
        return PlayerNumber;
    }
}
