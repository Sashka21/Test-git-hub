using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [Header("Race Settings")]
    public int totalLaps = 3;

    private List<PlayerRace> players = new List<PlayerRace>();
    private bool raceFinished = false;

    private IEnumerator Start()
    {
        // ЧЕКАЄМО 1 КАДР, ЩОБ SPAWNER ВСТИГ СТВОРИТИ МАШИНКИ
        yield return null;

        players.Clear();
        players.AddRange(FindObjectsOfType<PlayerRace>());

        foreach (var p in players)
        {
            p.totalCheckpointsPerLap = FindTotalCheckpoints();
        }

        Debug.Log("RaceManager found players: " + players.Count);
    }

    int FindTotalCheckpoints()
    {
        Checkpoint[] cps = FindObjectsOfType<Checkpoint>();
        return cps.Length;
    }

    // викликається з PlayerRace коли гравець фінішує
    public void PlayerFinished(PlayerRace player)
    {
        if (raceFinished) return;

        Debug.Log(player.playerName + " FINISHED!");

        raceFinished = true;

        // тут можна показати UI переможця
        // Time.timeScale = 0f; // якщо хочеш зупинити гру
    }
}
