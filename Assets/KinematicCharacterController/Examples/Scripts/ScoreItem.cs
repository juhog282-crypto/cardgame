using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    public int scoreValue = 10; 
    public float rotationSpeed = 100f; 

    void Update()
    {
        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
           
            GameManager.Instance.AddScore(scoreValue);

          
            
            Destroy(gameObject);
        }
    }
}