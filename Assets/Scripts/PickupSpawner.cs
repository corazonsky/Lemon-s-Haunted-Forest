using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
	public GameObject prefab;
	[Range(0.1f,0.6f)] public float emptyRadius = 0.25f;
	public int numberOfPickups = 5;
	public Vector3 spawnRange;

	bool m_IsPlaced;
	GameObject lastSpawn = null;
	

    private void Start()
    {
		for (int i = 0; i < numberOfPickups; i++)
		{
			m_IsPlaced = false;
			do
			{
				//Find an empty place to spawn a pickup:
				Vector3 spawnPosition =
					transform.position +
					new Vector3(
						Random.value * spawnRange.x,
						Random.value * spawnRange.y,
						Random.value * spawnRange.z
					);
				// Test whether it's really empty:
				if (Physics.OverlapSphere(spawnPosition, emptyRadius).Length == 0)
				{
					// Create a copy of [prefab] at position [spawnPosition], with no rotation, and
					// store the copy in the variable lastSpawn:
					lastSpawn = Instantiate(prefab, spawnPosition, Quaternion.identity);
					Debug.Log("Spawning pickup at " + spawnPosition);
					m_IsPlaced = true;
				}
				else
				{
					Debug.Log("Bad position - trying again");
				}
			} while (!m_IsPlaced);
		}
		
	}

    void Update()
	{

	}
}
