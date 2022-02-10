using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<Door> doors = new List<Door>();

    private void Start() {
        // @Todo: Find "probabilities.txt" in other places
        var ret = FileManager.LoadFile(Application.streamingAssetsPath + "/probabilities.txt");
        if (!ret.success) {
            Logger.Log($"Failed to load {Application.streamingAssetsPath + "/probabilities.txt"}");
        }


    }

    public void RandomizeDoors() {

    }
}
