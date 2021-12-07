using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDisguise : Skill
{
    Vector2 touchPos;
    private void OnEnable()
    {
        InputManager.OnTouchEnd += OnTouchEnd;
    }

    private void OnTouchEnd(Vector2 position)
    {
        if (skillState == SkillState.Selected)
        {
            touchPos = position;
            OnInvoke();
        }
    }

    protected override void Initialize()
    {
        this.skillName = "Disguise";
        this.useSelectPhase = true;
    }

    protected override void OnInvoke()
    {
        //Buat ray, ambil objeknya, jika objeknya termasuk Transformable, ganti objek ini menjadi objek itu <-- how???
        Debug.Log($"Skill {skillName} Invoked!");
        if (owner.IsUsingDefaultChild)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                if (objectHit.TryGetComponent<Disguiseables>(out Disguiseables obj))
                {
                    owner.SetChild(obj.gameObject);
                }
                // Do something with the object that was hit by the raycast.
            }
        }
        else
        {
            owner.UseDefaultChild();
        }
        UpdateSkillState(SkillState.Cooldown);
    }

}
