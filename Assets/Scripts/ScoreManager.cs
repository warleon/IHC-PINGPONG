using UnityEngine;

public class PingPongScoreManager : MonoBehaviour
{
    public string firstPlayerTableTag = "FirstPlayer";
    public string secondPlayerTableTag = "SecondPlayer";
    public string firstPlayerFloorTag = "FirstPlayerFloor";
    public string secondPlayerFloorTag = "SecondPlayerFloor";

    private enum LastHit
    {
        None,
        FirstPlayer,
        SecondPlayer
    }

    private LastHit lastHit = LastHit.None;
    private bool hasHitTableThisRally = false;

    public int scoreFirstPlayer = 0;
    public int scoreSecondPlayer = 0;

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.tag;

        switch (tag)
        {
            case "FirstPlayer":
                Debug.Log("Tocó mesa FIRST PLAYER");
                HandleHit(LastHit.FirstPlayer);
                break;

            case "SecondPlayer":
                Debug.Log("Tocó mesa SECOND PLAYER");
                HandleHit(LastHit.SecondPlayer);
                break;

            case "FirstPlayerFloor":
                Debug.Log("Tocó piso FIRST PLAYER");
                HandleFloorHit(LastHit.FirstPlayer);
                break;

            case "SecondPlayerFloor":
                Debug.Log("Tocó piso SECOND PLAYER");
                HandleFloorHit(LastHit.SecondPlayer);
                break;
        }
    }

    private void HandleHit(LastHit newHit)
    {
        if (lastHit == newHit) // two consecutive hits
        {
            Debug.Log("DOBLE REBOTE en " + newHit);
            AwardPoint(newHit == LastHit.FirstPlayer ? false : true);
            return;
        }

        lastHit = newHit;
        hasHitTableThisRally = true;
    }

    private void HandleFloorHit(LastHit floorSide)
    {
        if (!hasHitTableThisRally) // direct floor
        {
            Debug.Log("DIRECTO AL PISO EN " + floorSide);
            AwardPoint(floorSide == LastHit.FirstPlayer);
            return;
        }

        Debug.Log("LLEGÓ AL PISO DESPUÉS DE MESA: " + floorSide);
        AwardPoint(floorSide == LastHit.FirstPlayer ? false : true);
    }

    private void AwardPoint(bool pointForFirstPlayer)
    {
        if (pointForFirstPlayer)
        {
            scoreFirstPlayer++;
            Debug.Log("\nPUNTO PARA FIRST PLAYER — Score: " + scoreFirstPlayer + " - " + scoreSecondPlayer + "\n");
        }
        else
        {
            scoreSecondPlayer++;
            Debug.Log("\nPUNTO PARA SECOND PLAYER — Score: " + scoreFirstPlayer + " - " + scoreSecondPlayer + "\n");
        }

        ResetRally();
    }

    private void ResetRally()
    {
        lastHit = LastHit.None;
        hasHitTableThisRally = false;
        Debug.Log("Rally reiniciado\n");
    }
}
