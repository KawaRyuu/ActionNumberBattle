using UnityEngine;
using UnityEngine.UIElements;


//アイテムの種類と処理のクラス
//プレイヤーのステータス参照
public class Item
{
    PlayerData Pl;

    /*アイテムメモ：同じ種類のアイテムは所持不可能*/

    /*アイテム処理時に使う変数*/
    public float speed_up_timer = 0.0f;//関数Speed_Upで使用するタイマー

    /*アイテムの処理をする関数*/

    //自動的にランダム奪取が低い数へ適用される。
    public int Change_min(int []num,int size)//引数：個人ごとの数字のデータ入っている配列、配列のサイズ
    {
        int min = 9;
        for(int i = 0; i <= size;i++)
        {
            if (num[i] < min)//配列aのi番目がminより小さい数字なら
                min= i;//minに配列番号iを代入する
        }

        //一番小さい数字が入っていた配列番号を返す
        return min;
        /*
         * メモ：このアイテム使用した後に
         * 自身の数字を変更するアイテムを使用したら
         * 返した配列番号が最小ではない値になる可能性がある
         * 
         *→解決案：数字変更するアイテム使用時にもう一度この関数を呼び出す
         */

    }

    //4枚の数のうち、一番低い数が１～９にランダムで変わる。
    public void NUM_Random(int[] num, int size)
    {
        //一番低い数字が入っている配列番号を取得(minに代入)
        int min = Change_min(num,size);
        num[min] = Random.Range(1, 10);
    }

    //4枚の数の中で1以外の小さい数が1枚1に変わる。
    public void NumMin_One(int[] num, int size)
    {
        //一番低い数字が入っている配列番号を取得(minに代入)
        int min = Change_min(num, size);
        num[min] = 1;

        /*注意点、まだ１も１に変えるScriptになっています*/
    }

    //自身の数字をランダムな人と交換（交換している人同士は見える）
    public void Change_Random()
    {
        
    }

    //自身移動速度UP（3秒間）
    /*アイテム使ったときに移動速度アップの関数を持ってくる*/
    public void Item_Speed_Up()
    {
        Pl.Speed = 300.0f;
        speed_up_timer += Time.deltaTime;
        //別のアイテム使用時にバグ起きる可能性あり
        if (speed_up_timer > 3.0f)
            Pl.Speed = 5.0f;

    }

    //相手に投げ飛ばして移動速度Down（2秒間）
    /*PlayerDataに鈍足処理あり→鈍足タグついたオブジェクト召喚プログラムへ*/
    public void Item_SpDown()
    {
        //plefab召喚→プレイヤーが向いている向きに発射(3マス分進んだら消える)
        //plefab召喚

        //召喚したplefabを一定方向に発射
        //３マス進んだらdeleteする：if文
    }

    //一撃気絶のアイテム
    public void Critical()
    {
        Pl.Attack = 3;
    }

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
