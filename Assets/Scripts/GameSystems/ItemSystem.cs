using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//アイテムの種類と処理のクラス
//プレイヤーのステータス参照
public class Item
{
    //どのアイテムを所持しているかの変数


    //アイテムの処理をする関数

    //アイテムで無敵ではないが、自動的にランダム奪取が低い数へ適用される。
    public void Change_min(){}

    //4枚の数のうち、一番低い数が１～９にランダムで変わる。
    public void NUM_Random() {}

    //4枚の数の中で1以外の小さい数が1枚1に変わる。
    public void NumMin_One() {}

    //自身の数字をランダムな人と交換（交換している人同士は見える）
    public void Change_Random() { }

    //自身移動速度UP（3秒間）
    public void Speed_Up() { }

    //相手に投げ飛ばして移動速度Down（2秒間）
    public void SpDown() { }

    //一撃気絶のアイテム
    public void Critical() {  }	        

}

public class ItemSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
