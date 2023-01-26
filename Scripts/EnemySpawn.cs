using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject EnemyPrefab;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;

    // Use this for initialization
    void Start () {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(Wave());
    }
    private void spawnEnemy(){
        GameObject a = Instantiate(EnemyPrefab) as GameObject;
        switch(Random.Range(0,5)){
            case 0: //top
                a.transform.position = new Vector2(Random.Range(screenBounds.x + 5, -screenBounds.x - 5), screenBounds.y * 1.5f);
                break;
            case 1: //bottom
                a.transform.position = new Vector2(Random.Range(screenBounds.x + 5, -screenBounds.x - 5), -screenBounds.y * 1.5f);
                break;
            case 2: //left
                a.transform.position = new Vector2(-screenBounds.x * 1.5f, Random.Range(-screenBounds.y - 5, screenBounds.y + 5));
                break;
            default: //right
                a.transform.position = new Vector2(screenBounds.x * 1.5f, Random.Range(-screenBounds.y - 5, screenBounds.y + 5));
                break;
        }

    }
    IEnumerator Wave(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}