using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class QuadDrawer {
        public QuadDrawer(GraphicsDevice device) {
            this.device = device;
            effect = new BasicEffect(device);
            vertices = new VertexPositionNormalTexture[4];
            indices = new short[6];

            Vector2 center = new Vector2();
            center.X = device.Viewport.Width / 2;
            center.Y = device.Viewport.Height / 2;

            effect.LightingEnabled = false;
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(new Vector3(center, 0), new Vector3(center, 1), new Vector3(0, -1, 0));
            //effect.Projection = Matrix.CreateOrthographic(center.X * 2, center.Y * 2, -0.5f, 1);
            effect.Projection = Matrix.CreatePerspective(device.Viewport.Width, device.Viewport.Height, 1, 2000.0f);
            effect.TextureEnabled = true;

            vertices[0].Normal = new Vector3(0, 0, 1);
            vertices[1].Normal = new Vector3(0, 0, 1);
            vertices[2].Normal = new Vector3(0, 0, 1);
            vertices[3].Normal = new Vector3(0, 0, 1);
            vertices[0].TextureCoordinate = new Vector2(0, 0);
            vertices[1].TextureCoordinate = new Vector2(1, 0);
            vertices[2].TextureCoordinate = new Vector2(1, 1);
            vertices[3].TextureCoordinate = new Vector2(0, 1);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 3;
            indices[3] = 1;
            indices[4] = 2;
            indices[5] = 3;
        }

        public void Draw(Texture2D texture, Vector3 topLeft, Vector3 topRight, Vector3 bottomRight, Vector3 bottomLeft) {
            effect.Texture = texture;

            vertices[0].Position = topLeft;
            vertices[1].Position = topRight;
            vertices[2].Position = bottomRight;
            vertices[3].Position = bottomLeft;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,  // primitive type
                    vertices, 0, 4,              // vertex data
                    indices, 0, 2);              // index data
            }
        }

        private GraphicsDevice device;
        private BasicEffect effect;
        private VertexPositionNormalTexture[] vertices;
        private short[] indices;
    }
}
