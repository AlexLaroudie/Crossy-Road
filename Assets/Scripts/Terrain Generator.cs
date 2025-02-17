using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private Vector3 currentPosition = new Vector3(0, 0, 0);
    [SerializeField] private List<TerrainData> terrainDatas = new List<TerrainData>();
    [SerializeField] private int maxstartamount;
    private List<GameObject> currentTerrainscontains = new List<GameObject>();
    [SerializeField] private int maxterrain;
    

    private void Start()
    {
        /*for (int i = 0; i < 9; i++)
        {
            Instantiate(terrainDatas[0], currentPosition, Quaternion.identity);
            currentPosition.x++;
        }*/
        for (int i = 0; i < 5; i++)
        {
            SpawnSpecificTerrain(terrainDatas[0], true);  // herbe
        }

        // 2) Générer le reste de façon aléatoire
        for (int i = 0; i < maxstartamount - 5; i++)
        {
            SpawnTerrain(true);
        }


    }
    private void SpawnSpecificTerrain(TerrainData data, bool isStart)
    {
        // On fait la même chose que SpawnTerrain,
        // sauf qu'on n'utilise pas Random.Range pour choisir le terrain.
        // On met "terrainInSuccession = 1" pour ne créer qu'une seule ligne.
        int terrainInSuccession = 1;

        for (int i = 0; i < terrainInSuccession; i++)
        {
            GameObject terrainObj = Instantiate(data.terrain, currentPosition, Quaternion.identity);
            currentTerrainscontains.Add(terrainObj);

            if (!isStart)
            {
                if (currentTerrainscontains.Count > maxterrain)
                {
                    Destroy(currentTerrainscontains[0]);
                    currentTerrainscontains.RemoveAt(0);
                }
            }
            // --- Si on peut y mettre des arbres ---
            if (data.canSpawnTrees)
            {
                SpawnTreesOnTerrain(terrainObj, data);
            }

            currentPosition.x++;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            SpawnTerrain(false);
        }
        
    }
    private void SpawnTerrain(bool IsStart)
    {
        int whichTerrain = Random.Range(0, terrainDatas.Count);
        TerrainData data = terrainDatas[whichTerrain];
        int terrainInSuccession = Random.Range(1, data.maxInSuccession);

        for(int i =0;i<terrainInSuccession;i++)
        {
            GameObject terrainObj = Instantiate(data.terrain, currentPosition, Quaternion.identity);
            currentTerrainscontains.Add(terrainObj);

            if (!IsStart)
            {
                if (currentTerrainscontains.Count > maxterrain)
                {
                    Destroy(currentTerrainscontains[0]);
                    currentTerrainscontains.RemoveAt(0);
                }
            }
            // --- SI ce terrain peut accueillir des arbres, on en place quelques-uns ---
            if (data.canSpawnTrees)
            {
                SpawnTreesOnTerrain(terrainObj, data);
            }

            currentPosition.x++;

        }
        




        
    }
    private void SpawnTreesOnTerrain(GameObject terrainObj, TerrainData data)
    {
        // Combien d'arbres va-t-on placer sur cette ligne
        int numberOfTrees = Random.Range(0, data.maxTreesPerTile + 1);

        for (int i = 0; i < numberOfTrees; i++)
        {
            // Position de base de la tuile
            Vector3 terrainPos = terrainObj.transform.position;

            // Comme la tuile fait 25 en Z,
            // on peut placer un arbre n'importe où entre Z=0 et Z=25
            float offsetZ = Random.Range(-12f, 12f);

            // En X, la tuile ne fait qu'1 m de large,
            // on prend une position aléatoire entre 0 et 1
            float offsetX = Random.Range(0f, 1f);

            // Pour Y, on suppose que tout est à 0 (sol) 
            // (adaptez si vous avez un sol plus haut ou plus bas)
            float posY = terrainPos.y+0.5f;

            // Calcul de la position finale
            Vector3 spawnPos = new Vector3(
                terrainPos.x + offsetX,
                posY,
                terrainPos.z + offsetZ
            );

            // Vérifier qu'on a bien au moins 1 prefab d'arbre dans la liste
            if (data.treePrefabs != null && data.treePrefabs.Count > 0)
            {
                // Choisir aléatoirement un arbre parmi les 2 (ou +) possibles
                int randomTreeIndex = Random.Range(0, data.treePrefabs.Count);
                GameObject selectedTreePrefab = data.treePrefabs[randomTreeIndex];

                // Instancier l'arbre en enfant du terrain (pour le détruire avec)
                Instantiate(selectedTreePrefab, spawnPos, Quaternion.identity, terrainObj.transform);
            }
        }
    }


}