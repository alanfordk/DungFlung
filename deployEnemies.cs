using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployEnemies : MonoBehaviour
{

    public GameObject zombiePrefab;
    public Monkey monkey;
    public int lastScore;

    private Vector2 screenBounds;
    private Vector3 _initialPosition;

    public float respawnTime = 10.0f;
    void Start()
    {
        StartCoroutine(enemyWave());
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        monkey = GameObject.FindGameObjectWithTag("Monkey").GetComponent<Monkey>();
        lastScore = 0;
    }

    void Update()
    {
        if(monkey.score == (lastScore + 10))
        {
            respawnTime = respawnTime - 0.01f;
            lastScore = monkey.score;
            Debug.Log("Level Up");
        }
        
    }

    private void spawn()
    {
        GameObject a = Instantiate(zombiePrefab) as GameObject;
        _initialPosition = a.transform.position;
        a.transform.position = new Vector2(screenBounds.x, _initialPosition.y);
    }

    IEnumerator enemyWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawn();
        }
    }
}
