using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCommands : MonoBehaviour
{

    private GameObject playerObject;
    private GameObject DeadlySpikesWall;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        DeadlySpikesWall = GameObject.FindGameObjectWithTag("DeadlySpikesWall");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }

            GameObject[] musicNotes = GameObject.FindGameObjectsWithTag("Note");
            foreach (GameObject musicNote in musicNotes)
            {
                Destroy(musicNote);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (playerObject.CompareTag("Player"))
            {
                playerObject.tag = "Untagged";
            }
            else
            {
                playerObject.tag = "Player";
            }
        }

        if (DeadlySpikesWall != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (DeadlySpikesWall.active == true)
                {
                    DeadlySpikesWall.active = false;
                }
                else
                {
                    DeadlySpikesWall.active = true;
                }
            }
        }
    }
}
