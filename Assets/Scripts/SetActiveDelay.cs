using System.Collections;
using UnityEngine;

public class SetActiveDelay : MonoBehaviour
{
    [SerializeField] byte delayTime = 2;
    [SerializeField] GameObject inactiveGameObject;

    void Start()
    {
        if (inactiveGameObject != null)
            StartCoroutine(ActivateGameObject());
    }

    IEnumerator ActivateGameObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            inactiveGameObject.SetActive(true);
        }
    }
}
