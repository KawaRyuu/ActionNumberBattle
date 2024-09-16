using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタン押すと技選択のシーンへ移行する。
    public void SceneChange()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
