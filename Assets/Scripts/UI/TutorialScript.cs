using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    void Awake()
    {
        GameController.instance.PauseGame();
    }

    public void NextImage(Image image)
    {
        StartCoroutine(FadeImage(image));
    }

    IEnumerator FadeImage(Image image)
    {
        float newAlpha = image.color.a;
        while (image.color.a > 0)
        {
            newAlpha -= 0.05f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a: newAlpha);
            yield return new WaitForSecondsRealtime(0.03f);
        }

        image.gameObject.SetActive(false);
    }
}
