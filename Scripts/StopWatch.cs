using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    public static StopWatch Instance;

    public float timeStart;
    public Text textBox;

    private void Awake(){
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        textBox.text = "TIME\n" + timeStart.ToString("F0") + "\n(Secs)";
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length != 0){
            timeStart += Time.deltaTime;
            textBox.text = "TIME\n" + timeStart.ToString("F0") + "\n(Secs)" ;
        }
    }
}
