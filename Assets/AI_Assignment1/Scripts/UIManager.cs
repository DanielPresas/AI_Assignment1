using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public RectTransform recentFilePanel = null;
    public GameObject recentFilePrefab   = null;

    public List<string> recentFiles = new List<string>();

    private void Start() {
        recentFiles.Insert(0, $"{Application.streamingAssetsPath}/probabilities.txt");

        foreach(var rf in recentFiles) {
            var rfButton = Instantiate(recentFilePrefab);
            rfButton.transform.SetParent(recentFilePanel);
            rfButton.GetComponentInChildren<TextMeshProUGUI>().text = rf;
            rfButton.GetComponentInChildren<Button>().onClick.AddListener(() => {
                StartWithRecentFile(rfButton.GetComponentInChildren<TextMeshProUGUI>().text);
            });
        }
    }

    public void StartWithRecentFile(string filepath) {
        if(string.IsNullOrEmpty(filepath)) {
            Logger.Error("Not a valid filepath!");
            return;
        }

        GameManager.probabilitiesFilepath = filepath;
        SceneManager.LoadScene(1);
    }
}
