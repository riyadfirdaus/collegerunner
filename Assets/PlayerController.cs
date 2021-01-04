using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int a = 10, b = 20;
    float flo = 1.123f;
    string s = "sata mau makan";
    bool fact = true;

    // Start is called before the first frame update
    void start()
    {
        if(fact == true)
        {
            print(a.ToString() + "b = " + b.ToString() + s + flo.ToString());
        }
    }
}
