using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckValue : MonoBehaviour
{
    [SerializeField] private string SectionTag;

    public void Check()
    {
        Ray ray = new Ray(transform.position, Vector3.left);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == SectionTag)
            {
                GameManager.instance.PlayerScore = hit.collider.GetComponent<SegmentController>().Value;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.left, Color.blue);
    }
}
