using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour
{
    NavMeshAgent myAgent;
    public Transform[] places;
    int placeIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.SetDestination(places[placeIndex].position);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, myAgent.destination) < 0.1f)
        {
            placeIndex++;
            placeIndex = placeIndex % places.Length;
            myAgent.SetDestination(places[placeIndex].position);
        }
    }
}
