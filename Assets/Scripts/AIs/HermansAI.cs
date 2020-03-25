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

	private float targeTurnValue = 0f;
	private float targetScare = 0f;
	private float stunnerTurnaroundValue = 0f;

	private void Update()
	{
		// Previous direction
		Vector3 dir = target.transform.forward;

		// Wavy movement
		Vector3 wavyMovement = target.transform.right * (Mathf.Sin(Time.time) * 0.32f);

		// Random navigation
		if (Time.time > targetScare)
		{
			if (target.GetVelocity().magnitude <= 0.1f)
			{
				// steering sideways
				dir += target.transform.right * Mathf.Sin(targeTurnValue);
				Debug.DrawRay(target.transform.position, target.transform.right * Mathf.Sin(targeTurnValue) * 2, Color.green);
				targeTurnValue += 1f * Time.deltaTime;
			}

			
			
		}

		// Individual directions based on target
		Vector3 dirTarget = dir;
		//Vector3 dirAssassin = dir;
		//Vector3 dirStunner = dir;

		// Stunner wants to look around
		//stunnerTurnaroundValue += Time.deltaTime;// * 0.5f;
		//if (Mathf.Sin(stunnerTurnaroundValue) > 0f)
		//{
		//	dirStunner = -dirTarget;
		//}

		// Flocking
		//if (Vector3.Distance(target.transform.position, assassin.transform.position) < 3f)
		//{
		//	Vector3 resistance = assassin.transform.position - target.transform.position;
		//	float distance = Vector3.Distance(target.transform.position, assassin.transform.position);
		//	dirAssassin += resistance * (3f - distance);
		//	Debug.DrawRay(assassin.transform.position, resistance * (3f - distance), Color.blue);
		//}

		// Getting visible entities form all team members
		List<AICore.AIBrainInterface> visibleEntities = target.GetVisibleAIEntities();
		visibleEntities.AddRange(assassin.GetVisibleAIEntities());
		visibleEntities.AddRange(stunner.GetVisibleAIEntities());

		// Reacting to enemies
		foreach (AICore.AIBrainInterface visibleEntityInterface in visibleEntities)
		{
			// target avoids assassin
			if (visibleEntityInterface.team != target.team)
				if (visibleEntityInterface.type != AICore.AIType.target)
				{
					Vector3 enemyDir = (visibleEntityInterface.transform.position - target.transform.position).normalized;
					dirTarget = -enemyDir;
					Debug.DrawRay(target.transform.position, enemyDir, Color.red);
				}
		}

		// Movement
		Debug.DrawRay(target.transform.position, dir * 2f);
		target.SetDestination(target.transform.position + dirTarget);

		//Debug.DrawRay(assassin.transform.position, dir * 2f);
		//assassin.SetDestination(target.transform.position + dirAssassin);

		//Debug.DrawRay(stunner.transform.position, dir * 2f);
		//stunner.SetDestination(target.transform.position + dirStunner);

		return;

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
