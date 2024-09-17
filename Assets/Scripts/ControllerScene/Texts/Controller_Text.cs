using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_Text : MonoBehaviour
{
    ControllerConnection connection;

    [SerializeField]private Text[] PlayerConnectText;


    //èâä˙âª
    void Start()
    {
        connection = this.GetComponent<ControllerConnection>();

        for (int i = 0; i < 4; i++)
        {
            PlayerConnectText[i].text = ("ñ¢ê⁄ë±");
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < connection.GetControllerNumber(); i++)
        {
            PlayerConnectText[i].text = ("ê⁄ë±");
        }
    }
}
