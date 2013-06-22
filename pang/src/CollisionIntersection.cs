using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using XQUEST.GameObjectManagement;

namespace pang_01
{
    /// <summary>
    /// Enumeration of possible points of intersections
    /// in a rectangle intersection.
    /// </summary>
    public enum CollisionIntersectionPoint
    {
        None,
        Left,
        Top,
        Right,
        Bottom,
        BottomLeft,
        TopLeft,
        TopRight,
        BottomRight
    }

    /// <summary>
    /// Static class with method for determining intersection point
    /// in a rectangle intersection.
    /// </summary>
    public static class CollisionIntersection
    {
        public static CollisionIntersectionPoint GetIntersectionPoint(
          IGameObject collider,
          IGameObject collidee)
        {

            CollisionIntersectionPoint colliderDirection;
            BoundingBox bbCollider =
              new BoundingBox(
                new Vector3(collider.BoundingRectangle.X, collider.BoundingRectangle.Y,
                            0.0f),
                new Vector3(
                  collider.BoundingRectangle.X + collider.BoundingRectangle.Width,
                  collider.BoundingRectangle.Y + collider.BoundingRectangle.Height, 0.0f));

            BoundingBox bbCollidee =
              new BoundingBox(
                new Vector3(collidee.BoundingRectangle.X, collidee.BoundingRectangle.Y,
                            0.0f),
                new Vector3(
                  collidee.BoundingRectangle.X + collidee.BoundingRectangle.Width,
                  collidee.BoundingRectangle.Y + collidee.BoundingRectangle.Height, 0.0f));

            Vector3[] colliderCorners = bbCollider.GetCorners();
            Vector3[] collideeCorners = bbCollidee.GetCorners();

            // Find out which direction the collision was
            if (colliderCorners[0].Y > collideeCorners[3].Y &&
                colliderCorners[3].Y < collideeCorners[3].Y)
            {
                if (colliderCorners[0].X < collideeCorners[3].X)
                {
                    colliderDirection = CollisionIntersectionPoint.BottomRight;
                }
                else if (colliderCorners[1].X > collideeCorners[2].X)
                {
                    colliderDirection = CollisionIntersectionPoint.BottomLeft;
                }
                else
                {
                    colliderDirection = CollisionIntersectionPoint.Bottom;
                }
            }
            else if (colliderCorners[0].Y > collideeCorners[0].Y &&
                     colliderCorners[3].Y < collideeCorners[0].Y)
            {
                if (colliderCorners[0].X < collideeCorners[3].X)
                {
                    colliderDirection = CollisionIntersectionPoint.TopRight;
                }
                else if (colliderCorners[1].X > collideeCorners[1].X)
                {
                    colliderDirection = CollisionIntersectionPoint.TopLeft;
                }
                else
                {
                    colliderDirection = CollisionIntersectionPoint.Top;
                }
            }
            else
            {
                if (colliderCorners[0].X < collideeCorners[0].X)
                {
                    colliderDirection = CollisionIntersectionPoint.Right;
                }
                else
                {
                    colliderDirection = CollisionIntersectionPoint.Left;
                }
            }
            return colliderDirection;
        }
    }
}
