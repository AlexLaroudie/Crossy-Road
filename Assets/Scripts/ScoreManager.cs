using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; // R�f�rence � un TextMeshPro (UI)
    private int score;

    // La rang�e la plus haute atteinte jusqu�ici
    private int highestRowReached = 0;

    private void Start()
    {
        score = 0;
        UpdateScoreUI();
    }

    /// <summary>
    /// M�thode appel�e quand le joueur atteint une nouvelle rang�e (indexRow).
    /// On ne gagne des points que si c�est > highestRowReached.
    /// </summary>
    public void OnNewRowReached(int indexRow)
    {
        if (indexRow > highestRowReached)
        {
            // On gagne la diff�rence (en g�n�ral, +1)
            int gainedPoints = indexRow - highestRowReached;
            score += gainedPoints;
            highestRowReached = indexRow;
            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
