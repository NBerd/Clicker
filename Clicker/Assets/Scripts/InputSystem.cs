using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        #region EDITOR_CODE
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit)) 
            {
                if (raycastHit.collider.TryGetComponent(out IHitable hit))
                    hit.Hit();
            }
        }
#endif
        #endregion

        if (Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++) 
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began) 
                {
                    Ray ray = _camera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out RaycastHit raycastHit))
                    {
                        if (raycastHit.collider.TryGetComponent(out IHitable hit))
                            hit.Hit();
                    }
                }
            }
        }
    }

}