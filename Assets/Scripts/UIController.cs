using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
  public TMP_Text healthText;
  public TMP_Text gameOverText;

  public void Start()
  {
    gameOverText.enabled = false;
  }

  public void onPlayerHealthUpdated(int health)
  {
    if (health > 0)
    {
      UpdateHealth(health);
    }
    else
    {
      HideHealth();
      ShowGameOver();
    }
  }

  public void ShowGameOver()
  {
    gameOverText.enabled = true;
  }

  public void UpdateHealth(int health)
  {
    healthText.text = "Health: " + health;
  }

  public void HideHealth()
  {
    healthText.text = "";
  }
}
