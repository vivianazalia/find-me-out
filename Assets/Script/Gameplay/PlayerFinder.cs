using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    public List<InGameCharacterPlayer> targets = new List<InGameCharacterPlayer>();

    public List<ObjectForHide> objects = new List<ObjectForHide>();

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<InGameCharacterPlayer>();
        //Debug.Log("Collision player : " + player);
        if (player && player.playerType == PlayerType.thief)
        {
            if (!targets.Contains(player))
            {
                targets.Add(player);
            }
        }

        var obj = collision.GetComponent<ObjectForHide>();

        var pilot = GetComponentInParent<InGameCharacterPlayer>();
        //Debug.Log("Pilot : " + pilot + " Type : " + pilot.playerType);
        if (pilot != null && pilot.playerType == PlayerType.thief)
        {
            if (obj && collision.tag == "ObjectForHide")
            {
                if (!objects.Contains(obj))
                {
                    objects.Add(obj);
                    obj.SetTargetPlayer(pilot);
                    pilot.ShowPopUpTextObjectForHide(true);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<InGameCharacterPlayer>();
        if (player && player.playerType == PlayerType.thief)
        {
            if (targets.Contains(player))
            {
                targets.Remove(player);
            }
        }

        var obj = collision.GetComponent<ObjectForHide>();
        var pilot = GetComponentInParent<InGameCharacterPlayer>();
        if (pilot != null && pilot.playerType == PlayerType.thief)
        {
            if (obj && collision.tag == "ObjectForHide")
            {
                if (objects.Contains(obj))
                {
                    pilot.ShowPopUpTextObjectForHide(false);
                    objects.Remove(obj);
                    obj.RemoveTargetPlayer(pilot);
                    //obj.PopupText.gameObject.SetActive(false);
                }
            }
        }
    }

    public InGameCharacterPlayer GetFirstTarget()
    {
        float dist = float.MaxValue;
        InGameCharacterPlayer closeTarget = null;
        foreach (var player in targets)
        {
            float newDist = Vector2.Distance(transform.position, player.transform.position);
            if (newDist < dist)
            {
                dist = newDist;
                closeTarget = player;
            }
        }

        return closeTarget;
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
