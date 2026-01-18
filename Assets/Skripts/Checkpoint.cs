using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    [Tooltip("Order index of this checkpoint in the lap, starting from 0")] // tooltip for field index
    public int index = 0;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var pr = other.GetComponent<PlayerRace>();
        if (pr != null)
        {
            pr.PassCheckpoint(index);
            Debug.Log($"{pr.playerName} passed checkpoint {index}");
        }
    }
}
