using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetPlayerStats()
    {

    }

    private void Start()
    {
        //StartCoroutine(LoadBossScene());
    }

    IEnumerator LoadBossScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BossScene");
    }
}
