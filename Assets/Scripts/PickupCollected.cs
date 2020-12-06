using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupCollected : MonoBehaviour
{
    int pickups = 0;
    PickupSpawner spawner;
    int totalPickups;

    private void Awake()
    {
        Pickup.OnPickupGrab += ShowPickupsText;
    }
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PickupSpawner>();
        totalPickups = spawner.numberOfPickups;
        ShowPickupsText();
    }

    private void OnDestroy()
    {
        Pickup.OnPickupGrab -= ShowPickupsText;   
    }

    void ShowPickupsText()
    {
        GetComponent<TextMeshProUGUI>().text = "Lemons: "+pickups+"/"+totalPickups;
        pickups++;
    }
}
