using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�I�u�W�F�N�g���ړ������Ƃ�
        this.transform.position += new Vector3(0.02f, 0, 0);
    }
}
