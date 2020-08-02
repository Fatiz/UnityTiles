using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverScript : MonoBehaviour
{
    // Start is called before the first frame update

    public TMPro.TextMeshProUGUI textgui;
    void Start()
    {
      
        textgui.text = "Score:"+ GameplayContext.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}