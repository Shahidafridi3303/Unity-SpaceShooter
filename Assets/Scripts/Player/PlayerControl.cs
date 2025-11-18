using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
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

public class PlayerControl : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;

    private float maxLeft;
    private float maxRight;
    private float maxUp;
    private float maxDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;

        // Start with Left-0, Right-1, Down-0, Up-1 
        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.12f, 0)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.88f, 0)).x;
        maxUp = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.8f)).y;
        maxDown = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.05f)).y;
    }

    // Update is called once per frame
    void Update()
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
                Mathf.Clamp(transform.position.y, maxDown, maxUp),0);
        }
    }
}
