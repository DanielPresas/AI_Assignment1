using UnityEngine;

public class DeathTrigger : MonoBehaviour {
    public void Trigger() {
        GameManager.GameOver();
    }
}
