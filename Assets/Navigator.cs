using UnityEngine;
using UnityEngine.UI;

public class Navigator : MonoBehaviour
{
    [SerializeField] private Image _arrowSprite;
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        NavigatePlayer();
    }
    private void NavigatePlayer()
    {
        if (_player.CurrentDestination != null)
        {
            _arrowSprite.enabled = true;
            Follow();
        }
        else
        {
            _arrowSprite.enabled = false;
        }
    }
    private void Follow()
    {
        Vector3 targetDir = _player.CurrentDestination.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0f, 0f, 1f));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 15f);
    }
}
