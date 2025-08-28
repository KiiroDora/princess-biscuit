using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionNumberSetter : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI versionText = GetComponent<TextMeshProUGUI>();

        versionText.text = "Version " + Application.version;
    }
}
