using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubPortal : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    public string sceneToGoTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(FadeIn());
            StartCoroutine(FadeOut(sceneToGoTo));
        }
    }
    
    IEnumerator FadeIn()
    {
        var t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            var a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }
    
    IEnumerator FadeOut(string scene)
    {
        var t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            var a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
