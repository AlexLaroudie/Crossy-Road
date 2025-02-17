using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;         // R�f�rence au MainMenuPanel
    [SerializeField] private GameObject characterSelectionPanel; // R�f�rence au CharacterSelectionPanel

    // Bouton "Play"
    public void OnClickPlay()
    {
        // Cache le menu principal
        mainMenuPanel.SetActive(false);

        // Ici, tu peux lancer le jeu, d�bloquer le joueur, etc.
        // ex: GameManager.Instance.StartGame();
    }

    // Bouton "Choix Personnage"
    public void OnClickCharacterSelection()
    {
        // Cache le menu principal
        mainMenuPanel.SetActive(false);
        // Affiche le panel de s�lection
        characterSelectionPanel.SetActive(true);
    }

    // Bouton "Retour" ou "Confirm" depuis le panel de s�lection
    // (si tu veux revenir au menu)
    public void OnClickBackToMainMenu()
    {
        characterSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
