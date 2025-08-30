using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        if(currentCharacter == null)
        {
            Debug.LogWarning("<color=#248017>Who's working on this?!</color>");
        }
        // TODO: Make sure Distractables can shut off WorkOnTask
        currentCharacter.ChangeState(CharacterNavigation.NPC_State.Working);
        while (Mathf.Abs(Vector3.Angle(currentCharacter.transform.forward.normalized, this.transform.forward.normalized)) > 0.1f)
        {
            currentCharacter.transform.rotation = Quaternion.RotateTowards(currentCharacter.transform.rotation, this.transform.rotation, 0.5f);
            //print(Mathf.Abs(Vector3.Angle(currentCharacter.transform.forward.normalized, this.transform.forward.normalized));
            yield return null;
        }
        yield return new WaitForSeconds(taskTime);
        StartCoroutine(currentCharacter.GoToNextPlace(false));
        currentCharacter = null;
        yield return null;
    }
}
