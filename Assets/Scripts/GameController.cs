using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject turrets;
    public GameObject player;
    public AudioSource gameOverSound;
    
    public void onPlayerHealthUpdated(int health) 
    {
        if(health<=0) {
            Destroy(turrets);
            Destroy(player);
            gameOverSound.Play();
        }
    } 
}
