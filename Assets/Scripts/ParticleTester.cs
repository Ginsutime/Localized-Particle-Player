using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleTester : MonoBehaviour
{
    public UnityEvent OnKeyPress1;
    public UnityEvent OnKeyPress2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(1);
            OnKeyPress1.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(2);
            OnKeyPress2.Invoke();
        }
    }
}
