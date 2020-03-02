﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermansAI : PlayerAI
{
	// Editor variables

	// Public variables

	// Private variables
	private float nextRandomMoveTime;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	new void Start()
	{
		base.Start();
		nextRandomMoveTime = Time.time;
	}

	void Update()
	{
		if (nextRandomMoveTime <= Time.time)
		{
			foreach (AICore.AIBrainInterface member in members)
			{
				//member.SetDestination(member.transform.position + Vector3.one * Random.Range(-5f, 5f));
				member.SetDestination(member.transform.position * -1);
			}

			nextRandomMoveTime = Time.time + 0.5f;
		}

		// target
		foreach (AICore.AIBrainInterface visibleEntityInterface in target.GetVisibleAIEntities())
		{
			if (visibleEntityInterface.team != target.team) target.SetDestination(-visibleEntityInterface.transform.position);
		}

		// assassin
		foreach (AICore.AIBrainInterface visibleEntityInterface in assassin.GetVisibleAIEntities())
		{
			if (visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.target) assassin.SetDestination(visibleEntityInterface.transform.position);
		}

		// stunner
		foreach (AICore.AIBrainInterface visibleEntityInterface in stunner.GetVisibleAIEntities())
		{
			if (visibleEntityInterface.team != target.team && visibleEntityInterface.type != AICore.AIType.stunner) stunner.SetDestination(visibleEntityInterface.transform.position);
		}
	}

	//--------------------------
	// HermansAI methods
	//--------------------------
}
