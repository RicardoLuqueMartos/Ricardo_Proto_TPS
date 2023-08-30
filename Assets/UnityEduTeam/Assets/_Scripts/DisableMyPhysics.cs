using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMyPhysics : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] bool destroyMyCollider = false;

    private void OnEnable()
    {
        Invoke("DisablePhysics", delay);
    }

    private void DisablePhysics()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.isKinematic = true;

        if (destroyMyCollider)
        {
            var colliders = GetComponentsInParent<Collider>();

            for (int i = 0; i < colliders.Length; i++)
            {
                Destroy(colliders[i]);
            }
        }
    }
}
