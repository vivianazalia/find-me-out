using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    int lifetime = 300;
    float fadetime = 1f;
    float fadedelay = 0;
    int opacity = 100;
    public void ShowForSec(int seconds)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Kurangi opacity sebanyak 100/fadetime (di convert ke per frame dulu), kecuali sedang fadedelay
        //Kurangi 1 fadedelay per detik
        //destroy setelah 300 detik
    }
}
