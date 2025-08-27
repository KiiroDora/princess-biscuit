using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitPointBarBehavior : MonoBehaviour
{
    private Scrollbar scrollbar;
    private TextMeshProUGUI text;

    void Awake()
    {
        scrollbar = gameObject.GetComponent<Scrollbar>();
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }


    public void UpdateHitPointBar(Tower tower)
    {
        scrollbar.size = (float)tower.hitPoints / tower.hitPointsMax;
        text.text = tower.hitPoints + "/" + tower.hitPointsMax; 
    }
}
