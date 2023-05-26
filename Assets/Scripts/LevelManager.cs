using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int numberOfPlatforms;
    [SerializeField] private DoodleController player;

    private float _playerJumpHeight;
    private List<GameObject> _platforms = new List<GameObject>();
    
    // 1 najprv spawnut prvych 10 platforiem do okna bez toho aby sa touchovali
    // 2 kontrolovat ci nejaka nespadla pod obrazovku a ak hej tak ju presunut nad nech postupne spadne
    private void Start()
    {
        var size = platformPrefab.GetComponent<Platform>().Size;
        Instantiate(platformPrefab, transform.position, transform.rotation);
        int minX = 0;
        //int maxX = Screen.width - platformPrefab.GetComponent<>;
        
        //_playerJumpHeight = player.GetJumpHeight();
        //_platforms.Add(Instantiate(platformPrefab, ));
    }
    
    private void Update()
    {
        
    }
}
