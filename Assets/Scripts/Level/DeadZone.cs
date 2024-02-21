using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public static event Action PlayerDead;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {                                 
            PlayerDead?.Invoke();
        }
        if(other.CompareTag("Bot"))
        {          
            other.GetComponent<BotController>().ReturnToSpawn();
        }
    }
}
