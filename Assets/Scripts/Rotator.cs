using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    private bool AllowRotate = false;
    [SerializeField] private float StartAngleValue = 10;
    private float angle;
    [SerializeField] private float decrease;
    [SerializeField] private float minimumRandomDecrease;
    [SerializeField] private float maximumRandomDecrease;
    [SerializeField] private CheckValue Checker;
    [SerializeField] private Button spinButton;

    // Update is called once per frame
    void Update()
    {
        if (AllowRotate)
        {
            Rotate();
            if(angle > 0)
            {
                angle -= decrease;
            }
            else
            {
                AllowRotate = false;
                angle = StartAngleValue;
                spinButton.interactable = true;
                Checker.Check();
            }
            //while (angle > 0)
            //{
            //    angle -= decrease;
            //}
        }
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 1, 0), angle);
    }

    public void LaunchTheWheel()
    {
        spinButton.interactable = false;
        angle = StartAngleValue;
        decrease = Random.Range(minimumRandomDecrease, maximumRandomDecrease);
        AllowRotate = true;
    }
}
