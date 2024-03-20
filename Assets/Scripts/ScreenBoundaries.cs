using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    public Camera mainCamera;
    private Vector2 screenBounds;
    private float playerWidth;
    private float playerHeight;


    void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of player width / 2
        playerHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of payer height / 2
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + playerWidth, screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + playerHeight, screenBounds.y - playerHeight);
        transform.position = viewPos;
    }
}
