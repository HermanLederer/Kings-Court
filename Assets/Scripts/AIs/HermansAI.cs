using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermansAI : PlayerAI
{
	// Editor variables

	// Public variables

	// Private variables
	private AICore.AIControlBrain[] team;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	void Awake()
	{
		team = new AICore.AIControlBrain[3];
		team[0] = target;
		team[1] = assasin;
		team[2] = stunner;
	}

	void Start()
	{
		foreach (AICore.AIControlBrain member in team)
		{
			member.speedMultiplier = 1;
		}
	}

	void Update()
	{
		foreach (AICore.AIControlBrain member in team)
		{
			member.targetDirection.z += (member.transform.position.x + member.transform.position.z) / 2 * Random.Range(-180, 180);
		}
	}

	//--------------------------
	// HermansAI methods
	//--------------------------
}
