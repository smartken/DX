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
    public partial class Triangle3dForm : BaseGrapForm
    {

        float angle = 0, viewz = -6.0f;

        public Triangle3dForm()
        {
            InitializeComponent();
        }


        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), 3, _device,0 ,CustomVertex.PositionColored.Format, Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.onVertexBufferCreate);
            this.onVertexBufferCreate(vertexBuffer, null);
        }

        protected virtual void onVertexBufferCreate(object sender,EventArgs e) {
            VertexBuffer vb = (VertexBuffer)sender;
            CustomVertex.PositionColored[] verts = (CustomVertex.PositionColored[])vb.Lock(0, 0);
            verts[0].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            verts[0].Color = Color.Aqua.ToArgb();
            verts[1].Position = new Vector3(1.0f, -1.0f, 0.0f);
            verts[1].Color = Color.Brown.ToArgb();
            verts[2].Position = new Vector3(0.0f, 1.0f, 0.0f);
            verts[2].Color = Color.LightPink.ToArgb();
            vb.Unlock();
        }

        private void SetupMatrices() {
            int iTime = Environment.TickCount % 1000;
            angle = iTime * (2.0f * (float)Math.PI) / 1000.0f;
            _device.Transform.World = Matrix.Translation(1,0,0)*Matrix.RotationY(angle);
            _device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 3.0f, viewz), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI/4,1.0f,1.0f,100.0f);
        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.CullMode = Cull.None;
            _device.RenderState.Lighting = false;
            
        }

        protected override void beforeRender()
        {
            SetupMatrices();
            base.beforeRender();
        }

        protected override void rendering()
        {
            base.rendering();
            _device.SetStreamSource(0, vertexBuffer, 0);
            _device.VertexFormat = CustomVertex.PositionColored.Format;
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

        }


   
    }
}
