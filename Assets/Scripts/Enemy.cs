using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIStats))]
public class Enemy : Interactable
{
    PlayerManager PlayerManager;
    AIStats myStats;

    void Start()
    {
        PlayerManager = PlayerManager.instance;
        myStats = GetComponent<AIStats>();    
    }
    public override void Interact()
    {
        base.Interact();
        AICombat playerCombat = PlayerManager.player.GetComponent<AICombat>();
        if(playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}
