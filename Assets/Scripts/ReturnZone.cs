using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReturnZone : MonoBehaviour
{
    public MouseLaunch Player;
    public int haulsReturned = 0;
    public float weightOfHauls = 0;
    public TMP_Text weightGathered;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            haulsReturned++;
            weightOfHauls += MouseLaunch.haul.rb.mass;
            weightGathered.text = $"{weightOfHauls}KG Gathered";
            MouseLaunch.haul.gameObject.SetActive(false);
            MouseLaunch.haul = null;
        }
    }
}
