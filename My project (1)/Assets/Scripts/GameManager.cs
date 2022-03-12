using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver=false;

    private void Update(){
        
        if(Input.GetKeyDown(KeyCode.R)&&_isGameOver){
            SceneManager.LoadScene(1);
        }
        else if(Input.GetKeyDown(KeyCode.M)&&_isGameOver){
            SceneManager.LoadScene(0);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    public void GameOver(){
        _isGameOver=true;
    }
}
