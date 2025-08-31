using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailScreen : MonoBehaviour
{
    public AudioClip losingSound;
    public AudioSource source;
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        source.PlayOneShot(losingSound);
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
