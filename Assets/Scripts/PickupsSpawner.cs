using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pickups;

    void Start()
    {
        StartCoroutine(PickupsSpawn());
    }

    IEnumerator PickupsSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15f, 30f));
            Instantiate(pickups[Random.Range(0, pickups.Length)], new Vector2(Random.Range(-7, 5), Random.Range(-4, 3)), Quaternion.identity);
        }

    }
}
