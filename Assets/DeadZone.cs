using UnityEngine;

using KinematicCharacterController; 

public class DeadZone : MonoBehaviour
{

    public Vector3 respawnPosition = new Vector3(0f, 5f, 0f); 

    private void OnTriggerEnter(Collider other)
    {
    
        if (other.CompareTag("Player"))
        {
           
            KinematicCharacterMotor motor = other.GetComponent<KinematicCharacterMotor>();

            if (motor != null)
            {
                Debug.Log("플레이어 추락 감지! 전용 위치로 리스폰합니다.");
                
              
                motor.SetPosition(respawnPosition);
            }
        }
    }
}