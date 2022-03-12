using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [SerializeField]
    private bool _isCoop=false;
    private UIManager _uiMananger;
    private int _score;

    [SerializeField]
    private GameObject _rightDamage,_leftDamage;
    public float speed=3.5f;
    public float upBorder=1f;
    public float downBorder=-3.905f;
    
    public float horizontalBorder=10f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightLaser;
    [SerializeField]
    private GameObject _leftLaser;
    
    [SerializeField]
    private int _lives=3;
    public float fireRate=0.5f;
    private float _canFire=-1f;
    private bool _shield=false;


    [SerializeField]
    private float _tripleShotTime=5.0f;
    private spawnManager _spawnManager;
    private bool _tripleShot=false;
    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private AudioClip _explosion;
    private AudioSource _auSource;

    [SerializeField]
    private AudioClip _powerUpSound;
    void Start()
    {
        _auSource=GetComponent<AudioSource>(); 
        _uiMananger=GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager=GameObject.Find("SpawnManager").GetComponent<spawnManager>();
        if(_spawnManager==null)
            Debug.Log("yok");
        if(_auSource==null)
            Debug.Log("Audio source is NULL");
        else 
            _auSource.clip=_laserSound;
    } 

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        fireLaser();
    }


    void calculateMovement(){
        
        float horizontalInput=Input.GetAxis("Horizontal");
        float verticalInput=Input.GetAxis("Vertical");
        Vector3 move=new Vector3(horizontalInput,verticalInput,0);
        transform.Translate(move*speed*Time.deltaTime);

        if(transform.position.y>=upBorder)
            transform.position=new Vector3(transform.position.x,upBorder,0);
        else if(transform.position.y<=downBorder)
            transform.position=new Vector3(transform.position.x,downBorder,0);
    
        if(transform.position.x>=horizontalBorder)
            transform.position=new Vector3(-horizontalBorder,transform.position.y,0);
        else if(transform.position.x<=(-horizontalBorder))
            transform.position=new Vector3(horizontalBorder,transform.position.y,0);
    }
    void fireLaser(){
        if(Input.GetKeyDown(KeyCode.Space)&&Time.time>=_canFire){
            _canFire=Time.time+fireRate;
            if(_tripleShot){
                _auSource.Play();
                Instantiate(_rightLaser,(transform.position+new Vector3(0.4f,0.4f,0)),Quaternion.identity); 
                Instantiate(_leftLaser,(transform.position+new Vector3(-0.4f,0.4f,0)),Quaternion.identity);
            }
            Instantiate(_laser,(transform.position+new Vector3(0,1.5f,0)),Quaternion.identity);
            _auSource.Play();
        }
    }
    public void damage(){
        if(!_shield){
            if(_lives<=1){
                _uiMananger.updateLives(0);
                _uiMananger.gameOver();
                _spawnManager.onPlayerDeath();
                _auSource.clip=_explosion;
                _auSource.Play();
                Destroy(this.gameObject,0.5f);
            }
            else{
                AudioSource.PlayClipAtPoint(_explosion,transform.position);
                _lives--;
                if(_lives==2)
                    _rightDamage.SetActive(true);
                else if(_lives==1)
                    _leftDamage.SetActive(true);
                _uiMananger.updateLives(_lives);
                _auSource.clip=_laserSound;
                }
        }
        else{
            _shield=false;
            _shieldVisualizer.SetActive(false);

        }
    }
    public void powerUpTripleShot(){
        _tripleShot=true;
        StartCoroutine(TripleShotPowerDown());
    }
    public void powerUpShield(){
        AudioSource.PlayClipAtPoint(_powerUpSound,transform.position);
        _shield=true;
        _shieldVisualizer.SetActive(true);
    }

    public void powerUpSpeed(){
        speed*=2;
        StartCoroutine(speedUp());
    }
    private IEnumerator speedUp(){
        _auSource.clip=_powerUpSound;
        _auSource.Play();
        yield return new WaitForSeconds(0.5f);
        _auSource.clip=_laserSound;
        yield return new WaitForSeconds(_tripleShotTime-0.5f);
        speed/=2;
    }
    private IEnumerator TripleShotPowerDown(){
        _auSource.clip=_powerUpSound;
        _auSource.Play();
        yield return new WaitForSeconds(0.5f);
        _auSource.clip=_laserSound;
        yield return new WaitForSeconds(_tripleShotTime-0.5f);
        _tripleShot=false;
    }
    public void addScore(){
        _score++;
        _uiMananger.updateScore(_score);
    }
}