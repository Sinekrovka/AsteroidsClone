using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalsPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject doublePlayer;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    private void Awake()
    {
        Camera camera = Camera.main;
        float width = camera.pixelWidth;
        float height = camera.pixelHeight;
        minBounds = camera.ScreenToWorldPoint(new Vector2 (0, 0));
        maxBounds = camera.ScreenToWorldPoint(new Vector2 (width, height-50));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doublePlayer = Instantiate(player);
            doublePlayer.tag = "Shoot";
            float newX = other.transform.position.x;
            float newY = other.transform.position.y;
            if (other.transform.position.x >= maxBounds.x)
            {
                newX = minBounds.x;
            }
            else if(other.transform.position.x <= minBounds.x)
            {
                newX = maxBounds.x;
            }

            if (other.transform.position.y >= maxBounds.y)
            {
                newY = minBounds.y;
            }
            else if(other.transform.position.y <= minBounds.y)
            {
                newY = maxBounds.y;
            }
            
            other.transform.position = new Vector3(newX, newY, 0);
        }
    }

    private void Update()
    {
        Vector3 playerPos = player.transform.position;
        bool playerIntoBackground = playerPos.x > minBounds.x && playerPos.x < maxBounds.x &&
                                    playerPos.y > minBounds.y && playerPos.y < maxBounds.y;
        if (doublePlayer != null && playerIntoBackground)
        {
            Destroy(doublePlayer);
        }
    }
}
