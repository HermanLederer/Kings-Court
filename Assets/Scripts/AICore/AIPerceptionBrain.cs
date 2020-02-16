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
		private SphereCollider viewCollider;

		// Editor variables
		[SerializeField] private float fieldOfView = 3f;

		// Public variables

		// Private variables

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		void Awake()
		{
			viewCollider = GetComponent<SphereCollider>();
		}

		//--------------------------
		// AIPerceptionBrain methods
		//--------------------------
		public List<AIEntity> GetVisibleAIEntities()
		{
			return null; // NWY, please fix
		}
	}

	// Class for passing visible AI objects to PlayerAI
	public class AIEntity : Transform
	{
		public AITypes type { get; private set; }
	}
}
