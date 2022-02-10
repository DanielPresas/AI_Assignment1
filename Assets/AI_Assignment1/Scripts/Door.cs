using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoorProbabilities {
    // yyy => hot = y, noisy = y, safe = y
    public float yyy, yyn, yny, ynn;
    public float nyy, nyn, nny, nnn;
}

public class Door : MonoBehaviour {
    public bool hot = false;
    public bool noisy = false;
    public bool safe = false;

    [Space]
    public Material doorMaterial = null;
    public AudioClip noise = null;


}
