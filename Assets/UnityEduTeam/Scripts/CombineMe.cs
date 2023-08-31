using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMe : MonoBehaviour
{
    [SerializeField] float delay = 2f;

    private void OnEnable()
    {
        Invoke("Combine", delay);
    }

    private void Combine()
    {
        CombineChildrenAtTimeOrOrder comb = transform.GetComponentInParent<CombineChildrenAtTimeOrOrder>();

        if (comb != null)
            comb.Combine();
    }
}
