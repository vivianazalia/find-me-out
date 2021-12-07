using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Footstep : MonoBehaviour
{
    int age;
    [SerializeField][Range(3,300)] int lifetime = 120;
    [SerializeField][Range(0,30)] float fadetime = 1f;
    [SerializeField][Range(0,10)] float fadedelay = 0;
    [SerializeField] Color footstepColor = new Color(0.5f, 0.5f, 0.5f, 1);
    float opacity = 1f;
    SpriteRenderer spriteRenderer;

    float timer, fadetimer;
    public void ShowForSec(int seconds)
    {
        opacity = age / lifetime;
    }

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //destroy setelah 300 detik
        if (opacity != 1f || opacity != 0f)
        {
            spriteRenderer.color = new Color(footstepColor.r, footstepColor.g, footstepColor.b, opacity);
        }
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0f;
            age++;
        }
        fadetimer += Time.deltaTime;
        if (fadetimer > fadetime/100 && opacity > 0)
        {
            opacity -= 0.01f;
        }
        if (age > lifetime)
        {
            Destroy(this);
        }
    }
}
