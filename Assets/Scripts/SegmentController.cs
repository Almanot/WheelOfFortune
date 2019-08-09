using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentController : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private Text Visual;
    [SerializeField] public float myAngle;

    public int Value
    {
        get { return value; }
    }

    public void SetValues(int newvalue)
    {
        value = newvalue;
        Visual.text = newvalue.ToString();
    }
}
