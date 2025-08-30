using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReturnZone : MonoBehaviour
{
    public MouseLaunch Player;
    public int haulsReturned = 0;
    public float weightOfHauls = 0;
    public TMP_Text weightGathered, acquiredText;
    public Image weightIcon;
    public GameObject acquiredSquare;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && MouseLaunch.haul != null)
        {
            haulsReturned++;
            weightOfHauls += MouseLaunch.haul.rb.mass;
            weightGathered.text = $"{weightOfHauls} KG Gathered";
            acquiredText.text = $"{MouseLaunch.haul.displayName} Gathered";
            MouseLaunch.haul.gameObject.SetActive(false);
            MouseLaunch.haul = null;
            StartCoroutine(flashWeight());
        }
    }

    IEnumerator flashWeight()
    {

        acquiredSquare.SetActive(true);
        Color orig = weightIcon.color;
        weightIcon.color = Color.white;
        weightGathered.color = Color.black;
        yield return new WaitForSeconds(1f);
        weightIcon.color = orig;
        weightGathered.color = Color.white;
        acquiredSquare.SetActive(false);
    }


}
