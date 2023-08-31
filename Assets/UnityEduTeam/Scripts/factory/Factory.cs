using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public abstract void Generate(Vector3 position);

    public abstract Transform GenerateObject(Vector3 position);


}
