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
        public float duration = 0;
        float distanceTravelled;


        private void Start()
        {
            if (duration != 0)
            {
                speed = pathCreator.path.length / duration;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                SpriteAnim sa = GetComponentInChildren<SpriteAnim>();
                if (sa != null)
                {
                    float distanceForAnim = distanceTravelled;
                    float positionWithOneJump = Mathf.Repeat(distanceForAnim, pathCreator.path.length);
                    if (Mathf.Abs(positionWithOneJump)<=0.1f)
                    {

                        float positionWithTwoJumps = Mathf.Repeat(distanceForAnim, pathCreator.path.length * 2);
                        //at left, should not flip
                        if (Mathf.Abs(positionWithTwoJumps) <= 0.1f)
                        {
                            if (sa.isFliped)
                            {
                                sa.FlipAnim();
                                sa.ResetAnim();
                            }
                        }
                        else
                        {
                            if (!sa.isFliped)
                            {
                                sa.FlipAnim();
                                sa.ResetAnim();
                            }
                        }
                    }
                }

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