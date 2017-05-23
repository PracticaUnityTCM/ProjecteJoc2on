using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum GameState
{
    IsPlaying,
    IsPause,
    IsGameOver
}
public class GameUI : MonoBehaviour {
   public GameState gameState;
    public GameObject PauseUI;
    public GameObject PlayingUI;
    public Image fadePlane;
    public GameObject GameOverUI;
    public bool enablePause = true;
    int i;
	// Use this for initialization
	void Start () {
        gameState = GameState.IsPlaying;
        PauseUI.SetActive(false);
        GameOverUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (enablePause) {             // if Key P is Pressed and StateGame var is IsPause
            if (Input.GetKeyDown(KeyCode.P) && gameState == GameState.IsPause)
            {
                Resume();
                Debug.Log("resume");
            }
            else if (Input.GetKeyDown(KeyCode.P) && gameState == GameState.IsPlaying)
            {
                Debug.Log("Pause");
                Pause();
            }
        }
        if(gameState==GameState.IsPlaying)
        {
            GameOverUI.SetActive(false);
            PauseUI.SetActive(false);
            PlayingUI.SetActive(true);

        }
        else if (gameState == GameState.IsPause)
        {
            GameOverUI.SetActive(false);
            PlayingUI.SetActive(false);
            PauseUI.SetActive(true);
           // fadePlane.gameObject.SetActive(true);
        }
        else if(gameState==GameState.IsGameOver)
        {
            GameOverUI.SetActive(true);
            PlayingUI.SetActive(false);
            PauseUI.SetActive(false);
        }
	}
    public void Resume()
    {



        gameState = GameState.IsPlaying;
        
        StartCoroutine(Fade(new Color(0, 0, 0, .85f),Color.clear,1));
         Time.timeScale = 1f;
      
       
    }
    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
        } 
    void Pause()
    {  
        gameState = GameState.IsPause;
        StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, .85f), 1));
    }
    public void GameOver()
    {
        GameOverUI.SetActive(true);
        gameState = GameState.IsGameOver;
    }
    IEnumerator Fade(Color from, Color to, float time)
    {
        
        float speed = 1 / time;
        float percent = 0;
        while (percent < 1)
        {
            enablePause = false;
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
   
        }
        enablePause = true;
        if (gameState==GameState.IsPause)
        {
            Time.timeScale = 0f;
        }           
    }

}
