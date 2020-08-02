using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    
    // Start is called before the first frame update
    public string scenename;
    void Start()
    {
        
    }

    public void LoadScene(){
        SceneManager.LoadScene(scenename);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
