using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField]
    GameObject playersSet;
    [SerializeField]
    GameObject EnemysSet;
    [SerializeField]
    GameObject playerFireAreaSet;
    [SerializeField]
    GameObject EnemyAreaSet;

    //Enbaling GameScene
    private void Start()
    {
        playerFireAreaSet.SetActive(true);
        EnemysSet.SetActive(true);
        EnemyAreaSet.SetActive(true);
        SpwanPlayerList();
    }
    //For Instaiate players after 3 sec
    private async void SpwanPlayerList()
    {
        await new WaitForSeconds(0.3f);
        playersSet.SetActive(true);

    }
}
