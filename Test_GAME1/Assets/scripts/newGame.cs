using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newGame : MonoBehaviour

{
    public Button button;

    public void PlayPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
