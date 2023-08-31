using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineChildrenAtTimeOrOrder : MonoBehaviour
{
    [SerializeField]
    float m_delay = -1;

    bool m_done = false;
 
    private void OnEnable()
    {
        if (m_delay >= 0)       
            Invoke("Combine", m_delay);
    }   

    public void Combine()
    {
        if (m_done == false)
        {
            MeshCombiner.PrepareCombine(gameObject, true, true);
            m_done = true;
        }
    }
}
