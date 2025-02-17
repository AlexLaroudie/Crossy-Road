using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; // Référence à un TextMeshPro (UI)
    private int score;

    // La rangée la plus haute atteinte jusqu’ici
    private int highestRowReached = 0;

    private void Start()
    {
        score = 0;
        UpdateScoreUI();
    }

    /// <summary>
    /// Méthode appelée quand le joueur atteint une nouvelle rangée (indexRow).
    /// On ne gagne des points que si c’est > highestRowReached.
    /// </summary>
    public void OnNewRowReached(int indexRow)
    {
        if (indexRow > highestRowReached)
        {
            // On gagne la différence (en général, +1)
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
