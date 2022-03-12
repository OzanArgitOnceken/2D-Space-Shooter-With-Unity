using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed=4f;
    private Player _player;

    [SerializeField]
    private float _deathSpeed=0.5f;

    private Animator _anim;

    [SerializeField]
    private AudioClip _explosionSound;
    [SerializeField]
    private float _fireRate=4f;
    private float _fireTime=5f;
    private AudioSource _auSource;

    [SerializeField]
    private GameObject _enemyLaser;
    
    void Start()
    {
        _auSource=GetComponent<AudioSource>();
        _player=GameObject.Find("Player").GetComponent<Player>();
        if(_player==null){
            Debug.LogError("Player is null");
        }
        _anim=GetComponent<Animator>();
        if(_anim==null){
            Debug.LogError("The Animator is null");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down*speed*Time.deltaTime);
        if(transform.position.y<=-5f){
            float rand=Random.Range(-10f,10f);
            transform.position=new Vector3(rand,7,0);
        }
        this.shoot();
        
        
    }
    private void shoot(){
        if(Time.time>=_fireTime){
            _fireTime=Time.time+_fireRate;
            GameObject eL= Instantiate(_enemyLaser,(transform.position+new Vector3(0,-2,0)),Quaternion.identity);
            Laser tempL=eL.GetComponent<Laser>();
            tempL.assignEnemy();
            }
}
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Laser"){
            _auSource.clip=_explosionSound;
            _auSource.Play();
            Destroy(other.gameObject);
            _player.addScore();
            _anim.SetTrigger("OnEnemyDeath");
            speed=_deathSpeed;
            Destroy(GetComponent<Collider2D>(),0.2f);
            Destroy(this.gameObject,2.5f);
        }
        else if(other.tag=="Player"){
            _anim.SetTrigger("OnEnemyDeath");
            speed=_deathSpeed;
            Destroy(GetComponent<Collider2D>(),0.2f);
            Destroy(this.gameObject,2.5f);
            other.GetComponent<Player>().damage();
        }
    }
}