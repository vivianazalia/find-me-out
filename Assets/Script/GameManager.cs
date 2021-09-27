using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<Player> players = new List<Player>();
    public List<GameObject> Maps = new List<GameObject>(); //Di set di unity
    public enum State
    {
        Wait,
        Play,
        End
    }

    // Start is called before the first frame update
    void Start()
    {
        //Tampilkan menu pilih map
        //Pilih map
        //Tampilkan room
        //Tunggu semua player ready
        //Mulai main
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
