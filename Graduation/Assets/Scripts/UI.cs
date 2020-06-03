using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using GraduationVR;

namespace GraduationVR
{
    public class UI : MonoBehaviour
    {

        public TMP_InputField IDField;
        public Button JoinGame;
        public string theScene = "MainScene";

        // Start is called before the first frame update
        void Start()
        {
        }

        public void changeTheScene()
        {
            if (IDField.text != "")
            {
                SceneManager.LoadScene(theScene);
            }

        }

        // Update is called once per frame
        void Update()
        {
            //string id = IDField.text;
           
           //IDField.text = task.Result.ToString();
        }
    }
}
