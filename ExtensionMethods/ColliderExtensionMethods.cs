using UnityEngine;

namespace MUnityUtils
{
    public static class ColliderExtensionMethods
    {
        /// <summary>
        /// Check if capsule collider is grounded
        /// </summary>
        /// <param name="collider">Capsule collider</param>
        /// <param name="slopeLimit">Max slope limit</param>
        /// <returns>True if collider is grounded</returns>
        public static bool IsGrounded(this CapsuleCollider collider, float slopeLimit = 45f)
        {
            var isGrounded = false;
            var colliderTransform = collider.transform;
            var capsuleHeight = Mathf.Max(collider.radius * 2f, collider.height);
            var capsuleBottom = colliderTransform.TransformPoint(collider.center - Vector3.up * capsuleHeight / 2f);
            var radius = colliderTransform.TransformVector(collider.radius, 0f, 0f).magnitude;
            var up = colliderTransform.up;
            var ray = new Ray(capsuleBottom + up * .01f, -up);
            if (Physics.Raycast(ray, out var hit, radius * 5f))
            {
                var normalAngle = Vector3.Angle(hit.normal, colliderTransform.up);
                if (normalAngle < slopeLimit)
                {
                    var maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                    if (hit.distance < maxDist)
                       isGrounded = true;
                }
            }
            return isGrounded;
        }
        /// <summary>
        /// Check if sphere collider is grounded
        /// </summary>
        /// <param name="collider">Sphere collider</param>
        /// <param name="slopeLimit">Max slope limit</param>
        /// <returns>True if collider is grounded</returns>
        public static bool IsGrounded(this SphereCollider collider, float slopeLimit = 45f)
        {
            var isGrounded = false;
            var colliderTransform = collider.transform;
            var sphereBottom = colliderTransform.TransformPoint(collider.center - Vector3.up * collider.radius);
            var radius = colliderTransform.TransformVector(collider.radius, 0f, 0f).magnitude;
            var up = colliderTransform.up;
            var ray = new Ray(sphereBottom + up * .01f, -up);
            if (Physics.Raycast(ray, out var hit, radius * 5f))
            {
                var normalAngle = Vector3.Angle(hit.normal, colliderTransform.up);
                if (normalAngle < slopeLimit)
                {
                    var maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                    if (hit.distance < maxDist)
                        isGrounded = true;
                }
            }
            return isGrounded;
        }

        public static MaxMousePosition IsOffScreen(this Collider collider, Camera camera = null)
        {
            if (camera == null) camera = Camera.main;
            var maxPos = new MaxMousePosition();
            var position = collider.transform.position;
            var boundLeft = camera.WorldToScreenPoint(new Vector3(collider.bounds.min.x, position.y, position.z)).x;
            var boundRight = camera.WorldToScreenPoint(new Vector3(collider.bounds.max.x,position.y,position.z)).x;
            var distMeshBoundLeft = Input.mousePosition.x - boundLeft;
            var distMeshBoundRight = boundRight - Input.mousePosition.x;
            maxPos.MaxLeft = distMeshBoundLeft / Screen.width;
            maxPos.MaxRight = (Screen.width - distMeshBoundRight) / Screen.width;
            return maxPos;
        }
    }
    public struct MaxMousePosition
    {
        public float MaxLeft;
        public float MaxRight;
    }
}
