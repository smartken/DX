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
    public partial class Squer3dForm : BaseGrapForm
    {
        protected IndexBuffer indexBuffer;

        public Squer3dForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            indexBuffer = new IndexBuffer(typeof(int), 36, _device, 0, Pool.Default);
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), 20, _device, 0,CustomVertex.PositionColored.Format, Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.onVertexBufferCreate);
            indexBuffer.Created += new System.EventHandler(this.onIndexBufferCreate);
            this.onVertexBufferCreate(vertexBuffer,null);
            this.onIndexBufferCreate(indexBuffer, null);

        }

        protected void onIndexBufferCreate(object sender, EventArgs e) {
            int[] index = { 4, 5, 6, 5, 7, 6, 5, 1,
                            7, 7, 1, 3, 4, 0, 1, 4,
                            1,5,2, 0, 4, 2, 4, 6, 
                            3, 1, 0, 3, 0, 2, 2, 6,
                            7, 2, 7, 3 };
            int[] indexV = (int[])indexBuffer.Lock(0,0);
            for (int i = 0; i < 36; i++) {
                indexV[i] = index[i];
            }
            indexBuffer.Unlock();
        }

        protected void onVertexBufferCreate(object sender, EventArgs e) {
            CustomVertex.PositionColored[] verts = (CustomVertex.PositionColored[])vertexBuffer.Lock(0,0);
           /*
            verts[0].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            verts[0].Color = Color.Aqua.ToArgb();
            verts[1].Position = new Vector3(1.0f, 1.0f, 0.0f);
            verts[1].Color = Color.Brown.ToArgb();
            verts[2].Position = new Vector3(1.0f, -1.0f, 0.0f);
            verts[2].Color = Color.LightPink.ToArgb();
            verts[3].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            verts[3].Color = Color.Aqua.ToArgb();
            verts[4].Position = new Vector3(-1.0f, 1.0f, 0.0f);
            verts[4].Color = Color.Red.ToArgb();
            verts[5].Position = new Vector3(1.0f, 1.0f, 0.0f);
            verts[5].Color = Color.Brown.ToArgb();
            */
            verts[0].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            verts[0].Color = Color.Aqua.ToArgb();
            verts[1].Position = new Vector3(1.0f, 1.0f, 1.0f);
            verts[1].Color = Color.Brown.ToArgb();
            verts[2].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            verts[2].Color = Color.LightPink.ToArgb();
            verts[3].Position = new Vector3(1.0f, -1.0f, 1.0f);
            verts[3].Color = Color.Aqua.ToArgb();
            verts[4].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            verts[4].Color = Color.Red.ToArgb();
            verts[5].Position = new Vector3(1.0f, 1.0f, -1.0f);
            verts[5].Color = Color.Brown.ToArgb();
            verts[6].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            verts[6].Color = Color.Red.ToArgb();
            verts[7].Position = new Vector3(1.0f, -1.0f, -1.0f);
            verts[7].Color = Color.Brown.ToArgb();

            verts[8].Position = new Vector3(-2.0f, -2.0f, -2.0f);
            verts[8].Color = Color.LightPink.ToArgb();
            verts[9].Position = new Vector3(-2.0f, -2.0f, 2.0f);
            verts[9].Color = Color.LightPink.ToArgb();
            verts[10].Position = new Vector3(2.0f, -2.0f, 2.0f);
            verts[10].Color = Color.LightPink.ToArgb();
            verts[11].Position = new Vector3(-2.0f, -2.0f, -2.0f);
            verts[11].Color = Color.LightPink.ToArgb();
            verts[12].Position = new Vector3(2.0f, -2.0f, 2.0f);
            verts[12].Color = Color.LightPink.ToArgb();
            verts[13].Position = new Vector3(2.0f, -2.0f, -2.0f);
            verts[13].Color = Color.LightPink.ToArgb();


            verts[14].Position = new Vector3(-2.0f, -2.0f, 2.0f);
            verts[14].Color = Color.LightPink.ToArgb();
            verts[15].Position = new Vector3(-2.0f, 2.0f, 2.0f);
            verts[15].Color = Color.LightPink.ToArgb();
            verts[16].Position = new Vector3(2.0f, 2.0f, 2.0f);
            verts[16].Color = Color.LightPink.ToArgb();
            verts[17].Position = new Vector3(-2.0f, -2.0f, 2.0f);
            verts[17].Color = Color.LightPink.ToArgb();
         
            verts[18].Position = new Vector3(2.0f, 2.0f, 2.0f);
            verts[18].Color = Color.LightPink.ToArgb();
            verts[19].Position = new Vector3(2.0f, -2.0f, 2.0f);
            verts[19].Color = Color.LightPink.ToArgb();
           
            vertexBuffer.Unlock();
        }

        private void SetupMatrices()
        {
            Vector3 vc = new Vector3(0.0f, 0.0f, -5.0f);
            vc.TransformCoordinate(Matrix.RotationYawPitchRoll(angle, viewz, 0));
            _device.Transform.View = Matrix.LookAtLH(vc, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, 1.0f, 1.0f, 100.0f);
        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.CullMode = Cull.CounterClockwise;
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
            _device.Indices = indexBuffer;
            _device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, -6.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.DrawPrimitives(PrimitiveType.TriangleList, 8, 2);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 14, 2);
            SetupMatrices();
            _device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 8, 0, 12);

            /*
            _device.Transform.World = Matrix.Translation(0, 0, -1);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            _device.Transform.World = Matrix.RotationY((float)Math.PI) * Matrix.Translation(0, 0, 1);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            _device.Transform.World = Matrix.RotationY(-(float)Math.PI / 2) * Matrix.Translation(1, 0, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            _device.Transform.World = Matrix.RotationY((float)Math.PI / 2) * Matrix.Translation(-1, 0, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            _device.Transform.World = Matrix.RotationX((float)Math.PI / 2) * Matrix.Translation(0, 1, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            _device.Transform.World = Matrix.RotationX(-(float)Math.PI / 2) * Matrix.Translation(0, -1, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
        
             */ 
        }
    }
}
