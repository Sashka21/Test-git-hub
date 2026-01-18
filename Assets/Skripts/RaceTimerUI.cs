using UnityEngine;
using TMPro;

public class RaceTimerUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI lapText;

    private PlayerRace playerRace;
    private float startTime;
    private bool running;

    // вызывается ИЗ PlayerRace
    public void Init(PlayerRace race)
    {
        playerRace = race;

        if (playerNameText != null)
            playerNameText.text = race.playerName;

        startTime = Time.time;
        running = true;
    }

    void Update()
    {
        if (!running || playerRace == null) return;

        UpdateTimer();
        UpdateLap();
    }

    void UpdateTimer()
    {
        float elapsed = Time.time - startTime;

        int min = Mathf.FloorToInt(elapsed / 60f);
        float sec = elapsed % 60f;

        timerText.text = $"{min:00}:{sec:00.00}";
    }

    void UpdateLap()
    {
        if (playerRace.RaceManager == null) return;

        int currentLap = Mathf.Clamp(
            playerRace.completedLaps + 1,
            1,
            playerRace.RaceManager.totalLaps
        );

        lapText.text = $"Lap: {currentLap}/{playerRace.RaceManager.totalLaps}";
    }

    public void StopTimer()
    {
        running = false;
    }
}
