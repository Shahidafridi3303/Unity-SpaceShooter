using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
//using TouchPhase = UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

/* Screen Touch locations
    ┌───────────────────────┐
    │   ● (0,1)     ● (1,1) │
    │                       │
    │        ● (0.5,0.5)    │
    │                       │
    │   ● (0,0)     ● (1,0) │
    └───────────────────────┘
*/

enum PlayerControllerType
{
    Touch,
    TouchWithOffset,
    Joystick
}

public class PlayerControl : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;

    private float maxLeft;
    private float maxRight;
    private float maxUp;
    private float maxDown;

    [SerializeField] PlayerControllerType playerTouchType;

    [Header("Joystick")]
    public Joystick joyStick;
    [SerializeField] float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // Start with Left-0, Right-1, Down-0, Up-1 
        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.12f, 0)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.88f, 0)).x;
        maxUp = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.8f)).y;
        maxDown = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.05f)).y;
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerTouchType)
        {
            case PlayerControllerType.Touch:
                HandleTouchMovement();
                break;
            case PlayerControllerType.TouchWithOffset:
                HandleTouchMovementWithOffset();
                break;
            case PlayerControllerType.Joystick:
                HandleJoytickMovement();
                break;
        }
    }

    void HandleJoytickMovement()
    {
        if (joyStick != null)
        {
            float x = joyStick.Direction.x;
            float y = joyStick.Direction.y;

            if (x != 0 || y != 0)
            {
                Vector3 dir = new Vector3(x, y, 0);
                transform.position += dir * speed * Time.deltaTime;
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxLeft, maxRight), Mathf.Clamp(transform.position.y, maxDown, maxUp), 0);
    }


    }

    private void HandleTouchMovementWithOffset()
    {
        //if (Touch.activeTouches.Count > 0)

        if (Touch.fingers[0].isActive)
        {
            Touch myTouch = Touch.activeTouches[0];
            Vector3 screenPos = myTouch.screenPosition;
            Vector3 WorldLoc = mainCamera.ScreenToWorldPoint(screenPos);

            if (Touch.activeTouches[0].phase == TouchPhase.Began)
            {
                offset = WorldLoc - transform.position;
            }
            if (Touch.activeTouches[0].phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(WorldLoc.x - offset.x, WorldLoc.y - offset.y, 0);
            }
            if (Touch.activeTouches[0].phase == TouchPhase.Stationary)
            {
                transform.position = new Vector3(WorldLoc.x - offset.x, WorldLoc.y - offset.y, 0);
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxLeft, maxRight),
                Mathf.Clamp(transform.position.y, maxDown, maxUp), 0);
        }
    }
}
