using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    private GameManager _gameManager;
    [SerializeField]
    
    private Image _livesImg;
    [SerializeField]
    private Image _gameOver;

    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField]
    private Text _restartText;
    void Start()
    {
        _gameOver.enabled=false;
        _scoreText.text="Score: 0";
        _gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void updateScore(int score){
        _scoreText.text="Score: "+score.ToString();
    }
    public void updateLives(int currentLive){
        _livesImg.sprite=_liveSprite[currentLive];
    }
    public void gameOver(){
        _gameManager.GameOver();
        _gameOver.enabled=true;
        _restartText.gameObject.SetActive(true);
    }
}
