using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFactory : Factory
{

    [SerializeField] GameObject m_Prefab;

    public override void Generate(Vector3 position)
    {

    }

    public override Transform GenerateObject(Vector3 position)
    {
        Transform result = null;

        return result;
    }
}
