using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerControl : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Touch.activeTouches.Count > 0)
        
        if (Touch.fingers[0].isActive)
        {
            Touch myTouch = Touch.activeTouches[0];
            Vector2 screenPos = myTouch.screenPosition;
            Vector3 WorldLoc = mainCamera.ScreenToWorldPoint(screenPos);

            transform.position = new Vector3(WorldLoc.x, WorldLoc.y, 0);
        }
    }
}
