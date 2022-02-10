using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private bool _randomizeDoors = false;

    [SerializeField] private GameObject _player = null;
    [SerializeField] private Transform _spawnPoint;

    [Space]
    [SerializeField] private List<Door> _doors = new List<Door>();

    public static DoorProbabilities probabilities;

    public static GameObject player    => get._player;
    public static Transform spawnPoint => get._spawnPoint;
    public static List<Door> doors     => get._doors;

    private static GameManager get;

    private void Awake() {
        if(get != null) {
            gameObject.SetActive(false);
        }
        get = this;
    }

    private void Start() {
        // @Todo: Find "probabilities.txt" in other places
        var ret = FileManager.LoadFile(Application.streamingAssetsPath + "/probabilities.txt");
        if(!ret.success) {
            Logger.Log($"Failed to load {Application.streamingAssetsPath + "/probabilities.txt"}");
            return;
        }

        probabilities = ret.probabilities;
        Random.InitState((int)System.DateTime.Now.ToFileTimeUtc());
        ResetPlayer();
        RandomizeDoors();
    }

    private void Update() {
        if(_randomizeDoors) {
            RandomizeDoors();
            _randomizeDoors = false;
        }
    }

    public static void ResetPlayer() {
        var cc = player.GetComponent<CharacterController>();
        cc.enabled = false;

        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        cc.enabled = true;
    }

    public static void RandomizeDoors() {
        static DoorProbabilities.Key GetRandomType() {
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
