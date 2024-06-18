using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tool : Editor
{
    private void OnEnable()
    {
        Handles.DrawPolyLine(Vector3.zero, Vector3.one);
    }
}
