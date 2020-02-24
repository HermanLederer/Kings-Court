using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIStats))]
public class AICombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCD = 0f;
    public float attackDelay = 0.6f;
    public event System.Action onAttack;
    AIStats myStats;

    void Start()
    {
        myStats = GetComponent<AIStats>();
    }
    private void Update()
    {
        attackCD -= Time.deltaTime;   
    }
   public void Attack(AIStats targetStats)
   {
       if(attackCD <= 0f)
       {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (onAttack != null)
            {
                onAttack();
            }
            attackCD = 1f / attackSpeed;
       }
   }

   IEnumerator DoDamage (AIStats stats, float delay)
   {
       yield return new WaitForSeconds(delay);
       stats.TakeDamage(myStats.damage.GetValue());
   }
}
