using System.Collections;
using UnityEngine;

public class LightShow : MonoBehaviour
{
    private GameObject[] spotlights;

    private void Start()
    {
        spotlights = GameObject.FindGameObjectsWithTag("Spotlight");
    }

    public void StartRave()
    {
        StartCoroutine("RaveFlash", 1);
    }

    IEnumerator RaveFlash(int counter)
    {
        if(counter == 5)
        {
            yield break;
        }

        foreach (GameObject go in spotlights)
        {
            go.GetComponent<Light>().intensity = 10f;
        }

        yield return new WaitForSeconds(0.2f);

        foreach (GameObject go in spotlights)
        {
            go.GetComponent<Light>().intensity = 0f;
        }

        yield return new WaitForSeconds(0.2f);

        StartCoroutine("RaveFlash", counter + 1);
    }
}
