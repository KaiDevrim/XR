using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    
    public TMP_InputField NameField;
    public Button JoinGame;
    public string theScene = "MainScene";
    public Toggle boolGraduate;
    public TMP_Dropdown genderSelect;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void changeTheScene()
    {
        if (NameField.text != "") 
        {
            SceneManager.LoadScene(theScene);
        }
        
    }

    public void isOn(bool value)
    {
        if (value == true)
        {
            Debug.Log(value);
        }
        if (value == false)
        {
            Debug.Log(value);
        }
    }

    public void gender()
    {
        if(genderSelect.value == 1)
        {
            Debug.Log(genderSelect.value);
        }
        if (genderSelect.value == 0)
        {
            Debug.Log(genderSelect.value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        string name = NameField.text;
    }
}
