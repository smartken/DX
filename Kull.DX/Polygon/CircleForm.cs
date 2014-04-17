using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Kull.DX.Polygon
{
    public partial class CircleForm : BaseGrapForm
    {
        public CircleForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), 100, _device, Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.onVertexBufferCreate);
            this.onVertexBufferCreate(vertexBuffer, null);
        }

        protected virtual void onVertexBufferCreate(object sender, EventArgs e) {
            CustomVertex.PositionColored[] verts = (CustomVertex.PositionColored[])vertexBuffer.Lock(0, 0);
            for (int i = 0; i < 50; i++) {
                float theta = (float)(2 * Math.PI * i) / 49;
                verts[2 * i].Position = new Vector3((float)Math.Sin(theta), -1, (float)Math.Cos(theta));
                verts[2 * i].Color = System.Drawing.Color.LightPink.ToArgb();
                verts[2 * i+1].Position = new Vector3((float)Math.Sin(theta), 1, (float)Math.Cos(theta));
                verts[2 * i + 1].Color = Color.LightPink.ToArgb();

            }
            vertexBuffer.Unlock();
        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.CullMode = Cull.None;
            _device.RenderState.Lighting = false;
        }

        protected void setupMatrices() {
            _device.Transform.World = Matrix.RotationAxis(new Vector3((float)Math.Cos(Environment.TickCount / 250.0f), 1, (float)Math.Sin(Environment.TickCount / 250f)), Environment.TickCount / 3000.0f);
            _device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 3.0f, -5.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, 1.0f, 1.0f, 100.0f);
        }

        protected override void rendering()
        {
            base.rendering();
            setupMatrices();
            _device.SetStreamSource(0,vertexBuffer,0);
            _device.VertexFormat = CustomVertex.PositionColored.Format;
            _device.DrawPrimitives(PrimitiveType.TriangleStrip,0,(4*25)-2);
        }
    }
}
