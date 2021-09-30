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
    State gamestate = State.End;

    // Start is called before the first frame update
    void Start()
    {
        //
        //Tampilkan menu pilih map
        //Pilih map
        //Tampilkan room
        //Tunggu semua player join
        //Tunggu semua player ready
        //Mulai main
        GameLoop(State.Wait);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameLoop(State _gamestate)
    {
        gamestate = _gamestate;
        switch (gamestate)
        {
            case (State.Wait):
                Wait();
                break;
            case (State.Play):
                Play();
                break;
            case (State.End):
                End();
                break;
        }
    }

    void Wait()
    {
        //Do something
        GameLoop(State.Play);
    }
    void Play()
    {
        //Do something
        //Moving?
        //Do skill?
        GameLoop(State.End); //Kalau game selesai, ke sini
    }
    void End()
    {

    }
}
