using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterScript : MonoBehaviour
{
    private Vector3 syncPos;
    public float damping = 10;
    public float speed = 1;

    Rigidbody RB;

    // Use this for initialization
    void Start () {
        RB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //turn to closest killer
        var lookPos = GetClosestPlayer(getPlayers(), this.transform).position - transform.position;
        lookPos.y = 0;
        transform.LookAt(lookPos);

        //move forward
        transform.Translate(Vector3.forward *speed * Time.deltaTime);
        //roll forward
        /* RB.AddForce(new Vector3(5,0,0) * speed);
         this.position += myTransform.forward * moveSpeed * Time.deltaTime;
         */
    }

    List<Transform> getPlayers()
    {
        List<Transform> playerPositions = new List<Transform>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            playerPositions.Add(player.transform);
        }
        return playerPositions;
    }

    Transform GetClosestPlayer(List<Transform> Player, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in Player)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        print(bestTarget);
        return bestTarget;
    }
}
