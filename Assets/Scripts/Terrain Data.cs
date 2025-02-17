using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Data",menuName = "Terrain Data")]
public class TerrainData : ScriptableObject
{
    public GameObject terrain;
    public int maxInSuccession;

    public bool canSpawnTrees = false;

    // Plusieurs prefabs d'arbres possibles
    public List<GameObject> treePrefabs;

    // Nombre maximum d'arbres à instancier par ligne
    public int maxTreesPerTile = 5;       // Nombre max d’arbres par tuile
}
