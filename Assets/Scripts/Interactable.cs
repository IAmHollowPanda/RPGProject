using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactRadius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;


    public virtual void Interact(){
        //Debug.Log("Interacting with " + transform.name);
    }

    void Update(){
        if(isFocus && hasInteracted==false){
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= interactRadius){
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused (Transform playerTransform){
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused(){
        isFocus=false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected() {

        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, interactRadius);   
    }
}
