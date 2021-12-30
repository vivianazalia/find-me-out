using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayer : MonoBehaviour
{
    public float range;

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        //bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, range);
        bool isHit = Physics.SphereCast(transform.position, range, transform.forward, out hit, range);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Debug.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * hit.distance, range);
            Debug.Log("hit name : " + hit.transform.gameObject.name);
        }
        else
        {
            Debug.Log("No Hit");
            Gizmos.color = Color.green;
            Debug.DrawRay(transform.position, transform.forward * range);
            Gizmos.DrawWireSphere(transform.position + transform.forward * range, range);
        }
    }
}
