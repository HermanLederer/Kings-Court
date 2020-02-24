using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermansAI : PlayerAI
{
	// Editor variables

	// Public variables

	// Private variables
	private AICore.AIBrainInterface[] team;
	private float nextRandomMoveTime;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	void Awake()
	{
		team = new AICore.AIBrainInterface[3];
		team[0] = target;
		team[1] = assassin;
		team[2] = stunner;
	}

	void Start()
	{
		nextRandomMoveTime = Time.time;
	}

	void Update()
	{
		if (nextRandomMoveTime <= Time.time)
		{
			foreach (AICore.AIBrainInterface member in team)
			{
				member.SetDestination(member.transform.position + Vector3.one * Random.Range(-5f, 5f));
				//member.SetDestination(member.transform.position * -1);
			}

			nextRandomMoveTime = Time.time + 0.5f;
		}

		// target
		foreach (AICore.AIEntity visibleEntity in target.GetVisibleAIEntities())
		{
			if (visibleEntity.team != target.team) target.SetDestination(-visibleEntity.transform.position);
		}

		// assassin
		foreach (AICore.AIEntity visibleEntity in assassin.GetVisibleAIEntities())
		{
			if (visibleEntity.team != target.team && visibleEntity.type == AICore.AIType.target) assassin.SetDestination(visibleEntity.transform.position);
		}

		// stunner
		foreach (AICore.AIEntity visibleEntity in stunner.GetVisibleAIEntities())
		{
			if (visibleEntity.team != target.team && visibleEntity.type != AICore.AIType.stunner) stunner.SetDestination(visibleEntity.transform.position);
		}
	}

	//--------------------------
	// HermansAI methods
	//--------------------------
}
