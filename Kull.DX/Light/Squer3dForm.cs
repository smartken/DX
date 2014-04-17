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

namespace Kull.DX.Light
{
    public partial class Squer3dForm : BaseGrapForm
    {
        Material mtrl;

        public Squer3dForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionNormal), 6, _device, 0, CustomVertex.PositionNormal.Format, Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.onVetexBufferCreate);
            this.onVetexBufferCreate(vertexBuffer, null);
            mtrl = new Material();
            mtrl.Diffuse = Color.Yellow;
            mtrl.Ambient = Color.Red;
        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.CullMode = Cull.Clockwise;
            _device.RenderState.ZBufferEnable = true;
            _device.RenderState.Lighting = true;
            setupLight();
        }

        protected virtual void onVetexBufferCreate(object sender,EventArgs e){
            CustomVertex.PositionNormal[] verts = (CustomVertex.PositionNormal[])vertexBuffer.Lock(0, 0);
            verts[0].Position = new Vector3(-1.0f,-1.0f,0.0f);
            verts[0].Normal = new Vector3(0,0,-1);
            verts[1].Position = new Vector3(1.0f, 1.0f, 0.0f);
            verts[1].Normal = new Vector3(0, 0, -1);
            verts[2].Position = new Vector3(1.0f, -1.0f, 0.0f);
            verts[2].Normal = new Vector3(0, 0, -1);
            verts[3].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            verts[3].Normal = new Vector3(0, 0, -1);
            verts[4].Position = new Vector3(-1.0f, 1.0f, 0.0f);
            verts[4].Normal = new Vector3(0, 0, -1);
            verts[5].Position = new Vector3(1.0f, 1.0f, 0.0f);
            verts[5].Normal = new Vector3(0, 0, -1);
            vertexBuffer.Unlock();
        }

        private void setupMatrices() {
            _device.Transform.World = Matrix.RotationY(0);
            Vector3 v1 = new Vector3(0.0f,0.0f,-5.0f);
            v1.TransformCoordinate(Matrix.RotationYawPitchRoll(angle, viewz, 0));
            _device.Transform.View = Matrix.LookAtLH(v1,new Vector3(0.0f,0.0f,0.0f),new Vector3(0.0f,1.0f,0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, 1.0f, 1.0f, 100.0f);
        }

        protected void setupLight() {
            _device.Material = mtrl;
            _device.Lights[0].Type = LightType.Directional;
            _device.Lights[0].Diffuse = Color.White;
            _device.Lights[0].Direction = new Vector3(0,-2,4);
            _device.Lights[0].Update();
            _device.Lights[0].Enabled = true;
            _device.RenderState.Ambient = Color.FromArgb(0x808080);
        }

        protected override void rendering()
        {
            base.rendering();
            setupMatrices();
            _device.SetStreamSource(0, vertexBuffer, 0);
            _device.VertexFormat = CustomVertex.PositionNormal.Format;
            _device.Transform.World = Matrix.Translation(0, 0, -1);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);

            _device.Transform.World = Matrix.RotationY((float)Math.PI) * Matrix.Translation(0, 0, 1);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);

            _device.Transform.World = Matrix.RotationY(-(float)Math.PI/2) * Matrix.Translation(1, 0, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            
            _device.Transform.World = Matrix.RotationY((float)Math.PI / 2) * Matrix.Translation(-1, 0, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            
            _device.Transform.World = Matrix.RotationX((float)Math.PI / 2) * Matrix.Translation(0, 1, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            
            _device.Transform.World = Matrix.RotationX(-(float)Math.PI / 2) * Matrix.Translation(0, -1, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            
        }
    }
}
