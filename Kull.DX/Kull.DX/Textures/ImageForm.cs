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

namespace Kull.DX.Textures
{
    public partial class ImageForm : BaseGrapForm
    {

        Texture texture; 

        public ImageForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionTextured), 6, _device, 0, CustomVertex.PositionTextured.Format, Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.onVertexBufferCreate);
            this.onVertexBufferCreate(vertexBuffer, null);
            texture = TextureLoader.FromFile(_device,Application.StartupPath+@"\..\..\0030.jpg");
        }

        protected virtual void onVertexBufferCreate(object sender, EventArgs e) {
            CustomVertex.PositionTextured[] verts = (CustomVertex.PositionTextured[])vertexBuffer.Lock(0,0);
            verts[0].Position = new Vector3(-2.0f,-2.0f,2.0f);
            verts[0].Tu = 0.0f;
            verts[0].Tv = 10.0f;
            verts[1].Position = new Vector3(-2.0f, 2.0f, 2.0f);
            verts[1].Tu = 0.0f;
            verts[1].Tv = 0.0f;
            verts[2].Position = new Vector3(2.0f, 2.0f, 2.0f);
            verts[2].Tu = 10.0f;
            verts[2].Tv = 0.0f;
            verts[3].Position = new Vector3(-2.0f, -2.0f, 2.0f);
            verts[3].Tu = 0.0f;
            verts[3].Tv = 10.0f;
            verts[4].Position = new Vector3(2.0f, 2.0f, 2.0f);
            verts[4].Tu = 10.0f;
            verts[4].Tv = 0.0f;
            verts[5].Position = new Vector3(2.0f, -2.0f, 2.0f);
            verts[5].Tu = 10.0f;
            verts[5].Tv = 10.0f;
            vertexBuffer.Unlock();
        }

        protected void setupMatrices() {
            _device.Transform.World = Matrix.RotationY(0);
            _device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, -4.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, 1.0f, 1.0f, 100.0f);
        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.CullMode = Cull.None;
            _device.RenderState.Lighting = false;
            setupMatrices();
        }

        protected override void rendering()
        {
            base.rendering();
            _device.SetTexture(0, texture);
            _device.SetStreamSource(0, vertexBuffer, 0);
            _device.VertexFormat = CustomVertex.PositionTextured.Format;
            _device.DrawPrimitives(PrimitiveType.TriangleList,0,2);
        }

    }
}
