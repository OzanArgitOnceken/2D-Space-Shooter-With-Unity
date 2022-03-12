using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class leftLaser : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed=10f;
    // Update is called once per frame
    void Update()
    {
        move();
        
    }


    public void move(){
        Vector3 move=new Vector3(-1,1,0);
        transform.Translate(move*Time.deltaTime*speed);
        if(transform.position.y>=8f){
            Destroy(this.gameObject);
        }
    }


}
