using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMyPhysics : MonoBehaviour
{
    [SerializeField] float delay = 2f;

    private void OnEnable()
    {
        Invoke("DisablePhysics", delay);
    }

    private void DisablePhysics()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.isKinematic = true;
    }
}
