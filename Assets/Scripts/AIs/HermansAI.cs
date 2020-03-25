using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermansAI : PlayerAI
{
	// Editor variables

	// Public variables

	// Private variables

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	new void Start()
	{
		base.Start();
	}

	private float targetScare = 0f;
	private Vector3 targetScarePosition = Vector3.zero;
	private Vector3 targetScareDirection = Vector3.zero;

	private float enemyTargetLastSeenTime = 0f;
	private Vector3 enemyTargetLastSeenPosition = Vector3.zero;

	private float stunnerTurnAroundValue = 0f;

	private void Update()
	{
		//
		//
		// Previous directions
		Vector3 targetForward = target.transform.forward;
		Vector3 assassinForward = assassin.transform.forward;
		Vector3 stunnerForward = stunner.transform.forward;

		//
		//
		// Wavy movement
		Vector3 wavyMovementTarget = target.transform.right * Mathf.Sin(Time.time * 2.5f) * 0.5f;
		Vector3 wavyMovementAssassin = target.transform.right * Mathf.Sin(Time.time * 1f) * 0.5f;
		Vector3 wavyMovementStunner = target.transform.right * Mathf.Sin(Time.time * 5f) * 1f;

		//
		//
		// Getting visible entities form all team members
		List<AICore.AIBrainInterface> visibleEntities = target.GetVisibleAIEntities();
		visibleEntities.AddRange(assassin.GetVisibleAIEntities());
		visibleEntities.AddRange(stunner.GetVisibleAIEntities());

		//
		//
		// Reacting to enemies
		foreach (AICore.AIBrainInterface visibleEntityInterface in visibleEntities)
		{
			if (visibleEntityInterface.team != target.team)
			{
				// enemy assassin
				if (visibleEntityInterface.type == AICore.AIType.assassin)
				{
					if (Vector3.Distance(target.transform.position, visibleEntityInterface.transform.position) < Mathf.Max(15f, Vector3.Distance(target.transform.position, stunner.transform.position)))
					{
						targetScare = Time.time + 5f;
						targetScareDirection = -(visibleEntityInterface.transform.position - target.transform.position).normalized;
						targetScarePosition = visibleEntityInterface.transform.position;
					}
				}

				// enemy target
				else if (visibleEntityInterface.type == AICore.AIType.target)
				{
					enemyTargetLastSeenPosition = visibleEntityInterface.transform.position;
					enemyTargetLastSeenTime = Time.time;
				}
			}
		}

		//
		//
		// Analizing data and moving
		Vector3 direction;

		//
		//
		// Target
		direction = targetForward;

		if (targetScare >= Time.time)
		{
			direction = targetScareDirection;
		}
		else
		{
			if (Vector3.Distance(target.transform.position, stunner.transform.position) > 7.5f)
			{
				direction += (target.transform.position - stunner.transform.position).normalized;
				Debug.DrawRay(target.transform.position, (target.transform.position - stunner.transform.position).normalized * 5f, Color.blue);
			}
		}

		direction += wavyMovementTarget;
		direction = addWallResistance(target, direction);
		target.SetDestination(target.transform.position + direction);

		//
		//
		// Assassin
		if (enemyTargetLastSeenTime >= Time.time - 8f)
		{
			if (assassin.GetVelocity().magnitude <= 0)
				enemyTargetLastSeenTime = 0f;

			assassin.SetDestination(enemyTargetLastSeenPosition);
		}
		else
		{
			if (Vector3.Distance(assassin.transform.position, target.transform.position) > 20f)
			{
				assassin.SetDestination(target.transform.position);
			}
			else
			{
				direction = assassinForward;
				direction += wavyMovementAssassin;
				if (Mathf.Sin(Time.time * 1f) > 0.1f)
					direction = -direction;

				direction = addWallResistance(assassin, direction);

				if (Vector3.Distance(assassin.transform.position, target.transform.position) < 7f)
					direction += (assassin.transform.position - target.transform.position).normalized;

				assassin.SetDestination(assassin.transform.position + direction);
			}
		}
		
		//
		//
		// Stunner
		if (targetScare >= Time.time)
		{
			if (Vector3.Distance(stunner.transform.position, targetScarePosition) < 1.5f)
				targetScare = 0;

			stunner.SetDestination(targetScarePosition);
		}
		else
		{
			if (Vector3.Distance(stunner.transform.position, target.transform.position) > 7.5f)
			{
				stunner.SetDestination(target.transform.position);
			}
			else
			{
				direction = stunnerForward;
				direction += wavyMovementStunner;

				if (Mathf.Sin(Time.time * 1f) > 0.1f)
					direction = -direction;

				if (Vector3.Distance(stunner.transform.position, target.transform.position) < 2.5f)
					direction += (stunner.transform.position - target.transform.position).normalized;

				direction = addWallResistance(stunner, direction);
				stunner.SetDestination(stunner.transform.position + direction);
			}
		}
	}

	//--------------------------
	// HermansAI methods
	//--------------------------
	private Vector3 addWallResistance(AICore.AIBrainInterface member, Vector3 direction)
	{
		RaycastHit hit;
		float wallDistance = 7.5f;
		Vector3 wallResistance = Vector3.zero;
		if (member.raycastForward(out hit))
		{
			if (hit.distance <= wallDistance)
			{
				wallResistance = hit.normal * (hit.distance / wallDistance);
			}
			else
				wallResistance = Vector3.zero;
		}

		return direction.normalized + wallResistance;
	}
}
