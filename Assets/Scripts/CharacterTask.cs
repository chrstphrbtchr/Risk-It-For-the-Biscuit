using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTask : MonoBehaviour
{
    public bool currentlyBeingWorkedOn;
    public CharacterNavigation currentCharacter;
    public float taskTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginCharacterTask(CharacterNavigation theCharacter)
    {
        if (currentCharacter == null)
        {
            currentCharacter = theCharacter;
        }
        StartCoroutine("WorkOnTask");
    }
    IEnumerator WorkOnTask()
    {
        yield return null;
    }
}
