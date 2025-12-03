using UnityEngine;
using UnityEngine.UI;

public class PingPongScoreManager : MonoBehaviour
{
    [Header("Referencias físicas")]
    public Collider firstPlayerTable;
    public Collider secondPlayerTable;
    public Collider firstPlayerFloor;
    public Collider secondPlayerFloor;

    [Header("UI Score")]
    public Text scoreFirstPlayerText;
    public Text scoreSecondPlayerText;

    private enum LastHit
    {
        None,
        FirstPlayer,
        SecondPlayer
    }

    private LastHit lastHit = LastHit.None;
    private bool hasHitTableThisRally = false;

    private int scoreFirstPlayer = 0;
    private int scoreSecondPlayer = 0;

    private void OnCollisionEnter(Collision collision)
    {
        Collider col = collision.collider;

        if (col == firstPlayerTable)
        {
            Debug.Log("Tocó mesa FIRST PLAYER");
            HandleHit(LastHit.FirstPlayer);
        }
        else if (col == secondPlayerTable)
        {
            Debug.Log("Tocó mesa SECOND PLAYER");
            HandleHit(LastHit.SecondPlayer);
        }
        else if (col == firstPlayerFloor)
        {
            Debug.Log("Tocó piso FIRST PLAYER");
            HandleFloorHit(LastHit.FirstPlayer);
        }
        else if (col == secondPlayerFloor)
        {
            Debug.Log("Tocó piso SECOND PLAYER");
            HandleFloorHit(LastHit.SecondPlayer);
        }
    }

    private void HandleHit(LastHit newHit)
    {
        if (lastHit == newHit)
        {
            // Double bounce
            Debug.Log("DOBLE REBOTE en " + newHit);
            AwardPoint(newHit == LastHit.FirstPlayer ? false : true);
            return;
        }

        lastHit = newHit;
        hasHitTableThisRally = true;
    }

    private void HandleFloorHit(LastHit floorSide)
    {
        if (!hasHitTableThisRally)
        {
            // Direct to floor
            Debug.Log("DIRECTO AL PISO EN " + floorSide);
            AwardPoint(floorSide == LastHit.FirstPlayer);
            return;
        }

        // Hit table then floor
        Debug.Log("LLEGÓ AL PISO DESPUÉS DE MESA: " + floorSide);
        AwardPoint(floorSide == LastHit.FirstPlayer ? false : true);
    }

    private void AwardPoint(bool pointForFirstPlayer)
    {
        if (pointForFirstPlayer)
        {
            scoreFirstPlayer++;
            Debug.Log($"PUNTO PARA FIRST PLAYER — Score: {scoreFirstPlayer} - {scoreSecondPlayer}");
        }
        else
        {
            scoreSecondPlayer++;
            Debug.Log($"PUNTO PARA SECOND PLAYER — Score: {scoreFirstPlayer} - {scoreSecondPlayer}");
        }

        UpdateUI();
        ResetRally();
    }

    private void UpdateUI()
    {
        if (scoreFirstPlayerText) scoreFirstPlayerText.text = scoreFirstPlayer.ToString();
        if (scoreSecondPlayerText) scoreSecondPlayerText.text = scoreSecondPlayer.ToString();
    }

    private void ResetRally()
    {
        lastHit = LastHit.None;
        hasHitTableThisRally = false;
        Debug.Log("Rally reiniciado");
    }
}
