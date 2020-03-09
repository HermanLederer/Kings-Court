using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaysAI : PlayerAI
{
	// Other components

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

	void Update()
	{
//-------------------------------------------------------------------------------
		//The war bringer, Nio (Assassin)
			//check what Nio sees.
				//if Nio sees an enemy defender, nio head in that direction.
						//Nio will then move to pass the defender, continuously checking if they see the target.
				//if at any time Nio sees the target, it will chase.
				//if Nio sees an attacker, Trigger WarbringerSplit.
				//else if Nio sees nothing, then 

//-------------------------------------------------------------------------------



		//Lady 'Get fucked' Amari (Defender)
			//Circle SnuffleSnuff
				//If Amari sees an attacker, Trigger snufflsnuff split.


//------------------------------------------------------------------------------

		//Sir Noble SnuffleSnuff (Target)
			//Follow Nio.
		//Check what SnuffleSnuff sees.
			//If something is approaching SnuffleSnuff, trigger AmariStungun to stun it. then run the other way.
			//T


		//Snufflesnuff split
			//run the oposite direction from Amari and hide behind a nearby corner. 
			//then wait for amari to have registered the area to be safe.

		//Warbringer split
			//SnuffleSnuff will run and hide behind a nearby corner.
			//Trigger Amari's GuardingStance.

		//AmariStungun
			//Overrule guardingStance, and only attack the enemy attacker.
			//if the enemy attacker hasnt gotten closer to SnuffleSnuff in the last 3 seconds, Go back to target and circling.

//-------------------------------------------------------------------------------




	}
	//--------------------------
	// RaysAI methods
	//--------------------------


}
