using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public bool gameHasEnded = false;

    public float restartDelay = 1f;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public Text scoreTextWin;
    public Text timeTextWin;
    public Text scoreTextLose;
    public Text timeTextLose;

    private int enemyCount;

    float scoreValue = 0;
    float timeValue = 0;

    private void Start(){
        enemyCount = CaculateEnemyCount();
    }

    private void Update(){
        if (CaculateEnemyCount() == 0){
            gameHasEnded = true;
            ShowWinPanel();
        }
    }

    private void ShowWinPanel(){
        scoreValue = ScoreScript.instance.scoreTotal;
        timeValue = StopWatch.Instance.timeStart;
        gameOverPanel.SetActive(false);

        winPanel.SetActive(true);
        scoreTextWin.text = "SCORE\n" + scoreValue.ToString("F0") + "\n(Points)" ;
        timeTextWin.text = "TIME\n" + timeValue.ToString("F0") + "\n(Secs)";
    }

    private void ShowGameOverPanel(){
        scoreValue = ScoreScript.instance.scoreTotal;
        timeValue = StopWatch.Instance.timeStart;
        winPanel.SetActive(false);

        gameOverPanel.SetActive(true);
        scoreTextLose.text = "SCORE\n" + scoreValue.ToString("F0") + "\n(Points)" ;
        timeTextLose.text = "TIME\n" + timeValue.ToString("F0") + "\n(Secs)";
    }

    private int CaculateEnemyCount(){
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private int CaculatePlayerCount(){
        return GameObject.FindGameObjectsWithTag("Player").Length;
    }

    public void GameOver(){
        if (gameHasEnded == false){
            gameHasEnded = true;
            ShowGameOverPanel();
        }
    } 

    public void RestartOption(){
        Invoke("ClearTemporaryObjects",5f);
        Invoke("Restart", restartDelay);
    }

    private void ClearTemporaryObjects(){
        GameObject[] temporaryObjects = GameObject.FindGameObjectsWithTag("Item");

        foreach(var tempObject in temporaryObjects){
            Destroy(tempObject);
        }
    }

    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
