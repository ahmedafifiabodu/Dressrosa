using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticalSystem : MonoBehaviour
{
    public static int quset1_Counter;
    [SerializeField] private PlayerIsometricMovement player;
    [SerializeField] private NPC[] charater;
    [SerializeField] private GameObject stone , note;

    private void Start()
    {
        quset1_Counter = 0;
        stone.SetActive(false);
        note.SetActive(false);
    }

    void Update()
    {
        if(quset1_Counter == 6)
        {
            player._invertDirection = false;
            for(int i = 0; i < charater.Length; i++) 
            {
                charater[i].StartMovement();
            }
            stone.SetActive(true);
            note.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
