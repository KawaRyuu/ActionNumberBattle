using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�{�^��������������s����֐��@���s���邽�߂ɂ̓{�^���֊֐��o�^���K�v
    public void ChangeScene(string ControllerScene)
    {
        //�^�C�g���V�[������R���g���[���[�ݒ�̃V�[����
        SceneManager.LoadScene("ControllerScene");
    }
}
