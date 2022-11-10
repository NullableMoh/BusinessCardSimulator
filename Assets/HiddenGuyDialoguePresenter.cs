using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HiddenGuyDialoguePresenter : MonoBehaviour
{
    [SerializeField][TextArea(5, 15)] List<string> dialogue;
    [SerializeField] GameObject uIContainer;
    [SerializeField] float dialogueInterval;
    [SerializeField] AudioClip dialogueAudio;
    [SerializeField] AudioSource dialogueAudioSource;

    int dialogueIndex;
    bool uIContainerIsActive, showingDialogue;
    float audioPlayTime;

    TextMeshProUGUI text;
    DialogueActivator dialogueActivator;

    private void Awake()
    {
        dialogueIndex = -2;
        audioPlayTime = 0;
        showingDialogue = false;
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
    }

    private void TryActivateDialogue()
    {
        if (showingDialogue) return;

        if(dialogueIndex == -2 && !uIContainerIsActive)
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
        else
        {
            dialogueIndex = -1;
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
