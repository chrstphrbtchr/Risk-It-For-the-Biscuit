using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public introcube[] cubes;
    public float cubeMoveTime;
    public float delayBetweenCubes;
    public float cubeDistance;
    public GameObject startButton, creditsScreen;
    public bool creditsOn;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var cube in cubes) { cube.distance = cubeDistance; }
        StartCoroutine(cubeMoving());
    }

    IEnumerator  cubeMoving()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
           
            cubes[i].turnOn(cubeMoveTime);
            yield return new WaitForSeconds(delayBetweenCubes);
        }
      //  startButton.SetActive(true);
    }

    public void LoadKitchen()
    {
        SceneManager.LoadScene("Kitchen");
    }

    public void CreditsButton()
    {
        if (creditsScreen.activeSelf == false) creditsScreen.SetActive(true);
        else creditsScreen.SetActive(false);
    }
}
