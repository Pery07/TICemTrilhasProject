using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContactHandler : MonoBehaviour
{
    public Image itemImage;
    public PlayerAudioController audioController;
    bool canWinLevel = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit - Defeat");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log("Item Received");
            Destroy(collision.gameObject);
            itemImage.color = Color.white;
            canWinLevel = true;
            audioController.PlayGetItem();
        }

        if (collision.gameObject.CompareTag("FinalPoint"))
        {
            if (canWinLevel)
            {
                Debug.Log("canWin");
            }
            else
            {
                Debug.Log("cantWin");
            }
        }
    }

}
