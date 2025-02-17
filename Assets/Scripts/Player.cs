using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    

    // --- Pour le score ---
    [SerializeField] private ScoreManager scoreManager;
    private int lastKnownRow = 0; // Rangée la plus avancée sur l'axe X au moment du Start

    private void Start()
    {
        animator = GetComponent<Animator>();

        // On mémorise la rangée de départ (arrondie ou “floor”).
        // Par exemple, si le joueur spawn à X=0, lastKnownRow = 0.
        lastKnownRow = Mathf.FloorToInt(transform.position.x);
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Saut avant => on détache du log avant de bouger
            DetachFromLog();
            float zDifference = 0;
            if (transform.position.z % 1 != 0)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MoveCharacter(new Vector3(1, 0, zDifference));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Saut arrière => on détache du log
            DetachFromLog();
            float zDifference = 0;
            if (transform.position.z % 1 != 0)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MoveCharacter(new Vector3(-1, 0, zDifference));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // Saut sur le côté => on NE détache pas
            MoveCharacter(new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // Saut sur l’autre côté => on NE détache pas
            MoveCharacter(new Vector3(0, 0, -1));
        }
    }

    /// <summary>
    /// Détache le joueur de son parent (log) s'il en a un.
    /// </summary>
    private void DetachFromLog()
    {
        // On se détache seulement si on est enfant d’un log
        if (transform.parent != null && transform.parent.CompareTag("Log"))
        {
            transform.SetParent(null);
        }
    }

    /// <summary>
    /// Effectue l'animation "hop" et déplace le joueur d’un certain Vector3.
    /// </summary>
    
    private void MoveCharacter(Vector3 difference)
    {
        if (difference != Vector3.zero)
        {
            // On ignore la composante Y pour éviter l’inclinaison.
            Vector3 lookDir = new Vector3(difference.x, 0f, difference.z);

            // Calcul de la rotation cible sans offset
            Quaternion baseRotation = Quaternion.LookRotation(lookDir);

            // On ajoute un offset de 90° sur l'axe Y
            Quaternion offset = Quaternion.Euler(0f, -90f, 0f);

            // Multiplication de la rotation de base par l'offset
            transform.rotation = baseRotation * offset;
        }

        transform.position += difference;

        // ... (score / rangée etc.) ...
        int newRow = Mathf.FloorToInt(transform.position.x);
        if (newRow > lastKnownRow)
        {
            scoreManager.OnNewRowReached(newRow);
            lastKnownRow = newRow;
        }
    }




    /// <summary>
    /// Méthode appelée à la fin de l’animation hop (Animation Event).
    /// Elle remet IsHoping à false pour autoriser un nouveau saut.
    /// </summary>


    /// <summary>
    /// Si on atterrit sur un Log (tag = "Log"), on devient son enfant
    /// pour se laisser transporter si le Log se déplace.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Log"))
        {
            transform.SetParent(collision.transform);
        }
    }

    /// <summary>
    /// Si on quitte le Log, on revient sans parent.
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Log"))
        {
            transform.SetParent(null);
        }
    }
}
