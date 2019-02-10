using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField]
    Transform reticleT;

    [SerializeField, Range(0.1f, 100f)]
    float rayDistance;
    [SerializeField]
    Color rayColor = Color.magenta;
    [SerializeField]
    LayerMask vrLayer;

    [SerializeField]
    Transform cameraViewT;
    RaycastHit hit;

    Vector3 reticleInitialSize = Vector3.zero;

    void Start()
    {
        reticleInitialSize = reticleT.localScale;
    }


    void FixedUpdate()
    {
        if (Physics.Raycast(cameraViewT.position, cameraViewT.forward, 
            out hit, rayDistance, vrLayer))
        {
            if (hit.collider)
            {
                reticleT.localScale =  reticleInitialSize * hit.distance * 0.1f;
                reticleT.position = hit.point;
                reticleT.localRotation =
                    Quaternion.FromToRotation(Vector3.forward, hit.normal);
            }
        }
        else
        {
            reticleT.localScale = reticleInitialSize;
            reticleT.localPosition = transform.localPosition;
            reticleT.localRotation = transform.localRotation;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(cameraViewT.position, cameraViewT.forward * rayDistance);
    }
}
