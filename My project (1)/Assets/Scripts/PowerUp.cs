using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed=3f;
    [SerializeField]
    private int ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*_speed*Time.deltaTime);
        if(transform.position.y<=-10)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            if(this.ID==1)
                other.GetComponent<Player>().powerUpTripleShot();
            else if(this.ID==2)
                other.GetComponent<Player>().powerUpSpeed();
            else if(this.ID==3)
                other.GetComponent<Player>().powerUpShield();
            Destroy(this.gameObject);
        }
    }
}
