using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private const float BACKGROUND_FRAGMENT_DISSTANCE = 100f;

    public static BackgroundController instance;

    [SerializeField] private Player _player;
    [SerializeField] private Transform _backgroundFragment;

    private Vector3 _lastBackgroundFragmentPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _lastBackgroundFragmentPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(_lastBackgroundFragmentPos, _player.GetPosition()) < BACKGROUND_FRAGMENT_DISSTANCE)
        {
            //render BACKGROUND part
            _lastBackgroundFragmentPos = BackgroundGenerator(_lastBackgroundFragmentPos + Vector3.right * 110f);

        }
    }

    public Vector3 BackgroundGenerator(Vector3 backgroundPositon)
    {
        Instantiate(_backgroundFragment, backgroundPositon, Quaternion.identity);
        _backgroundFragment.transform.localScale = new Vector3(_backgroundFragment.transform.localScale.x * (-1), 
                                                               _backgroundFragment.transform.localScale.y, 
                                                               _backgroundFragment.transform.localScale.z);
        return backgroundPositon;
    }

}
