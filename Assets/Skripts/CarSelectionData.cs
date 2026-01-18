using UnityEngine;

public class CarSelectionData : MonoBehaviour
{
    public static CarSelectionData Instance;

    public int player1Index;
    public int player2Index;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
