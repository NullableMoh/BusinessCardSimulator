using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePresenter : MonoBehaviour
{
    [SerializeField] bool loopDialogue, fadeDialogueWithTime;
    [SerializeField][TextArea(5, 15)] List<string> dialogue;
    [SerializeField] GameObject uIContainer;
    [SerializeField] float dialogueInterval, dialogueFadeInterval, dialogueFadeAmount, dialogueFadeWaitTime;
    [SerializeField] AudioClip dialogueAudio;
    [SerializeField] AudioSource dialogueAudioSource;

    int dialogueIndex;
    bool uIContainerIsActive, showingDialogue;
    float audioPlayTime, initialDialogueInterval, timeTillFadeStarts;

    TextMeshProUGUI text;
    DialogueActivator dialogueActivator;

    private void Awake()
    {
        dialogueIndex = -2;
        audioPlayTime = 0;
        showingDialogue = false;
        initialDialogueInterval = dialogueInterval;
    }

    private void OnEnable()
    {
        dialogueActivator = FindObjectOfType<DialogueActivator>();
        dialogueActivator.OnTryActivateDialogue += TryActivateDialogue;
    }

    private void OnDisable()
    {
        dialogueActivator.OnTryActivateDialogue -= TryActivateDialogue;
    }


    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        UIContainerSetActive(false);

        StartCoroutine(FadeDialogueWithTime());
    }

    private void TryActivateDialogue(GameObject obj)
    {
        if (obj != gameObject) return;

        if (showingDialogue)
        {
            dialogueInterval = 0;
        }
        else
        {
            if (dialogueIndex == -2 && !uIContainerIsActive)
            {
                UIContainerSetActive(true);
                dialogueIndex = 0;
            }
            else if (dialogueIndex == -1)
            {
                UIContainerSetActive(false);
                dialogueIndex++;
                return;
            }
            else if (dialogueIndex == 0 && !uIContainerIsActive)
            {
                UIContainerSetActive(true);
            }

            StartCoroutine(SlowlyShowDialogue());
        }
    }

    IEnumerator SlowlyShowDialogue()
    {
        showingDialogue = true;
        
        StartCoroutine(PlayDialogueAudio());

        var fullDialogueText = dialogue[dialogueIndex];
        for(int i = 0; i <= fullDialogueText.Length; i++)
        {
            text.text = fullDialogueText.Substring(0, i);
            yield return new WaitForSeconds(dialogueInterval);
        }

        showingDialogue = false;

        if (dialogueIndex < dialogue.Count - 1)
        {
            dialogueIndex++;
        }
        else if(loopDialogue)
        {
            dialogueIndex = -1;
        }

        dialogueInterval = initialDialogueInterval;
        timeTillFadeStarts = Time.time + dialogueFadeWaitTime;
    }

    IEnumerator FadeDialogueWithTime()
    {
        while (true)
        {
            if(!showingDialogue && Time.time >= timeTillFadeStarts)
            {
                if (uIContainer.GetComponent<CanvasGroup>().alpha - dialogueFadeAmount >= 0)
                {
                    uIContainer.GetComponent<CanvasGroup>().alpha -= dialogueFadeAmount;
                }
                else
                {
                    uIContainer.GetComponent<CanvasGroup>().alpha = 0;
                }
                yield return new WaitForSeconds(dialogueFadeInterval);
            }
            else
            {
                uIContainer.GetComponent<CanvasGroup>().alpha = 1;
                yield return new WaitForSeconds(dialogueFadeInterval);
            }
        }
    }

    IEnumerator PlayDialogueAudio()
    {
        while (showingDialogue)
        {
            PlayAudio(dialogueAudio);
            yield return new WaitForSeconds(dialogueInterval);
        }
        dialogueAudioSource.Stop();
    }

    void PlayAudio(AudioClip clip)
    {
        if(dialogueAudioSource.clip != clip || Time.time > audioPlayTime)
        {
            audioPlayTime = Time.time + clip.length;
            dialogueAudioSource.clip = clip;
            dialogueAudioSource.Play();
        }
    }

    void UIContainerSetActive(bool state)
    {
        uIContainerIsActive = state;
        uIContainer.SetActive(state);
    }
}
