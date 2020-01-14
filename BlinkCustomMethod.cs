using UnityEngine;

namespace BlinkFatigue
{
	internal static class BlinkCustomMethod
	{
		internal static float reworkSubstractTime = 0f;

		internal static void CustomBlinkingSequence(Scp173PlayerScript scpScript)
		{
			if (!scpScript.isServer || !scpScript.isLocalPlayer)
			{
				return;
			}
			
			Scp173PlayerScript._remainingTime -= Time.fixedDeltaTime;
			Scp173PlayerScript._blinkTimeRemaining -= Time.fixedDeltaTime;

			if (Scp173PlayerScript._remainingTime >= 0f)
			{
				return;
			}
			
			Scp173PlayerScript._blinkTimeRemaining = scpScript.blinkDuration_see + 0.4f;
			Scp173PlayerScript._remainingTime = Mathf.Max(BlinkConfigs.minReworkBlinkTime, Random.Range(BlinkConfigs.minBlinkTime, BlinkConfigs.maxBlinkTime) - reworkSubstractTime);
			
			if (!scpScript._allowMove)
			{
				reworkSubstractTime += Random.Range(BlinkConfigs.reworkAddMin, BlinkConfigs.reworkAddMax);
				// If SCP-173 is sick of your shit, this basically negates an infinite stacking of the blink fatigue ability
				if (reworkSubstractTime > BlinkConfigs.minBlinkTime)
				{
					reworkSubstractTime = BlinkConfigs.minBlinkTime;
				}
			}
			
			var array = PlayerManager.players;
			for (int i = 0; i < array.Count; i++)
			{
				var comp = array[i].GetComponent<Scp173PlayerScript>();
				if (comp != null)
				{
					comp.RpcBlinkTime();
				}
			}
		}
	}
}
