using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    [Tooltip("Assign RaceManager from the scene")]
    [SerializeField] private RaceManager raceManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerRace pr = other.GetComponent<PlayerRace>();
        if (pr == null)
        {
            Debug.LogWarning("Player has no PlayerRace component: " + other.name);
            return;
        }

        pr.TryFinish(raceManager);
    }
}