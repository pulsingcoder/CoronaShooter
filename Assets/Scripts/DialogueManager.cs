using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public AudioSource dialogueAudio;
    public Animator boxAnim;
    public GameObject endScene;
    public GameObject dialogNext;
    public Text dialogueText;
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        GetComponent<DialogueTrigger>().TriggerDialogue();
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        boxAnim.SetBool("isOpen", true);
        sentences.Clear();
       
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (!dialogueAudio.isPlaying)
        dialogueAudio.Play();
        if (sentences.Count ==0)
        {
            EndDialog();
            return;
        }
        string sentence =  sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialog()
    {
      if (endScene !=null)
        {
            endScene.GetComponent<EndScene>().End();
        }
        Debug.Log("End of sentences");
        boxAnim.SetBool("isOpen", false);
        if (dialogNext!=null)
        dialogNext.SetActive(true);
        gameObject.SetActive(false);
       
        
    }
}
