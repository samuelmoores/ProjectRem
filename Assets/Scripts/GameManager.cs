using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] KnifeSpawnLocations;
    public GameObject KnifePrefab;
    GameObject SpawnedKnife;
    int index;
    GameObject player;
    PlayerHealth playerHealth;
    Tom tom;
    float gameResetTimer = 4.0f;
    float knifeSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        knifeSpawnTimer = 1.0f;
        tom = GameObject.Find("Tom").GetComponent<Tom>();
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

            if(index < 6)
            {
                knifeSpawnTimer = Random.Range(0.25f, 0.45f);
            }
            else
            {
                knifeSpawnTimer = Random.Range(0.25f, 1.0f);

            }
        }
        else if(knifeSpawnTimer <= 0 && tom.Alive())
        {
            Vector3 SpawnLocation = player.transform.position + new Vector3(0.0f, 100.0f, 0.0f);
            Quaternion spawnRotation = player.transform.rotation;

            SpawnedKnife = GameObject.Instantiate(KnifePrefab, SpawnLocation, spawnRotation);

            knifeSpawnTimer = 3.0f;


        }


        if (!playerHealth.Alive())
        {
            gameResetTimer -= Time.deltaTime;
        }

        if(gameResetTimer <= 0.0f)
        {
            SceneManager.LoadScene(1);
        }


    }
}
