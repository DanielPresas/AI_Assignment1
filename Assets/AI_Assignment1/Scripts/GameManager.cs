using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private bool _randomizeDoors = false;

    [SerializeField] private GameObject _player    = null;
    [SerializeField] private Transform _spawnPoint = null;
    [SerializeField] private Transform _deathPoint = null;

    [Space]
    [SerializeField] private List<Door> _doors = new List<Door>();
    [SerializeField] private GameObject _floor = null;

    public static DoorProbabilities probabilities;

    public static GameObject player    => get._player;
    public static Transform spawnPoint => get._spawnPoint;
    public static Transform deathPoint => get._deathPoint;

    public static List<Door> doors => get._doors;
    public static GameObject floor => get._floor;

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
        ResetGame();
    }

    private void Update() {
        if(_randomizeDoors) {
            RandomizeDoors();
            _randomizeDoors = false;
        }
    }

    public void ResetGame() {
        StopAllCoroutines();
        Random.InitState((int)System.DateTime.Now.ToFileTimeUtc());
        ResetPlayer();
        RandomizeDoors();
        floor.SetActive(true);
    }

    public static void ResetPlayer() {
        var cc = player.GetComponent<CharacterController>();
        cc.enabled = false;

        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        cc.enabled = true;

    }

    public static void PlayerDeath() {
        var cc = player.GetComponent<CharacterController>();
        cc.enabled = false;

        player.transform.position = deathPoint.position;
        player.transform.rotation = deathPoint.rotation;

        cc.enabled = true;
    }

    public static void GameOver() {
        static IEnumerator DespawnFloor() {
            yield return new WaitForSeconds(3.5f);
            floor.SetActive(false);
        }

        get.StartCoroutine(DespawnFloor());
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
