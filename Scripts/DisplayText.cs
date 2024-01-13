using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public Text objectText;
    public InputField display;

    // Start is called before the first frame update
    void Start(){
        objectText.text = PlayerPrefs.GetString("user_name");
    }

    public void Create(){
        objectText.text = display.text;
        PlayerPrefs.SetString("user_name", objectText.text);
        PlayerPrefs.Save();
    }
}
