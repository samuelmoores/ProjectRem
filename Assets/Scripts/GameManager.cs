using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float knifeSpawnTime;
    public Transform[] KnifeSpawnLocations;
    public GameObject KnifePrefab;
    GameObject SpawnedKnife;
    int index;
    GameObject player;
    PlayerHealth playerHealth;
    float gameResetTimer = 4.0f;
    float knifeSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        knifeSpawnTimer -= Time.deltaTime;

        if(knifeSpawnTimer <= 0 && index < KnifeSpawnLocations.Length)
        {
            Transform location = KnifeSpawnLocations[index];
            location.position += new Vector3(0.0f, 20.0f, 0.0f);
            SpawnedKnife = GameObject.Instantiate(KnifePrefab, KnifeSpawnLocations[index++]);
            knifeSpawnTimer = knifeSpawnTime;
        }


        if(!playerHealth.Alive())
        {
            gameResetTimer -= Time.deltaTime;
        }

        if(gameResetTimer <= 0.0f)
        {
            SceneManager.LoadScene(1);
        }


    }
}
