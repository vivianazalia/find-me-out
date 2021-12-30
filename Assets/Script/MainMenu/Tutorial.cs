using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<Sprite> tutorials = new List<Sprite>();
    [SerializeField] private Image currentTutorial;
    private int currentIndex = 0;

    private void Start()
    {
        currentTutorial.sprite = tutorials[currentIndex];
    }

    public void Next()
    {
        if (currentIndex < tutorials.Count - 1)
        {
            currentIndex++;
            currentTutorial.sprite = tutorials[currentIndex];
        }
    }

    public void Previous()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            currentTutorial.sprite = tutorials[currentIndex];
        }
    }
}
