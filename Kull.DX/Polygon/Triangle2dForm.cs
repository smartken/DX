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
    public partial class Triangle2dForm : BaseGrapForm
    {
        CustomVertex.TransformedColored[] verts,verts2;

        public Triangle2dForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            /*
            verts = new CustomVertex.TransformedColored[3];
            verts[0].Position = new Vector4(150.0f,50.0f,0.5f,1.0f);
            verts[0].Color = Color.Aqua.ToArgb();
            verts[1].Position = new Vector4(250.0f, 250.0f, 0.5f, 1.0f);
            verts[1].Color = Color.Brown.ToArgb();
            verts[2].Position = new Vector4(50.0f, 250.0f, 0.5f, 1.0f);
            verts[2].Color = Color.LightPink.ToArgb();
            */
            verts = new CustomVertex.TransformedColored[6];
            verts[0].Position = new Vector4(10.0f, 10.0f, 0.5f, 1.0f);
            verts[0].Color = Color.Aqua.ToArgb();
            verts[1].Position = new Vector4(210.0f, 10.0f, 0.5f, 1.0f);
            verts[1].Color = Color.Brown.ToArgb();
            verts[2].Position = new Vector4(110.0f, 60.0f, 0.5f, 1.0f);
            verts[2].Color = Color.LightPink.ToArgb();
            verts[3].Position = new Vector4(210.0f, 210.0f, 0.5f, 1.0f);
            verts[3].Color = Color.Aqua.ToArgb();
            verts[4].Position = new Vector4(110.0f, 160.0f, 0.5f, 1.0f);
            verts[4].Color = Color.Brown.ToArgb();
            verts[5].Position = new Vector4(10.0f, 210.0f, 0.5f, 1.0f);
            verts[5].Color = Color.LightPink.ToArgb();

        }

        protected void move(float x,float y){
            verts2 = verts.Clone() as CustomVertex.TransformedColored[];
            for (int i = 0; i < verts2.Length; i++) {
                verts2[i].X += x;
                verts2[i].Y += y;
            }
        }

        protected override void  rendering()
        {
            base.rendering();
            _device.VertexFormat = CustomVertex.TransformedColored.Format;
          //  _device.DrawUserPrimitives(PrimitiveType.TriangleList, 2, verts);
            move(1.0f, 1.0f);
            _device.DrawUserPrimitives(PrimitiveType.TriangleFan, 5, verts2);
      
            verts2 = null;

        }
    }
}
