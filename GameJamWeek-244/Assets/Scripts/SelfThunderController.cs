using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shoot a thunder bolt from 5 points to the player
public class SelfThunderController : MonoBehaviour
{
	public float ThunderBoltDuration = 1f;

	// Thunder Bolt 
	[SerializeField] GameObject thunderBoltPrefab;

	[SerializeField] Transform spot1;
	[SerializeField] Transform spot2;
	[SerializeField] Transform spot3;
	[SerializeField] Transform spot4;
	[SerializeField] Transform spot5;

	public void SelfShootThunder()
	{
		ShootBolt(spot1, transform);
		ShootBolt(spot2, transform);
		ShootBolt(spot3, transform);
		ShootBolt(spot4, transform);
		ShootBolt(spot5, transform);
	}

	public void ShootBolt(Transform start, Transform end)
	{
		// Start LightningBolt
		thunderBoltPrefab.transform.GetChild(0).position = start.position;

		// End LightningBolt
		thunderBoltPrefab.transform.GetChild(1).position = end.position;

		GameObject lightningBoltInst = Instantiate(thunderBoltPrefab);

		StartCoroutine(LightningBoltDuration(lightningBoltInst));
	}

	IEnumerator LightningBoltDuration(GameObject lightningBoltToKill)
	{
		yield return new WaitForSeconds(ThunderBoltDuration);

		Destroy(lightningBoltToKill);
	}
}
