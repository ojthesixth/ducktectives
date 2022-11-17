using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class typeWriter : MonoBehaviour
{
    [SerializeField] private float writingSpeed = 45f;

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'.','!','?'}, 0.8f),
        new Punctuation(new HashSet<char>() {',',';',':'}, 0.5f)
    };


    private Coroutine typingCoroutine;


    public void Run(string textToType, TMP_Text textLabel)
    {
       typingCoroutine = StartCoroutine(routine:TypeText(textToType, textLabel));
    }


   public void Stop()
    {
    StopCoroutine(typingCoroutine);
    IsRunning = false;
    } 


    public IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {

        IsRunning = true;    

        textLabel.text = string.Empty;
        yield return new WaitForSeconds(1);

        float t = 0;
        int charIndex = 0;
        
        while (charIndex < textToType.Length)
        {

        int lastCharIndex = charIndex;

            t += Time.deltaTime * writingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(value: charIndex, min: 0, max: textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
        {
                //bool isLast = i > textToType.Substring(0, -1);

                bool isLast;
            if (i == charIndex - 1)
                {
                     isLast = true;
                }
                else
                {
                     isLast = false;
                }

            textLabel.text = textToType[..(i + 1)];

            if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
            {
                yield return new WaitForSeconds(waitTime);
            }
        }

            yield return null;
        }

        IsRunning = false;
        //textLabel.text = textToType;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

    waitTime = default;
    return false;

    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;
    

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
