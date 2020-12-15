using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;
	private Player player;

	private void Start()
	{
		player = FindObjectOfType<Player>();
	}

	public void SetMaxHealth(int health)
	{
		slider.maxValue = player.health;
		slider.value = player.health;

		fill.color = gradient.Evaluate(1f);
	}

    public void TakeDamage(int amount)
	{
		if (player.health <= 0)
		{
			player.PlayerDied();
		}
		else
		{
			player.health -= amount;
			slider.value = player.health;
			fill.color = gradient.Evaluate(slider.normalizedValue);
		}

	}

}
