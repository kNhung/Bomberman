using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;

    int scoreEnemy = 0;
    float scoreTime = 0;
    float maxTimeToScore = 300f;
    public float scoreTotal = 0;
    public Text scoreText;


    private void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        scoreText.text = "SCORE\n" + scoreTotal.ToString() + "\n(Points)" ;
    }

    public void AddScore(){
        scoreEnemy += 10;
        scoreTime = Mathf.Clamp((maxTimeToScore-StopWatch.Instance.timeStart)/maxTimeToScore, 0f, 1f)*maxTimeToScore/10;
        scoreTotal = scoreEnemy + scoreTime;
        scoreText.text = "SCORE\n" + scoreTotal.ToString("F0") + "\n(Points)" ;
    }
}
