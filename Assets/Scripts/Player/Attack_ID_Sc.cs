using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_ID_Sc : MonoBehaviour
{
    //UŒ‚‚Ìí—Ş
    public enum ATTACK
    {
        WING,
        SWAROWRETURN,
        STRIKE_BACK,
        RUSHATTACK,
    }

    ATTACK           attack_type;
    Player.PLAYER_ID player_id;

    //î•ñ‚ğ‰Šú‰»
    public void InitializeAttackInfo(ATTACK attack, Player.PLAYER_ID player)
    {
        attack_type = attack;
        player_id = player;
    }

    //UŒ‚‚Ìí—Ş‚ğæ“¾
    public ATTACK AttackID_Retrun()
    {
        return attack_type;
    }

    //ƒvƒŒƒCƒ„[‚ÌID‚ğæ“¾
    public Player.PLAYER_ID PlayerID_Return()
    {
        return player_id;
    }
}
