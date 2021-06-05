using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDebug : MonoBehaviour
{
    public bool w;
    public bool a;
    public bool s;
    public bool d;
    public bool space;

    void FixedUpdate()
    {
        w = Input.GetKey(KeyCode.W);
        a = Input.GetKey(KeyCode.A);
        s = Input.GetKey(KeyCode.S);
        d = Input.GetKey(KeyCode.D);
        space = Input.GetKey(KeyCode.Space);

    }
}
