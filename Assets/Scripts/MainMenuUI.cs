using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;         // Référence au MainMenuPanel
    [SerializeField] private GameObject characterSelectionPanel; // Référence au CharacterSelectionPanel

    // Bouton "Play"
    public void OnClickPlay()
    {
        // Cache le menu principal
        mainMenuPanel.SetActive(false);

        // Ici, tu peux lancer le jeu, débloquer le joueur, etc.
        // ex: GameManager.Instance.StartGame();
    }

    // Bouton "Choix Personnage"
    public void OnClickCharacterSelection()
    {
        // Cache le menu principal
        mainMenuPanel.SetActive(false);
        // Affiche le panel de sélection
        characterSelectionPanel.SetActive(true);
    }

    // Bouton "Retour" ou "Confirm" depuis le panel de sélection
    // (si tu veux revenir au menu)
    public void OnClickBackToMainMenu()
    {
        characterSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
