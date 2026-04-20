
using UnityEngine;

namespace EMU.DT.Simulation
{
    public class EMUController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private Transform[] trackPoints;
        
        private int currentPoint = 0;
        private bool isMoving = false;
        
        public void StartMovement()
        {
            isMoving = true;
        }
        
        public void StopMovement()
        {
            isMoving = false;
        }
        
        private void Update()
        {
            if (!isMoving || trackPoints.Length == 0)
            {
                return;
            }
            
            var target = trackPoints[currentPoint].position;
            var direction = (target - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(target);
            
            if (Vector3.Distance(transform.position, target) &lt; 0.1f)
            {
                currentPoint = (currentPoint + 1) % trackPoints.Length;
            }
        }
    }
}
