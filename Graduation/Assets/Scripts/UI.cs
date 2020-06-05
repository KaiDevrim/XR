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
        public static string uiID;


        public void getText()
        {
            uiID = IDField.text;
            Debug.Log(uiID);
        }

        public void changeScene()
        {

        }
        
    }
}
