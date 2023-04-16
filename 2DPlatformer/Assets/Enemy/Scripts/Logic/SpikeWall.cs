using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{
    public float movingSpeed = 4.5f;
    public float maxDistanceFromPlayer = 5f;
    public float waitTime = 3f;
    private bool waitSpikes = false;
    private bool stopSpikes = false;

    void Update()
    {
        if (PlayerController.Instance.Health <= 0)
            return;

        if (waitSpikes == true)
        {
            StartCoroutine(waiter());
            return;
        }

        if (stopSpikes == true)
        {
            return;
        }


        float distance = Mathf.Abs(PlayerController.Instance.transform.position.x - this.transform.position.x);

        if (distance > maxDistanceFromPlayer)
        {
            Vector3 newPosition = new Vector3(PlayerController.Instance.transform.position.x - maxDistanceFromPlayer, this.transform.position.y, this.transform.position.z);
            this.transform.position = newPosition;
        }

        transform.Translate(movingSpeed * Time.deltaTime, 0, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WaitSpikes"))
        {
            waitSpikes = true;
        }
        if (collision.CompareTag("StopSpikes"))
        {
            stopSpikes = true;
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(waitTime);
        waitSpikes = false;
    }

}
