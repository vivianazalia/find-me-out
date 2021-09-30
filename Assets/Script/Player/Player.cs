using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();


    [SerializeField] PlayerSettings playerSettings; //Di set di inspector
    IPlayerInput playerInput;
    PlayerMovement playerMovement;

    private void Awake()
    {
        // TODO: Buat class KeyboardInput yg memakai IPlayerInput. AiInput gausah
        // playerInput = playerSettings.IsAI ? new AiInput() as IPlayerInput : new KeyboardInput() as IPlayerInput;
        playerInput = new KeyboardInput();
        playerMovement = new PlayerMovement(playerInput, transform, playerSettings);
    }

    public void UseSkill(char c)
    {
        //Cari 'c' itu buat mengaktifkan skill mana
        foreach(var s in skills)
        {
            if (s.GetInvokeKey() == c)
            {
                s.Invoke();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
