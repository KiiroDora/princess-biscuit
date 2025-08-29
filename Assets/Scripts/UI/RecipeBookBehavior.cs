using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] pages;
    [SerializeField] private TextMeshProUGUI textbox;

    private int currentPageIndex = 0;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;


    void Start()
    {
        textbox.text = pages[currentPageIndex].text;

        if (currentPageIndex == pages.Length - 1)
        {
            nextButton.interactable = false;
        }

        if (currentPageIndex > 0)
        {
            prevButton.interactable = true;
        }

        if (currentPageIndex < pages.Length - 1)
        {
            nextButton.interactable = true;
        }

        if (currentPageIndex == 0)
        {
            prevButton.interactable = false;
        }
    }


    public void GoToNextPage()
    {
        currentPageIndex++;

        if (currentPageIndex == pages.Length - 1)
        {
            nextButton.interactable = false;
        }

        if (currentPageIndex > 0)
        {
            prevButton.interactable = true;
        }

        textbox.text = pages[currentPageIndex].text;
    }


    public void GoToPreviousPage()
    {
        currentPageIndex--;

        if (currentPageIndex < pages.Length - 1)
        {
            nextButton.interactable = true;
        }

        if (currentPageIndex == 0)
        {
            prevButton.interactable = false;
        }

        textbox.text = pages[currentPageIndex].text;
    }
}
