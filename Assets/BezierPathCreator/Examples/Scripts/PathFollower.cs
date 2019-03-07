using UnityEngine;

namespace PathCreation.Examples
{

    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public bool applyRotation = true;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        bool isPaused;


        private void Start()
        {
            transform.position = pathCreator.path.GetPointAtDistance(0, endOfPathInstruction);
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume(bool isFlipped)
        {
            distanceTravelled = isFlipped ? pathCreator.path.length: 0;
            isPaused = false;
        }

        void Update()
        {
            if (isPaused)
            {
                return;
            }
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                if (applyRotation)
                {
                    transform.rotation = pathCreator.path.GetRotationAtDistance2D(distanceTravelled, endOfPathInstruction);
                }
                //transform.right = target.position - transform.position;
            }
        }
    }
}