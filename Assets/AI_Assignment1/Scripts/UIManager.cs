using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public RectTransform recentFilePanel = null;
    public GameObject recentFilePrefab   = null;

    public List<string> recentFiles = new List<string>();

    public string customFilepath { get; set; }

    private void Start() {
        recentFiles.Insert(0, $"{Application.streamingAssetsPath}/probabilities.txt");

        foreach(var rf in recentFiles) {
            var rfButton = Instantiate(recentFilePrefab);
            rfButton.transform.SetParent(recentFilePanel);
            rfButton.GetComponentInChildren<TextMeshProUGUI>().text = rf;
            rfButton.GetComponentInChildren<Button>().onClick.AddListener(() => {
                StartWithFile(rfButton.GetComponentInChildren<TextMeshProUGUI>().text);
            });
        }
    }

    public void StartWithFile(string filepath) {
        if(string.IsNullOrEmpty(filepath)) {
            Logger.Error("Failed to load probabilities because filepath is empty or null!");
            return;
        }

        if(!File.Exists(filepath)) {
            Logger.Error($"Failed to load probabilities because filepath doesn't exist!\nPath: {filepath}");
            return;
        }

        GameManager.probabilitiesFilepath = filepath;
        SceneManager.LoadScene(1);
    }

    public void StartWithCustomFilepath() {
        StartWithFile(customFilepath);
    }
}
