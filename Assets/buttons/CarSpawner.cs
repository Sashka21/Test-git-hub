using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // масив префабів машин

    void Start()
    {
        // Отримуємо обрану машину з PlayerPrefs
        int id = PlayerPrefs.GetInt("SelectedCar", 0);

        // Створюємо об'єкт машини на сцені
        Instantiate(carPrefabs[id], transform.position, Quaternion.identity);
    }
}
