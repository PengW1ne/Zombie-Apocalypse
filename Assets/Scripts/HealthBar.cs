using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public Animation HeartPuls;
	public Gradient gradient;
	public Image fill;
	private Player player;
	[SerializeField] private Text healthCount;

	private void Start()
	{
		player = FindObjectOfType<Player>();
		healthCount.text = player.health.ToString();
	}

	private void Update()
	{
		if (player.health <= 0)
		{
			player.PlayerDied();
		}
	}

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}

    public void TakeDamage(int amount)
	{
		if (player.health <= 45)
		{
			HeartPuls.Play();
		}
		
		player.health -= amount;
		
		if (player.health <= 0)
		{
			player.health = 0;
			player.PlayerDied();
		}
		
		healthCount.text = player.health.ToString();
		slider.value = player.health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
