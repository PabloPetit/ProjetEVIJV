using UnityEngine;
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

	// Use this for initialization
	public void Start ()
	{
		player = GetComponent<Player> ();

		life = maxLife;
		dead = false;
		timer = 0f;
		bloodCanvas = new EventName (BloodCanvas.BLOOD_CANVAS_CHANNEL);
		lifeBar = new EventName (LifeBar.LIFE_BAR_CHANNEL);


	}
	
	// Update is called once per frame
	public void Update ()
	{
		timer += Time.deltaTime;
		if (timer >= timeBeforeAutoCure && !dead && life != maxLife) {
			life += Time.deltaTime * autoCureValue;
		}
	
		if (player.isHuman) {
			EventManager.TriggerAction (bloodCanvas, new object[]{ (life / maxLife) });
			EventManager.TriggerAction (lifeBar, new object[]{ (life / maxLife) });
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

		float damage = (float)param [0];
		int id = (int)param [1];

		life -= damage;

		if (life <= 0f) {
			life = 0f;
			Death ();
			SendXp (id);
		}
	}

	public void SendXp (int id)
	{
		EventName xpEvent = new EventName (Player.XP_CHANNEL, id);
		EventManager.TriggerAction (xpEvent, new object[]{ player.experience.RetrievedXp () });
	}

	public void ReceiveLife (object[] param)
	{
		if (dead)
			return;
	}

	public virtual void Death ()
	{
		dead = true;
	}


}
