using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool debug = false;

    public GameObject player = null;

    public List<Door> doors = new List<Door>();
    public Transform spawnPoint;

    public static DoorProbabilities probabilities;

    private void Start() {
        // @Todo: Find "probabilities.txt" in other places
        var ret = FileManager.LoadFile(Application.streamingAssetsPath + "/probabilities.txt");
        if(!ret.success) {
            Logger.Log($"Failed to load {Application.streamingAssetsPath + "/probabilities.txt"}");
            return;
        }

        probabilities = ret.probabilities;
        Random.InitState((int)System.DateTime.Now.ToFileTimeUtc());
        player.transform.position = spawnPoint.position;
    }

    private void Update() {
        if(debug) {
            RandomizeDoors();
            debug = false;
        }
    }

    public void RandomizeDoors() {
        DoorProbabilities.Key GetRandomType() {
            var rand = Random.Range(0.0f, 1.0f);

            var chosenType = DoorProbabilities.Key.YYY;
            for(int i = 0; i < probabilities.dict.Length; i++) {
                float p = probabilities.dict[i];
                if(rand <= p) {
                    chosenType = (DoorProbabilities.Key)i;
                    break;
                }
                rand -= p;
            }

            return chosenType;
        }

        foreach(var d in doors) {
            var type = GetRandomType();
            switch(type) {
                case DoorProbabilities.Key.YYY: d.doorFlags |= (int)Door.Flags.Hot | (int)Door.Flags.Noisy | (int)Door.Flags.Safe; break;
                case DoorProbabilities.Key.YYN: d.doorFlags |= (int)Door.Flags.Hot | (int)Door.Flags.Noisy;                        break;
                case DoorProbabilities.Key.YNY: d.doorFlags |= (int)Door.Flags.Hot                         | (int)Door.Flags.Safe; break;
                case DoorProbabilities.Key.YNN: d.doorFlags |= (int)Door.Flags.Hot;                                                break;
                case DoorProbabilities.Key.NYY: d.doorFlags |=                       (int)Door.Flags.Noisy | (int)Door.Flags.Safe; break;
                case DoorProbabilities.Key.NYN: d.doorFlags |=                       (int)Door.Flags.Noisy;                        break;
                case DoorProbabilities.Key.NNY: d.doorFlags |=                                               (int)Door.Flags.Safe; break;
                case DoorProbabilities.Key.NNN: d.doorFlags  = 0;                                                                  break;
            }

            d.ResetBasedOnFlags();
        }

    }
}
