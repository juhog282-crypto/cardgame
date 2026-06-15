using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 목표 지점 도달 시 게임 매니저의 클리어 호출
            GameManager.Instance.StageClear();
        }
    }
}