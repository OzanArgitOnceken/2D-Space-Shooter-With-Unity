using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed=10f;
    private bool isEnemyLaser=false;
    // Update is called once per frame
    void Update()
    {
        move();
    }
    public void move(){
        int vertical;
        if(isEnemyLaser)
            vertical=-1;
        else
            vertical=1;
        Vector3 move=new Vector3(0,vertical,0);
        transform.Translate(move*Time.deltaTime*speed);
        if(transform.position.y>=8f||transform.position.y<=-10f){
            Destroy(this.gameObject);
        }
    }

    public void assignEnemy(){
        isEnemyLaser=true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            other.GetComponent<Player>().damage();
            Destroy(this.gameObject);
        }
    }


}
