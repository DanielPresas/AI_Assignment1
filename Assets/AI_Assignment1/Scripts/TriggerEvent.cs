using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour {

    public UnityEvent<Collider> enterEvent = null;
    public UnityEvent<Collider> exitEvent = null;
    public UnityEvent<Collider> stayEvent = null;

    private void OnTriggerEnter(Collider other) {
        if(enterEvent != null) enterEvent.Invoke(other);
    }

    private void OnTriggerExit(Collider other) {
        if(exitEvent != null) exitEvent.Invoke(other);
    }

    private void OnTriggerStay(Collider other) {
        if(stayEvent != null) stayEvent.Invoke(other);
    }

}
