using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnableGroups;

    private bool active = true;

    private void Start()
    {
        Time.timeScale = 1f;
        SpawnNext();
    }

    public void SpawnNext()
    {
        if(!active)
        {
            return;
        }

        int i = Random.Range(0, spawnableGroups.Length);

        Instantiate(spawnableGroups[i], transform.position, Quaternion.identity);
    }

    public void StopSpawning()
    {
        FindObjectOfType<UIManager>().GameOver();
        active = false;
    }
}
