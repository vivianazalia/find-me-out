using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public float range;
    //public List<InGameCharacterPlayer> targets = new List<InGameCharacterPlayer>();

    public InGameCharacterPlayer playerTarget;

    public List<ObjectForHide> objects = new List<ObjectForHide>();

    public Transform bulletPos;

    private void Update()
    {
        AddTarget();
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        //bool isHit = Physics.Raycast(transform.position, transform.forward * -1, out hit, range);
        bool isHit = Physics.SphereCast(transform.position, range, transform.forward * -1, out hit, range);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Debug.DrawRay(transform.position, transform.forward * hit.distance * -1);
            Gizmos.DrawWireSphere(transform.position + transform.forward * hit.distance * -1, range);
            //Debug.Log("hit name playerFinder: " + hit.transform.gameObject.name);
        }
        else
        {
            //Debug.Log("No Hit player");
            Gizmos.color = Color.green;
            Debug.DrawRay(transform.position, transform.forward * range * -1);
            Gizmos.DrawWireSphere(transform.position + transform.forward * range * -1, range);
        }
    }

    public void AddTarget()
    {
        RaycastHit hit;
        //bool isHit = Physics.Raycast(transform.position, transform.forward * -1, out hit, range);
        bool isHit = Physics.SphereCast(transform.position, range, transform.forward * -1, out hit, range);

        if (isHit)
        {
            InGameCharacterPlayer target = hit.collider.GetComponent<InGameCharacterPlayer>();
            if (target != null && target.playerType == PlayerType.thief)
            {
                playerTarget = target;
                //if (!targets.Contains(target))
                //{
                //    targets.Add(target);
                //}
            }
        }
        else
        {
            if (playerTarget)
            {
                playerTarget = null;
            }
        }
        
        //if(!isHit || hit.transform.GetComponent<InGameCharacterPlayer>() == null)
        //{
        //    if(targets.Count > 0)
        //    {
        //        targets.Clear();
        //    }
        //}
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    var player = collision.GetComponent<InGameCharacterPlayer>();
    //    //Debug.Log("Collision player : " + player);
    //    if (player && player.playerType == PlayerType.thief)
    //    {
    //        if (!targets.Contains(player))
    //        {
    //            targets.Add(player);
    //        }
    //    }
    //
    //    var obj = collision.GetComponent<ObjectForHide>();
    //
    //    var pilot = GetComponentInParent<InGameCharacterPlayer>();
    //    //Debug.Log("Pilot : " + pilot + " Type : " + pilot.playerType);
    //    if (pilot != null && pilot.playerType == PlayerType.thief)
    //    {
    //        if (obj && collision.tag == "ObjectForHide")
    //        {
    //            if (!objects.Contains(obj))
    //            {
    //                objects.Add(obj);
    //                obj.SetTargetPlayer(pilot);
    //                pilot.ShowPopUpTextObjectForHide(true);
    //            }
    //        }
    //    }
    //
    //}
    //
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    var player = collision.GetComponent<InGameCharacterPlayer>();
    //    if (player && player.playerType == PlayerType.thief)
    //    {
    //        if (targets.Contains(player))
    //        {
    //            targets.Remove(player);
    //        }
    //    }
    //
    //    var obj = collision.GetComponent<ObjectForHide>();
    //    var pilot = GetComponentInParent<InGameCharacterPlayer>();
    //    if (pilot != null && pilot.playerType == PlayerType.thief)
    //    {
    //        if (obj && collision.tag == "ObjectForHide")
    //        {
    //            if (objects.Contains(obj))
    //            {
    //                pilot.ShowPopUpTextObjectForHide(false);
    //                objects.Remove(obj);
    //                obj.RemoveTargetPlayer(pilot);
    //            }
    //        }
    //    }
    //}

    public InGameCharacterPlayer GetFirstTarget()
    {
        if (playerTarget)
        {
            return playerTarget;
        }

        return null;
        //float dist = float.MaxValue;
        //InGameCharacterPlayer closeTarget = null;
        //foreach (var player in targets)
        //{
        //    float newDist = Vector2.Distance(transform.position, player.transform.position);
        //    if (newDist < dist)
        //    {
        //        dist = newDist;
        //        closeTarget = player;
        //    }
        //}
        //
        //return closeTarget;
    }
}
