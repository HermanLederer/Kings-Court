using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICore
{
	public class AICombatBrain : MonoBehaviour
	{
		[SerializeField] private int maxHealth = 3;
		public int currentHealth { get; private set; }

		public float attackSpeed = 1f;
		private float attackCD = 0f;
		public float attackDelay = 0.6f;

		//--------------------------
		// MonoBehaviour methods
		//--------------------------
		void Start()
		{
			currentHealth = maxHealth;
		}

		private void Update()
		{
			attackCD -= Time.deltaTime;

			// Debugging for damage
			if (Input.GetKeyDown(KeyCode.T))
			{
				TakeDamage(1);
			}
		}

		//--------------------------
		// AICombat methods
		//--------------------------
		public void Attack()
		{
			if (attackCD <= 0f)
			{
				StartCoroutine(DoDamage(attackDelay));

				attackCD = 1f / attackSpeed;
			}
		}

		IEnumerator DoDamage(float delay)
		{
			yield return new WaitForSeconds(delay);
			TakeDamage(1);
		}

		public void TakeDamage(int damage)
		{
			currentHealth -= damage;
			Debug.Log(transform.name + " was hit");

			if (currentHealth <= 0)
			{
				Die();
			}
		}

		public virtual void Die()
		{
			Debug.Log(transform.name + " died");
			Destroy(gameObject);
		}
	}
}