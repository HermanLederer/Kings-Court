using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICore
{
	public class AICombatBrain : MonoBehaviour
	{
		[SerializeField] private LayerMask playerLayerMask;

		private AIBrainInterface brainInterface;
		private readonly float radius = 1.5f;

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		private void Awake()
		{
			brainInterface = GetComponent<AIBrainInterface>();
		}

		private void Update()
		{
			if (brainInterface.stun < Time.time)
			{
				switch (brainInterface.type)
				{
					case AIType.assassin:
						foreach (AIBrainInterface entityInterface in GetEnemiesInRadius(radius))
						{
							entityInterface.TakeDamage();
						}
						break;
					case AIType.stunner:
						foreach (AIBrainInterface entityInterface in GetEnemiesInRadius(radius))
						{
							entityInterface.Stun(5f);
							brainInterface.Stun(7f);
						}
						break;
				}
			}
		}

		//--------------------------
		// AICombat methods
		//--------------------------
		public List<AIBrainInterface> GetEnemiesInRadius(float radius)
		{
			List<AIBrainInterface> visibleEntities = new List<AIBrainInterface>();

			// geting all AI entities in radus
			Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, radius, playerLayerMask);

			// converting colliders to AIEntities
			foreach (Collider collider in collidersInRadius)
			{
				// does not contain AIControlBrain check
				if (collider.gameObject.GetComponent<AIBrainInterface>() == null) continue;

				// is eliminated check
				if (collider.transform.parent.GetComponent<PlayerAI>().isEliminated) continue;

				// teammate check
				if (collider.gameObject.GetComponent<AIBrainInterface>().team == brainInterface.team) continue;

				// adding to visibleEntites list
				visibleEntities.Add(collider.gameObject.GetComponent<AIBrainInterface>());
			}

			return visibleEntities;
		}
	}
}