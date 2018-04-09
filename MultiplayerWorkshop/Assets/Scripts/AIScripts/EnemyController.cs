using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    //EnemyStats
    public int detectionRange;

    public int movementSpeed;

    public NavMeshAgent navAgent;
    protected GameObject[] pointList;

    private GameObject[] players;
    public GameObject player;
    private EnemyStateMachine SM;

    protected virtual void FSMStart() { }
    protected virtual void FSMUpdate() { }

    //private 
    //FindObjectsOfType<Agent>()

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = movementSpeed;
        
        FSMStart();
	}

    // Update is called once per frame
    void Update() {
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            player = players[0];
            for (int i = 1; i < players.Length; i++)
            {
                if (Vector3.Distance(players[i].transform.position, this.transform.position) < Vector3.Distance(player.transform.position, this.transform.position))
                {
                    player = players[i];
                }
            }
        }
        FSMUpdate();
    }
}