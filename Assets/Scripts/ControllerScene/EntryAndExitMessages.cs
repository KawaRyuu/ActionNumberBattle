using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntryAndExitMessages : MonoBehaviour
{
    // �v���C���[�������Ɏ󂯎��ʒm
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        print($"�v���C���[#{playerInput.user.index}������");
    }

    // �v���C���[�ގ����Ɏ󂯎��ʒm
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        print($"�v���C���[#{playerInput.user.index}���ގ�");
    }
}
