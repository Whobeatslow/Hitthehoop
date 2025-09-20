using UnityEngine;
using TMPro;

public class collider : MonoBehaviour
{
    public int skor = 0; 
    public TextMeshProUGUI skorText;
    public ParticleSystem kutlamaEfekti;
    public float efektsuresi = 2f;

    void Start()
    {
        GuncelleSkor();
    }

    private void OnTriggerEnter(Collider other)
    {

        skor++;
        kutlamaEfekti.Play();
        StartCoroutine(StopEffectAfterDuration());

        GuncelleSkor();
    }

    void GuncelleSkor()
    {
        skorText.text = "Skor: " + skor.ToString();
    }
    private System.Collections.IEnumerator StopEffectAfterDuration()
    {
        yield return new WaitForSeconds(efektsuresi); 
        kutlamaEfekti.Stop(); 
    }
}
