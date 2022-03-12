using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _powerUps;
    private bool _spawn=true;
    [SerializeField]
    private GameObject _prefabEnemy;

    [SerializeField]
    private float freq=5f;
    
    private float freq2;

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _asteroid;


    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(spawnTripplePU());
        StartCoroutine(SpawnAsteroid());
    }

    void Update()
    {/*
    

        if(Time.time>=freq2&&_spawn){
            freq2+=freq;
            Vector3 pos=new Vector3(Random.Range(-10f,10f),10,0);
            GameObject newObj= Instantiate(_prefabEnemy,pos,Quaternion.identity);
            newObj.transform.parent=_enemyContainer.transform;
        }

    */
    }
    private IEnumerator SpawnRoutine(){
        while(_spawn){
            Vector3 pos=new Vector3(Random.Range(-10f,10f),10,0);
            GameObject newObj= Instantiate(_prefabEnemy,pos,Quaternion.identity);
            newObj.transform.parent=_enemyContainer.transform;
            yield return new WaitForSeconds(freq);
        }
    }    
    private IEnumerator SpawnAsteroid(){
        while(_spawn){
            Vector3 pos=new Vector3(Random.Range(-10f,10f),10,0);
            GameObject newObj= Instantiate(_asteroid,pos,Quaternion.identity);
            newObj.transform.parent=_enemyContainer.transform;
            yield return new WaitForSeconds(freq*3);
        }
    }
    private IEnumerator spawnTripplePU(){
        while(_spawn){
            Vector3 postToSpan=new Vector3(Random.Range(-9f,9f),7,0);
            int random=Random.Range(0,3);

            Instantiate(_powerUps[random],postToSpan,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5,13)); 
        }
    }
    public void onPlayerDeath(){
        _spawn=false;
    }
}