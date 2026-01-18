using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class PlayerRace : MonoBehaviour
{

    [SerializeField] private RaceTimerUI raceTimer;
    [SerializeField] private RaceManager raceManager;

    public RaceManager RaceManager => raceManager;


    [Header("Identity")]
    public string playerName = "Player";

    [Header("Race settings")]
    public int totalCheckpointsPerLap = 0; // set by inspector or RaceManager

    // runtime
    public int nextCheckpointIndex = 0; // checkpoint expected next (0..N-1)
    public int completedLaps = 0;
    public bool IsFinished { get; private set; } = false;
    public float finishTime { get; private set; }

    // flag: the player has passed all checkpoints in the current lap (waiting for the finish)
    private bool allCheckpointsPassed = false;

    // called from Checkpoint when player crosses a checkpoint trigger
    public void PassCheckpoint(int index)
    {
        if (IsFinished) return;

        // Only accept checkpoint if it matches expected next
        if (index != nextCheckpointIndex)
        {
            // wrong checkpoint -> ignore
            return;
        }

        // correct checkpoint: advance expectation
        nextCheckpointIndex++;

        // Если дошли до последнего чекпоинта (то есть next == total)
        if (nextCheckpointIndex >= totalCheckpointsPerLap)
        {
            // Не увеличиваем completedLaps здесь!
            // Вместо этого отмечаем, что все чекпоинты пройдены и ждём пересечения финиша.
            nextCheckpointIndex = 0;
            allCheckpointsPassed = true;
            Debug.Log($"{playerName} passed all checkpoints for the lap (ready to finish).");
        }
        else
        {
            Debug.Log($"{playerName} passed checkpoint {index} (next expected {nextCheckpointIndex}).");
        }
    }

    private void Start()
    {
        if (raceTimer != null)
        {
            raceTimer.Init(this);
        }
    }

    // Called by FinishLineTrigger when player crosses finish line.
    // raceManager passed explicitly (variant A).
    public void TryFinish(RaceManager raceManager)
    {
        if (IsFinished) return;

        // Require that player passed all checkpoints this lap before finishing
        if (allCheckpointsPassed)
        {
            // complete the lap now
            completedLaps++;

            nextCheckpointIndex = 0;

            //raceTimer?.SetLap(completedLaps + 1);
            allCheckpointsPassed = false;

            Debug.Log($"{playerName} completed a lap ({completedLaps}/{RaceManager.totalLaps}) by crossing finish.");

            if (completedLaps >= RaceManager.totalLaps)
            {
                Finish(raceManager);
            }
        }
        else
        {
            Debug.Log($"{playerName} tried to finish but hasn't passed all checkpoints yet (completedLaps={completedLaps}, required={RaceManager.totalLaps}).");
        }
    }

    private void Finish(RaceManager raceManager)
    {
        if (IsFinished) return;
        IsFinished = true;
        finishTime = Time.time;
        if (raceManager != null)
            raceManager.PlayerFinished(this);
        else
            Debug.LogWarning("TryFinish called but raceManager is null");

        raceTimer?.StopTimer();
    }

    // Optional helper to reset state (useful for restarting)
    public void ResetRaceState()
    {
        nextCheckpointIndex = 0;
        completedLaps = 0;
        IsFinished = false;
        finishTime = 0f;
        allCheckpointsPassed = false;
    }
}