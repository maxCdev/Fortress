using UnityEngine;
using System.Collections;

public class BuildPanelController : MonoBehaviour {
    public GameObject panel;
    public void ShowHidePanels()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }
}
