using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProbabilities {
    // yyy => hot = y, noisy = y, safe = y
    public enum Key {
        YYY = 0, YYN, YNY, YNN,
        NYY, NYN, NNY, NNN,
    }

    public float[] dict = new float[8];

    public float yyy { get => dict[(int)Key.YYY]; set => dict[(int)Key.YYY] = value; }
    public float yyn { get => dict[(int)Key.YYN]; set => dict[(int)Key.YYN] = value; }
    public float yny { get => dict[(int)Key.YNY]; set => dict[(int)Key.YNY] = value; }
    public float ynn { get => dict[(int)Key.YNN]; set => dict[(int)Key.YNN] = value; }
    public float nyy { get => dict[(int)Key.NYY]; set => dict[(int)Key.NYY] = value; }
    public float nyn { get => dict[(int)Key.NYN]; set => dict[(int)Key.NYN] = value; }
    public float nny { get => dict[(int)Key.NNY]; set => dict[(int)Key.NNY] = value; }
    public float nnn { get => dict[(int)Key.NNN]; set => dict[(int)Key.NNN] = value; }
}

public class Door : MonoBehaviour {
    public enum Flags {
        Hot   = 0x1,
        Noisy = 0x2,
        Safe  = 0x4,
    }

    public bool hot   { get => (doorFlags & (int)Flags.Hot)   > 0; set => doorFlags |= (int)Flags.Hot   * (value ? 1 : 0); }
    public bool noisy { get => (doorFlags & (int)Flags.Noisy) > 0; set => doorFlags |= (int)Flags.Noisy * (value ? 1 : 0); }
    public bool safe  { get => (doorFlags & (int)Flags.Safe)  > 0; set => doorFlags |= (int)Flags.Safe  * (value ? 1 : 0); }

    public int doorFlags = 0;

    [Header("Elements to use")]
    public Material normalMaterial = null;
    public Material hotMaterial = null;
    public AudioClip noise = null;

    [Header("Object references")]
    public new Renderer renderer = null;
    public AudioSource audioSource = null;

    private void Start() {
        audioSource.enabled = false;
        renderer.material = normalMaterial;
    }

    private void OnTriggerEnter(Collider other) {
        if(!audioSource.enabled) return;
        _playSound = true;
        StartCoroutine(PlaySoundCoroutine());
    }

    private void OnTriggerExit(Collider other) {
        _playSound = false;
    }

    bool _playSound = false;
    IEnumerator PlaySoundCoroutine() {
        while(_playSound) {
            audioSource.PlayOneShot(noise);
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void ResetBasedOnFlags() {
        Logger.Log($"{gameObject.name} flags: {doorFlags:x} (hot = {hot}, noisy = {noisy}, safe = {safe})");

        renderer.material = hot ? hotMaterial : normalMaterial;
        audioSource.enabled = noisy;
    }

}
