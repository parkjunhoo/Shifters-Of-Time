using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TypingText : MonoBehaviour
{
    TextMeshProUGUI text;
    string input;

    [SerializeField]
    bool cycle = false;

    [SerializeField]
    float speed = 0.2f;

    bool coroutineDoing = false;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        input = text.text;
    }

    private void FixedUpdate()
    {
        if (cycle && coroutineDoing == false)
        {
            StartCoroutine(Typing());
        }
    }

    IEnumerator Typing()
    {
        coroutineDoing = true;
        for (int i = 0; i < input.Length; i++)
        {
            text.text = input.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        coroutineDoing = false;
    }
}
