using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentRefresher : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    private async void OnEnable()
    {
        if (navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();

        await Task.Delay(100);

        if (navMeshAgent == null)
            return;

        bool wasEnabled = navMeshAgent.enabled;

        navMeshAgent.enabled = false;
        navMeshAgent.enabled = wasEnabled;
    }
}
