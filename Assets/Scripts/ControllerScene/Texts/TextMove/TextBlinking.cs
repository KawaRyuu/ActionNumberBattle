using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinking : MonoBehaviour
{
    public Text dotweenText;
    public float dotweenInterval;


    void Start()
    {
        //点滅処理
        dotweenText.DOFade(0.0f, dotweenInterval)   // アルファ値を0にしていく
                   .SetLoops(-1, LoopType.Yoyo);    // 行き来を無限に繰り返す
    }
}
