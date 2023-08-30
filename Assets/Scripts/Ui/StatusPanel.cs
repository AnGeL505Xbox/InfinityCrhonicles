using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    public Text name_label;

    public Slider health_slider;
    public Image health_slider_bar;
    public Text health_label;

    public void SetStats(string name, Stats stats)
    {
        this.name_label.text = name;

        this.SetHealth(stats.health, stats.maxHealth);
    }

    public void SetHealth(float health, float maxHealth)
    {
        this.health_label.text = $"{Mathf.RoundToInt(health)} / {Mathf.RoundToInt(maxHealth)}";
        float percentage = health / maxHealth;

        this.health_slider.value = percentage;

        if (percentage <= 0.5f && percentage > 0.33)
        {
            this.health_slider_bar.color = Color.yellow;
        }
        else if (percentage <= 0.33f)
        {
            this.health_slider_bar.color = Color.red;
        }
    }
}
