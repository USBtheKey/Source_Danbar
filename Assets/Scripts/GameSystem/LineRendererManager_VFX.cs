using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager_VFX : MonoBehaviour
{
    [SerializeField] private GameObject[] objs = null;

    [SerializeField] private float delayBetweenEachRenderer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartProceedure());
    }


    private IEnumerator StartProceedure()
    {

        foreach(GameObject obj in objs)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(delayBetweenEachRenderer);
        }
        yield break;
    }
}
