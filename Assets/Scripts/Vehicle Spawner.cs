using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeperationTime;
    [SerializeField] private float maxSeperationTime;
    [SerializeField] private bool iscar;
    [SerializeField] private bool Isrightside;

    // Nouveau champ : permet de définir la rotation Y
    // Par ex. -90 pour spawner à droite, +90 pour spawner à gauche
    [SerializeField] private float spawnRotationY ;

    private void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            if (iscar)
            {
                yield return new WaitForSeconds(Random.Range(minSeperationTime, maxSeperationTime));
                Instantiate(
                    vehicle,
                    spawnPos.position,
                    Quaternion.Euler(0f, spawnRotationY, 0f)
                );
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(minSeperationTime, maxSeperationTime));
                GameObject go =Instantiate(
                    vehicle,
                    spawnPos.position,
                    Quaternion.identity);
                if(Isrightside)
                {
                    go.transform.Rotate(new Vector3(0, 180, 0));
                }
                
            }
        }
    }
}

