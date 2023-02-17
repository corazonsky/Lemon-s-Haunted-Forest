using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InfoBox2 : MonoBehaviour
{
    public float textShowTime = 4f;
    
    void Awake()
    {
        Pickup.OnPickupGrab += ShowPickupsText;
        LevelFinish.OnCollectMoreLemon += ShowCollectMoreText;
        VillageGateTrigger.OnVillageGate += ShowVillageGateText;
        
    }


    private void OnDestroy()
    {
        Pickup.OnPickupGrab -= ShowPickupsText;
        LevelFinish.OnCollectMoreLemon -= ShowCollectMoreText;
        VillageGateTrigger.OnVillageGate -= ShowVillageGateText;
        
    }

    void ShowPickupsText()
    {
        GetComponent<TextMeshProUGUI>().text = "Lemon Collected!";
        Invoke("Delete", textShowTime);
    }

    void ShowCollectMoreText()
    {
        GetComponent<TextMeshProUGUI>().text = "The Priest requested more lemons!";
        Invoke("Delete", textShowTime);
    }

    void ShowVillageGateText()
    {
        GetComponent<TextMeshProUGUI>().text = "Welcome to the Village!";
        Invoke("Delete", textShowTime);
    }


    void Delete()
    {
        GetComponent<TextMeshProUGUI>().text = "";
    }
}
