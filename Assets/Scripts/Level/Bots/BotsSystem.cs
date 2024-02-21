using UnityEngine;

public class BotsSystem : MonoBehaviour
{
    [SerializeField]
    private Transform[] destinationPoints;
    [SerializeField]
    private Transform[] finishDestinationPoints;
    BotController botController;

    int nextFinishDestination;
    public Transform GetRandomDestinationPoint()
    {
        int randomNumber = Random.Range(0, destinationPoints.Length);
        if (randomNumber < finishDestinationPoints.Length)
        { 
            nextFinishDestination = randomNumber;
            botController.isNextDestinationPointFinish = true;
            botController.SetNextPoint(finishDestinationPoints[nextFinishDestination]);
        }
        return destinationPoints[randomNumber];
    }
   
    public void SetCurrentBot(BotController botController)
    {
        this.botController = botController;
    }
   

}
