using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DisplayCanvas : NetworkBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
