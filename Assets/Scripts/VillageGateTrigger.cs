using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VillageGateTrigger : MonoBehaviour
{
    public static event Action OnVillageGate;

    private void OnTriggerEnter(Collider other)
    {
        OnVillageGate?.Invoke();
    }
}
