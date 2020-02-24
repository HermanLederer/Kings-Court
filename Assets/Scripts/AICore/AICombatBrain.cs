using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICore
{
	public class AICombatBrain : MonoBehaviour
	{
		public float attackSpeed = 1f;
		[SerializeField] private LayerMask playerLayerMask;

		private AIBrainInterface brainInterface;

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		private void Awake()
		{
			brainInterface =  GetComponent<AIBrainInterface>();
		}

		private void Update()
		{
			// Debugging for damage
			if (Input.GetKeyDown(KeyCode.T))
			{
				brainInterface.TakeDamage();
			}

			switch (brainInterface.type)
			{
				case AIType.target: // target checks
					foreach (AIEntity entity in GetAIEntitiesInRadius(1f))
					{
						switch (entity.type)
						{
							case AIType.assassin:
								brainInterface.TakeDamage();
								break;
							case AIType.stunner:
								brainInterface.Stun(4f); // getting stunned by stunner
								break;
						}
					}
					break;
				case AIType.assassin: // assassin checks
					foreach (AIEntity entity in GetAIEntitiesInRadius(1f))
					{
						switch (entity.type)
						{
							case AIType.target:
								brainInterface.Stun(5f); // stunning themselves on target collision (for testing)
								break;
							case AIType.stunner:
								brainInterface.Stun(7f); // getting stunned by stunner
								break;
						}
					}
					break;
				case AIType.stunner: // stunner checks
					foreach (AIEntity entity in GetAIEntitiesInRadius(1f))
					{
						brainInterface.Stun(5f); // stunning themselves
					}
					break;
			}
		}

		//--------------------------
		// AICombat methods
		//--------------------------
		public List<AIEntity> GetAIEntitiesInRadius(float radius)
		{
			List<AIEntity> visibleEntities = new List<AIEntity>();

			// geting all AI entities in radus
			Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, radius, playerLayerMask);

			// converting colliders to AIEntities
			foreach (Collider collider in collidersInRadius)
			{
				// self check
				if (collider.gameObject == gameObject) continue;

				// adding to visibleEntites list
				visibleEntities.Add(new AIEntity(collider.transform, collider.gameObject.GetComponent<AIBrainInterface>().type, collider.gameObject.GetComponent<AIBrainInterface>().team));
			}

			return visibleEntities;
		}
	}
}