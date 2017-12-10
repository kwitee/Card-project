using CardProject.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace CardProject.Cards
{
    public class AnimationQueue : Singleton<AnimationQueue>
    {
        [SerializeField]
        private float moveDistanceImprecision = 0.01f;

        [SerializeField]
        private float moveVelocity = 1.5f;

        private List<Animation> animationQueue = new List<Animation>();

        public void AddAnimation(Animation animation)
        {
            animationQueue.RemoveAll(a => a.Object == animation.Object && a.Skipable); 
            animationQueue.Add(animation);
        }

        private void Update()
        {            
            animationQueue.RemoveAll(animation => animation.Object == null);
            var toRemove = new List<Animation>();

            foreach (var currentAnimation in animationQueue)
            {
                var currentTransform = currentAnimation.Object.transform;
                currentAnimation.StartTime = Time.time;

                if (Vector3.Distance(currentTransform.position, currentAnimation.Position) <= moveDistanceImprecision)
                {
                    if (currentAnimation.RotateAfter)
                        currentTransform.Rotate(0, 180, 0);

                    toRemove.Add(currentAnimation);
                }
                else
                {
                    if (currentAnimation.RotateBefore && !currentAnimation.RotatedBefore)
                    {
                        currentTransform.Rotate(0, 180, 0);
                        currentAnimation.RotatedBefore = true;
                    }

                    var fracMove = ((Time.time - currentAnimation.StartTime.Value) * moveVelocity);
                    currentTransform.position = Vector3.Lerp(currentTransform.position, currentAnimation.Position, fracMove);
                }

                if (!currentAnimation.Concurent)
                    break;
            }

            foreach (var remove in toRemove)
            {
                if (remove.DestroyAfter)
                    Destroy(remove.Object);
            }

            animationQueue.RemoveAll(animation => toRemove.Contains(animation));
        }
    }

    public class Animation
    {
        public GameObject Object { get; private set; }
        public Vector3 Position { get; private set; }
        public bool RotateBefore { get; private set; }
        public bool RotatedBefore { get; set; }
        public bool RotateAfter { get; private set; }
        public bool Concurent { get; private set; }
        public bool Skipable { get; private set; }
        public bool DestroyAfter { get; private set; }
        
        private float? startTime;
        public float? StartTime
        {
            get { return startTime; }
            set
            {
                if (startTime == null)
                    startTime = value;
            }
        }
       
        public Animation(GameObject gameObject, Vector3 position, bool rotateBefore, bool rotateAfter, bool concurent, bool skipable, bool destroyAfter = false)
        {
            Object = gameObject;
            Position = position;
            RotateBefore = rotateBefore;
            RotateAfter = rotateAfter;
            Concurent = concurent;
            Skipable = skipable;
            DestroyAfter = destroyAfter;
        }        
    }
}