using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICore
{
	/// <summary>
	/// AIControlBrain is used by players to manipulate the entity's movement
	/// </summary>
	[RequireComponent(typeof(AIPerceptionBrain))]
	[RequireComponent(typeof(CharacterController))]
	public class AIControlBrain : MonoBehaviour
	{
		// Other components
		public AIPerceptionBrain perceptionBrain { get; private set; }
		private CharacterController characterController;

		// Editor variables
		[SerializeField] private float maxMovementSpeed = 3f;
		[Tooltip("Maximum rotation angle per unit of time in radians")]
		[SerializeField] private float maxRotationSpeed = 0.1f;

		// Public variables
		[HideInInspector] private float _speedMultiplier;
		[HideInInspector] public Vector3 targetDirection;

		public float speedMultiplier
		{
			get { return _speedMultiplier; }
			set
			{
				_speedMultiplier = value;

				// limit to 1 as maximum value
				if (_speedMultiplier > 1) _speedMultiplier = 1;
			}
		}

		// Private variables

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		void Awake()
		{
			characterController = GetComponent<CharacterController>();

			speedMultiplier = 1;
			targetDirection = transform.rotation.eulerAngles;
		}

		void Update()
		{
			// rotate towards targetRotation
			Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, maxRotationSpeed * Time.deltaTime, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDirection);

			// moving
			characterController.SimpleMove(Vector3.zero * speedMultiplier);
		}

		//--------------------------
		// AIControlBrain methods
		//--------------------------
	}
}
