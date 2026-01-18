using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform spawnP1;
    public Transform spawnP2;

    void Start()
    {
        /*int p1 = CarSelectionData.Instance.player1Index;
        int p2 = CarSelectionData.Instance.player2Index;

        GameObject car1 = Instantiate(carPrefabs[p1], spawnP1.position, spawnP1.rotation);
        GameObject car2 = Instantiate(carPrefabs[p2], spawnP2.position, spawnP2.rotation);

        // призначаємо керування
        car1.GetComponent<Player>().SetControlsForPlayer(1);
        car2.GetComponent<Player>().SetControlsForPlayer(2);

        // імена для RaceManager / UI
        car1.GetComponent<PlayerRace>().playerName = "Player 1";
        car2.GetComponent<PlayerRace>().playerName = "Player 2";
        */
    }
}
