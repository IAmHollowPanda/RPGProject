using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    public Interactable focus;
    public LayerMask movementMask;
    public Camera camera;
    PlayerMotor motor;


    public int range = 100;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if(Input.GetMouseButtonDown(0)){
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, range, movementMask)){
                // Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);

                removeFocus();
            }
        }else if(Input.GetMouseButtonDown(1)){
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, range)){
                
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null){
                    SetFocus(interactable);
                } 
            }
        }
    }

    void SetFocus(Interactable newFocus){

        if(newFocus!=focus){
            if(focus != null)
                focus.OnDefocused();
            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        focus = newFocus;
        newFocus.OnFocused(transform);
       
        // motor.MoveToPoint(newFocus.transform.position);
    }

    void removeFocus(){

        if(focus!=null)
            focus.OnDefocused();
        focus = null;
        motor.stopFollowingTarget();
    }
}
