﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;

    public bool destroyOnDeath;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    public NetworkStartPosition[] spawnPoints;

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;

                // called on the Server, will be invoked on the Clients
                RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        if (healthBar != null)
        {
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Set the player’s position to origin
            transform.position = Vector3.zero;

            if (isLocalPlayer)
            {
                // Set the spawn point to origin as a default value
                Vector3 spawnPoint = Vector3.zero;

                // If there is a spawn point array and the array is not empty, pick one at random
                if (spawnPoints != null && spawnPoints.Length > 0)
                {
                    spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                }

                // Set the player’s position to the chosen spawn point
                transform.position = spawnPoint;
            }
        }
    }
}