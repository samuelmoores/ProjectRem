using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class KnifeSpawner : MonoBehaviour
{
    public GameObject KnifePrefab;
    GameObject SpawnedKnife;

    public void Spawn()
    {
        SpawnedKnife = GameObject.Instantiate(KnifePrefab, transform);
    }
}
