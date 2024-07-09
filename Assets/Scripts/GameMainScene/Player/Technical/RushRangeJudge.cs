using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushRangeJudge : MonoBehaviour
{
    TechnicalData tec;

    [SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        tec = Player.GetComponent<TechnicalData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /************“–‚½‚Á‚½‚Ìˆ—(‰½‚©‚Ì“–‚½‚Á‚½)*****************/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("“G”­Œ©");
            tec.target = other.gameObject;
        }
    }
}
