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
		public List<AIEntity> GetVisibleAIEntities()
		{
			List<AIEntity> visibleEntities = new List<AIEntity>();

			// geting all AI entities
			Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, radiusOfView, playerLayerMask);

			// filtering out entities out of radiusOfView and behind obstacles
			foreach (Collider collider in collidersInRadius)
			{
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
				if (collider.gameObject.GetComponent<AIControlBrain>() == null) continue;

				// adding to visibleEntites list
				visibleEntities.Add(new AIEntity(collider.transform, collider.gameObject.GetComponent<AIControlBrain>().type));
			}

			return visibleEntities;
		}
	}

	// Class for passing visible AI objects to PlayerAI
	public class AIEntity
	{
		public Transform transform { get; private set; }
		public AIType type { get; private set; }

		public AIEntity(Transform transform, AIType type)
		{
			this.transform = transform;
			this.type = type;
		}
	}
}
