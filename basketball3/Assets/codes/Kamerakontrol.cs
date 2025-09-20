using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public Transform top;
    public Vector3 offset = new Vector3(0, 5, -10);

    void LateUpdate()
    {

        transform.position = top.position + offset;


        transform.rotation = Quaternion.Euler(30, 0, 0);
    }
}
