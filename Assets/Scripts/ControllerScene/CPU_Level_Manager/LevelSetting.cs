using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSetting : MonoBehaviour
{
    //Dropdownを格納する変数
    [SerializeField] private Dropdown dropdown1;
    [SerializeField] private Dropdown dropdown2;
    [SerializeField] private Dropdown dropdown3;

    //難易度のID
    public enum Level_ID
    {
        EASY,       //やさしい
        NORMAL,     //ふつう
        DIFFICULT   //つよい
    }

    Level_ID Level_1;
    Level_ID Level_2;
    Level_ID Level_3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /**********CPU1の判定****************/
        if (dropdown1.value == 0)
            SetCPULevel_1(Level_ID.EASY);

        if (dropdown1.value == 1)
            SetCPULevel_1(Level_ID.NORMAL);

        if (dropdown1.value == 2)
            SetCPULevel_1(Level_ID.DIFFICULT);

        /**********CPU2の判定****************/
        if (dropdown2.value == 0)
            SetCPULevel_2(Level_ID.EASY);

        if (dropdown2.value == 1)
            SetCPULevel_2(Level_ID.NORMAL);

        if (dropdown2.value == 2)
            SetCPULevel_2(Level_ID.DIFFICULT);

        /**********CPU3の判定****************/
        if (dropdown3.value == 0)
            SetCPULevel_3(Level_ID.EASY);

        if (dropdown3.value == 1)
            SetCPULevel_3(Level_ID.NORMAL);

        if (dropdown3.value == 2)
            SetCPULevel_3(Level_ID.DIFFICULT);

        Debug.Log(("現在のCPU1のつよさは") + GetCPULevel_1());
        Debug.Log(("現在のCPU2のつよさは") + GetCPULevel_2());
        Debug.Log(("現在のCPU3のつよさは") + GetCPULevel_3());
    }

    //CPUの難易度を決める関数
    void SetCPULevel_1(Level_ID LevelSetting)
    {
        Level_1 = LevelSetting;
    }
    void SetCPULevel_2(Level_ID LevelSetting)
    {
        Level_2 = LevelSetting;
    }
    void SetCPULevel_3(Level_ID LevelSetting)
    {
        Level_3 = LevelSetting;
    }


    //CPUの決めたつよさの難易度をゲームメインにいるCPUに受け渡す
    public Level_ID GetCPULevel_1()
    {
        return Level_1;
    }

    public Level_ID GetCPULevel_2()
    {
        return Level_2;
    }

    public Level_ID GetCPULevel_3()
    {
        return Level_3;
    }
}
