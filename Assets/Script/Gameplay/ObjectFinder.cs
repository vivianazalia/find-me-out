using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinder : MonoBehaviour
{
    public float range;

    public List<ObjectForHide> objects = new List<ObjectForHide>();

    InGameCharacterPlayer owner;

    private void Start()
    {
        owner = GetComponentInParent<InGameCharacterPlayer>();
    }

    private void Update()
    {
        AddTarget();
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, range);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Debug.DrawRay(transform.position, transform.forward * hit.distance);
            //Debug.Log("hit name obj objectFinder : " + hit.transform.gameObject.name);
        }
        else
        {
            //Debug.Log("No Hit obj");
            Gizmos.color = Color.green;
            Debug.DrawRay(transform.position, transform.forward * range);
        }
    }

    public void AddTarget()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, range);

        if (isHit)
        {
            ObjectForHide target = hit.transform.GetComponent<ObjectForHide>();
            if (target != null && target.CompareTag("ObjectForHide"))
            {
                if (!objects.Contains(target))
                {
                    objects.Add(target);

                    if (owner)
                    {
                        target.SetTargetPlayer(owner);
                        owner.ShowPopUpTextObjectForHide(true);
                    }
                    
                }
            }
        }

        if (!isHit || hit.transform.GetComponent<ObjectForHide>() == null)
        {
            if (objects.Count > 0)
            {
                foreach(ObjectForHide obj in objects)
                {
                    obj.RemoveTargetPlayer(owner);
                }
                owner.ShowPopUpTextObjectForHide(false);
                objects.Clear();
            }
        }
    }

    public ObjectForHide GetTargetHide()
    {
        float dist = float.MaxValue;
        ObjectForHide closeTarget = null;
        foreach (var obj in objects)
        {
            float newDist = Vector2.Distance(transform.position, obj.transform.position);
            if (newDist < dist)
            {
                dist = newDist;
                closeTarget = obj;
            }
        }

        return closeTarget;
    }
}
