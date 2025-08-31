using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailScreen : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;

    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Kitchen");
    }

    public void QuitButton()
    {
       Application.Quit();
    }

}
