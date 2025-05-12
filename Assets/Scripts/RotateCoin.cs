using System.Collections;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Rotate( new Vector3(90, 180, 90), 2f, 120));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Rotate(Vector3 angle, float duration, int numberOfSteps)
    {
        while (true)
        {
            yield return new WaitForSeconds(duration / numberOfSteps);
            transform.Rotate(angle / numberOfSteps);
        }
    }
}
