using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmarsAI : PlayerAI
{
	// Editor variables

	// Public variables
	public float lookRadius = 20;

	// Private variables
	private AICore.AIControlBrain[] team;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	void Awake()
	{
		team = new AICore.AIControlBrain[3];
		team[0] = target;
		team[1] = assassin;
		team[2] = stunner;
	}

	void Start()
	{
		foreach (AICore.AIControlBrain member in team)
		{
			assassin.speedMultiplier = 1;
			target.speedMultiplier = 1.3f;
			stunner.speedMultiplier = 1.6f;
		}
	}

	void Update()
	{
		foreach (AICore.AIControlBrain member in team)
		{
			member.targetDirection.z += (member.transform.position.x + member.transform.position.z) / 2 * Random.Range(-180, 180);
		}
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);	
	}
		//--------------------------
		// AIControlBrain methods
		//--------------------------
	
	//--------------------------
	// AmarsAI methods
	//--------------------------
}
