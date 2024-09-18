using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreScriptableObject", menuName = "Result_Scriptable", order = 0)]
public class ResultScoreScriptableObject : ScriptableObject
{
    public GameObject[] players_array        = new GameObject[4];

    public int[] player_total_sums           = new int[4]; //最終合計値
    public int[] player_total_bonus_points   = new int[4]; //ボーナスポイントの合計
    public int[] player_number_sums          = new int[4]; //数字の合計
    public int[] player_change_number_counts = new int[4]; 

}
