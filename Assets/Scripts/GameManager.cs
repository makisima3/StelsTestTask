using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private SuccesView succesView;

    [SerializeField]
    private GameOverView gameOverView;

    private void Awake()
    {
        Instance = this;
    }

    public void Win()
    {
        succesView.gameObject.SetActive(true);

        succesView.Show();
    }

    public void Lose()
    {
        gameOverView.gameObject.SetActive(true);

        gameOverView.Show();
    }
}
