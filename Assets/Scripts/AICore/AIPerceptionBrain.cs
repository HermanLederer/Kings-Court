using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICore
{
	/// <summary>
	/// AIPerceptionBrain is used by players to observer the entity's visible environment
	/// </summary>
	public class AIPerceptionBrain : MonoBehaviour
	{
		// Other components

		// Editor variables
		[Range(0, 360)]
		[SerializeField] private float angleOfView = 3f;
		[SerializeField] private float radiusOfView = 3f;
		[SerializeField] private LayerMask playerLayerMask;

		// Public variables

		// Private variables

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		void Awake()
		{

		}

		//--------------------------
		// AIPerceptionBrain methods
		//--------------------------
		public List<AIBrainInterface> GetVisibleAIEntities()
		{
			List<AIBrainInterface> visibleEntities = new List<AIBrainInterface>();

			// geting all AI entities
			Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, radiusOfView, playerLayerMask);

			// filtering out entities out of radiusOfView and behind obstacles
			foreach (Collider collider in collidersInRadius)
			{
				// self check
				if (collider.gameObject == gameObject) continue;

				Vector3 entityPosition = collider.transform.position;

				float distanceToEntity = Vector3.Distance(transform.position, entityPosition);
				Vector3 directionToEntity = Vector3.Normalize(entityPosition - transform.position);

				// out of angleOfView check
				if (Vector3.Angle(transform.forward, directionToEntity) > angleOfView / 2) continue;

				// behind an obstacle check
				RaycastHit hit;
				Physics.Raycast(transform.position, directionToEntity, out hit, distanceToEntity);
				if (hit.collider != collider) continue;

				// does not contain AIControlBrain check
				if (collider.gameObject.GetComponent<AIBrainInterface>() == null) continue;

				// adding to visibleEntites list
				visibleEntities.Add(collider.gameObject.GetComponent<AIBrainInterface>());
			}

			return visibleEntities;
		}
	}
}
