using UnityEngine;

public class NotAdsZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AdvZone.inNoAdvZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AdvZone.inNoAdvZone = false;
        }
    }
}
