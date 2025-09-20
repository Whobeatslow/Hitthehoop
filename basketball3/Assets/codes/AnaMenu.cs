using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
       
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        
        Application.Quit();
        Debug.Log("Oyun kapatıldı."); 
    }
}