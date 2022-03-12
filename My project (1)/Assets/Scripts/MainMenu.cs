using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void loadSinglePlayer(){
        SceneManager.LoadScene(1);
    }public void loadCoOp(){
        SceneManager.LoadScene(2);
    }
}
