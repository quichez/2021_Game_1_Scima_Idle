using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;
    public TextMeshProUGUI enemyName;
    public Image healthBar;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Subscribe(Enemy enemy)
    {
        enemy.OnEnemyHealthUpdated += UpdateHealthBar;
    }   

    public void UpdateHealthBar()
    {
        enemyName.text = Stage.Instance.CurrentEnemy.name;
        healthBar.fillAmount = Stage.Instance.CurrentEnemy.HealthPercentage;
        healthText.text = Stage.Instance.CurrentEnemy.CurrentHealth.Rounded.ToString() + " / " + Stage.Instance.CurrentEnemy.Health.ToString();
    }
}
