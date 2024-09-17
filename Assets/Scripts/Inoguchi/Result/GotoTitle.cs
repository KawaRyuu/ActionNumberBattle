using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GotoTitle : MonoBehaviour
{
    //Player‚ÌInputSystem
    PlayerInput playerInput;

    private void Awake()
    {
        TryGetComponent(out playerInput);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool SelectButton = playerInput.actions["SelectButton"].WasPressedThisFrame();
        Debug.Log(SelectButton);

        if (SelectButton)
        {
            Debug.Log("o");
            ChangeScene();
        }
    }

    //ƒ^ƒCƒgƒ‹‰æ–Ê‚É‘JˆÚ
    void ChangeScene()
    {
        Debug.Log("‚½‚¢‚Æ‚é");
        SceneManager.LoadScene("TitleScene");
    }
}
