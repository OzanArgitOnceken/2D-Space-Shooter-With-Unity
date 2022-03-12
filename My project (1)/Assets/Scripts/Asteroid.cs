using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed=2f;

    private Animator _anim;
    [SerializeField]
    private float _fallSpeed=1.5f;
    [SerializeField]
    private float _deathSpeed=0.2f;
    void Start()
    {
        _anim=GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.down*_fallSpeed*Time.deltaTime);
        transform.Rotate(Vector3.forward*_rotationSpeed*Time.deltaTime);
        if(transform.position.y<=-5f){
            float rand=Random.Range(-10f,10f);
            transform.position=new Vector3(rand,7,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Laser"){
            Destroy(other.gameObject);
            _anim.SetTrigger("OnTrigger");
            _fallSpeed=_deathSpeed;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2f);
        }
        else if(other.tag=="Player"){
            _anim.SetTrigger("OnTrigger");
            _fallSpeed=_deathSpeed;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.5f);
            other.GetComponent<Player>().damage();
        }
        
    }
}
