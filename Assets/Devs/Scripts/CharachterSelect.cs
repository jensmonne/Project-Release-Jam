using UnityEngine;
using UnityEngine.InputSystem;

public class CharachterSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] PlayerJoinSquares;
    [SerializeField] private PlayerInputManager playerInputManager;
    private int playerCount = 0;
    private readonly Color[] playerColors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow };

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        GameObject player = playerInput.gameObject;
        player.transform.position = new Vector2(PlayerJoinSquares[playerCount].transform.position.x, (PlayerJoinSquares[playerCount].transform.position.y) + 5f);
        player.GetComponent<Renderer>().material.color = playerColors[playerCount];
        player.GetComponent<Player>().SpawnCursor(playerColors[playerCount]);
        playerCount++;
    }

}
