﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public Player player;

	public float maxLife;
	public float life;
	public bool dead;

	public float timeBeforeAutoCure;
	public float autoCureValue;
	public float timer;

	public EventName lifeChannel;
	public EventName damageChannel;

	public EventName bloodCanvas;
	public EventName lifeBar;

	public Player lastShooter;
	public Vector3 lastShooterPosition;
	public float lastHitDate;

	public UnityEngine.AI.NavMeshAgent nav;

	// Position of the shooter when this player was hit


	float maxLifeInit;

	// Use this for initialization
	public void Start ()
	{
		player = GetComponent<Player> ();

		life = maxLife;
		dead = false;
		timer = 0f;
		lastHitDate = 0f;
		bloodCanvas = new EventName (BloodCanvas.BLOOD_CANVAS_CHANNEL);
		lifeBar = new EventName (LifeBar.LIFE_BAR_CHANNEL);
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> (); // null if human

		maxLifeInit = maxLife;
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{

		maxLife = maxLifeInit * (1 + Mathf.Log10 (.5f + player.experience.level / 2f));

		timer += Time.deltaTime;
		if (timer >= timeBeforeAutoCure && !dead && life != maxLife) {
			life += Time.deltaTime * autoCureValue;
			life = Mathf.Min (life, maxLife);

		}
	
		if (player.isHuman) {
			EventManager.TriggerAction (bloodCanvas, new object[]{ (life / maxLife) });
			EventManager.TriggerAction (lifeBar, new object[]{ life, maxLife });
		}

	}


	public void OpenChannels (int id)
	{
		if (lifeChannel == null) {
			lifeChannel = new EventName (Player.LIFE_CHANNEL, id);
		}
		if (damageChannel == null) {
			damageChannel = new EventName (Player.DAMAGE_CHANNEL, id);
		}

		EventManager.StartListening (lifeChannel, ReceiveLife);
		EventManager.StartListening (damageChannel, TakeDamage);
	}

	public void CloseChannels ()
	{
		EventManager.StopListening (lifeChannel);
		EventManager.StopListening (damageChannel);
	}

	public void TakeDamage (object[] param)
	{

		if (dead)
			return;

		timer = 0f;

		float damage = (float)param [0];
		lastShooter = (Player)param [1];
		lastShooterPosition = lastShooter.gameObject.transform.position;
		lastHitDate = Time.time;
	
		if (lastShooter.isHuman) {
			EventName hit = new EventName (HitMarker.HITMARKER_CHANNEL);
			EventManager.TriggerAction (hit, new object[]{ });
		}

		life -= damage;

		if (life <= 0f) {
			life = 0f;
			dead = true;

			if (nav != null) {
				nav.Stop ();
				nav.enabled = false;//For Teleportation purposes
			}

			this.player.deathCount++;
			Death ();

			if (lastShooter.isHuman) {
				SendPlayerLogInfo ();
			}

			SendXp ();
			SendKillInfo ();
			SendToKillLog ();

		}
	}

	public virtual void SendPlayerLogInfo ()
	{
		string str = "ENEMY KILLED : " + player.name + " [ " + player.experience.level + " ] - [ +" + (int)(player.experience.RetrievedXp ()) + " XP]";
		EventName playerLogEvent = new EventName (PlayerLog.PLAYER_LOG_CHANNEL);
		EventManager.TriggerAction (playerLogEvent, new object[]{ str });
	}

	public virtual void SendXp ()
	{
		EventName xpEvent = new EventName (Player.XP_CHANNEL, lastShooter.id);
		EventManager.TriggerAction (xpEvent, new object[]{ player.experience.RetrievedXp () });
	}

	public virtual void SendKillInfo ()
	{
		EventName killEvent = new EventName (Player.KILL_COUNT_CHANNEL, lastShooter.id);
		EventManager.TriggerAction (killEvent, new object[]{ player });
	}

	public virtual void SendToKillLog ()
	{
		EventName killLogEvent = new EventName (KillLog.KILL_LOG_CHANNEL);
		// create string
		string color1; 
		string color2; 
		string finalstr;


		finalstr = lastShooter.name + " -> " + player.name;
		EventManager.TriggerAction (killLogEvent, new object[]{ finalstr });
	}


	public void ReceiveLife (object[] param)
	{
		if (dead)
			return;
	}

	public virtual void Death ()
	{
		
	}


}
