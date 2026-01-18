using UnityEngine;

public class PlayersSetup : MonoBehaviour
{
    public Player player1;
    public Player player2;

    void Start()
    {
        player1.SetControlsForPlayer(1); // WASD
        player2.SetControlsForPlayer(2); // стрелки
    }
}
