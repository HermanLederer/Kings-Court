using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AmarsAI : PlayerAI
{
	// Private variables
	private float enemyDistanceRun = 15f;

	private float nextRandomMoveTime;

	private Vector3 follow;
	

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	new void Start()
	{
		base.Start();
		nextRandomMoveTime = 0f;
	}

	private void Update()
	{
		// Vector3 targetWander = target.transform.right * Mathf.Sin(Time.time * 2.0f);
		// Vector3 stunnerWander = stunner.transform.right * Mathf.Sin(Time.time * 6f);

		if (nextRandomMoveTime <= Time.time)
		{
			foreach (AICore.AIBrainInterface member in members)
			{
				member.SetDestination(member.transform.position + Vector3.one * Random.Range(-5f, 5f));
			}

			nextRandomMoveTime = Time.time + 0.5f;
		}

		Vector3 follow;

		// target
		follow = target.transform.forward;
		
		targetRun();

		// assassin
		assassinChase();
		

		// Stunner
		foreach (AICore.AIBrainInterface visibleEntityInterface in stunner.GetVisibleAIEntities())
		{
			if (visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.assassin)
			{
				if (Vector3.Distance(stunner.transform.position, assassin.transform.position) > 20f)
					{
						stunner.SetDestination(assassin.transform.position);
					}
			}
		}
		stunnerChase();
	}

	//--------------------------
	// AmarsAI methods
	//--------------------------
	private void targetRun()
	{
		Vector3 targetWander = target.transform.right * Mathf.Sin(Time.time * 2.0f);
		float distance = Vector3.Distance(transform.position, assassin.transform.position);
		if (distance < enemyDistanceRun)
		{
			Vector3 dirToEnemy = transform.position - assassin.transform.position;
			Vector3 newPos = transform.position + dirToEnemy;

			target.SetDestination(stunner.transform.position);
		}
		else
		{
			follow += (target.transform.position - stunner.transform.position).normalized;
		}
		follow += targetWander;
		target.SetDestination(target.transform.position + follow);
	}

	private void assassinChase()
	{
		foreach (AICore.AIBrainInterface visibleEntityInterface in assassin.GetVisibleAIEntities())
		{
			if (visibleEntityInterface.team != target.team && visibleEntityInterface.type == AICore.AIType.target)
			{
				assassin.SetDestination(visibleEntityInterface.transform.position);
			}

			else if (Vector3.Distance(assassin.transform.position, target.transform.position) > 20f)
			{
				follow += (assassin.transform.position - target.transform.position).normalized;
			}
		}
	}
	
	 private void stunnerChase()
	 {
		 Vector3 stunnerWander = stunner.transform.right * Mathf.Sin(Time.time * 6f);
		 if (Vector3.Distance(stunner.transform.position, target.transform.position) > 10f)
		{
			stunner.SetDestination(target.transform.position);
		}
			follow = stunner.transform.forward;
			follow += stunnerWander;
	 }
}
