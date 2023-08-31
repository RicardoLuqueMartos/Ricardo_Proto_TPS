using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePool : Pool
{
    private List<Transform> sphereReferences = new List<Transform>();

    public override Transform Get(Vector3 position)
    {
        Transform result= null;

        for (int i = 0; i < sphereReferences.Count; i++)
        {
            if (!sphereReferences[i].gameObject.activeInHierarchy)
            {
                sphereReferences[i].gameObject.SetActive(true);
                sphereReferences[i].position= position;
            }
        }

        return result;
    }

    public override void Release(Transform element)
    {
        element.gameObject.SetActive(false);
    }

}
