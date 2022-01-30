using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler
{
    private bool mouseHoverPlayed;
    [SerializeField] private AudioSource hoverSource, clickSource;

    private void Awake()
    {
        hoverSource = GameObject.Find("HoverSource").GetComponent<AudioSource>();
        clickSource = GameObject.Find("ClickSource").GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!mouseHoverPlayed)
        {
            hoverSource.Play();
            mouseHoverPlayed = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseHoverPlayed = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        clickSource.Play();
    }
}
