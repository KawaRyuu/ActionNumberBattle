using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class EntryAndExitMessages : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Image[] circle_mains;
    [SerializeField] Vector3[] positions = new Vector3[4];
    [SerializeField] ResultScoreScriptableObject result_score;



    private void Awake()
    {
        
        PlayerClone();
    }

    private void Start()
    {
        result_score.players_array = GameObject.FindGameObjectsWithTag("Player");
    }

    void PlayerClone()
    {
        for(int i = 0;i<4;i++)
        {
            GameObject clone = Instantiate(player, positions[i], Quaternion.identity);
            clone.GetComponent<Player>().SetPlayerId((Player.PLAYER_ID)i);
            clone.transform.position = positions[i];
            Debug.Log("ポジション");

            //clone.GetComponent<WazaCoolTimeMove>().SetObj(circle_mains[i]);
        }
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
    }
}
