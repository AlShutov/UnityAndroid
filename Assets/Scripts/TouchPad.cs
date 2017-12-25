using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TouchPad : MonoBehaviour
{
     

    public bool touched;

    private void OnMouseDown()
    {
        touched = true;
        Debug.Log(touched);
    }
    private void OnMouseUp()
    {
        touched = false;
        Debug.Log(touched);
    }
}

