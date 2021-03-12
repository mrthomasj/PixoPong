using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float _ySpeed = 3;

    private GameManager gameManager;

    private Vector3 moveAmount;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!gameManager.gameRunning)
            return;

        float direction = Input.GetAxis("Vertical") * _ySpeed * Time.deltaTime;

        direction += transform.localPosition.y;
        ClampPosition(ref direction);

        moveAmount = new Vector3(transform.localPosition.x, direction, 0);

        transform.localPosition = moveAmount;
    }

    private void ClampPosition(ref float yPos)
    {
        float minY = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 4.5f;
        float maxY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y -4.5f;

        yPos = Mathf.Clamp(yPos, minY, maxY);

    }

}
