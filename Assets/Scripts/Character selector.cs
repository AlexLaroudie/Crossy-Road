using UnityEngine;
using UnityEngine.UI; // si tu utilises les boutons legacy, sinon TextMeshPro
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    [Header("Liste de prefabs de personnages")]
    [SerializeField] private GameObject[] characterPrefabs;

    [Header("Parent (vide) où on instancie le personnage")]
    [SerializeField] private Transform playerParent;

    [Header("UI Boutons")]
    [SerializeField] private Button buttonPrevious;
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonConfirm;

    // Optionnel : si tu veux afficher le nom du personnage
    [SerializeField] private TMP_Text characterNameText;

    private GameObject currentCharacter;
    private int currentIndex = 0;

    private void Awake()
    {
        // Connecter les boutons aux méthodes
        buttonPrevious.onClick.AddListener(OnClickPreviousCharacter);
        buttonNext.onClick.AddListener(OnClickNextCharacter);
        buttonConfirm.onClick.AddListener(OnClickConfirmSelection);

        UpdateCharacter(); // instancie le perso initial
    }

    private void OnClickPreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + characterPrefabs.Length) % characterPrefabs.Length;
        UpdateCharacter();
    }

    private void OnClickNextCharacter()
    {
        currentIndex = (currentIndex + 1) % characterPrefabs.Length;
        UpdateCharacter();
    }

    private void OnClickConfirmSelection()
    {
        // Optionnel : stocker l'index choisi, etc.
        // Revenir au menu principal ou lancer direct la partie.
        Debug.Log("Personnage index " + currentIndex + " confirmé.");

        // Si tu veux revenir au menu : 
        // 1) On appelle "BackToMainMenu()" du MainMenuUI
        //    => Il faut une référence vers MainMenuUI, ou un manager commun.
        // 2) Ou, plus simple, tu peux direct faire :
        //    gameObject.SetActive(false); // cache ce panel
        //    // Rendre le MainMenuPanel actif à nouveau.
    }

    private void UpdateCharacter()
    {
        // Detruit l'ancien perso
        if (currentCharacter != null)
            Destroy(currentCharacter);

        // Instancie le nouveau
        if (characterPrefabs.Length > 0)
        {
            var prefab = characterPrefabs[currentIndex];
            currentCharacter = Instantiate(prefab, playerParent);

            // Optionnel : afficher le nom du prefab
            if (characterNameText != null)
                characterNameText.text = prefab.name;
        }
    }
}
