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

namespace Kull.DX.Meshs
{
    public partial class TextureForm : BaseGrapForm
    {
        Texture texture = null;
        Mesh mesh;
        Material meshMaterial;

        public TextureForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            mesh = Mesh.Box(_device,2.0f,2.0f,2.0f);
            meshMaterial = new Material();
            meshMaterial.Ambient = Color.White;
            meshMaterial.Diffuse = Color.White;
            texture = TextureLoader.FromFile(_device,@"..\..\0030.jpg");
            Mesh mesh1 = mesh.Clone(mesh.Options.Value,VertexFormats.Position|VertexFormats.Normal|VertexFormats.Texture0|VertexFormats.Texture1,mesh.Device);
            using (VertexBuffer vb = mesh1.VertexBuffer) {
                CustomVertex.PositionNormalTextured[] verts = (CustomVertex.PositionNormalTextured[])vb.Lock(0, typeof(CustomVertex.PositionNormalTextured), LockFlags.None, mesh1.NumberVertices);
                try
                {
                    for (int i = 0; i < verts.Length; i += 4)
                    {
                        verts[i].Tu = 0.0f;
                        verts[i].Tv = 0.0f;
                        verts[i + 1].Tu = 1.0f;
                        verts[i + 1].Tv = 0.0f;
                        verts[i + 2].Tu = 1.0f;
                        verts[i + 2].Tv = 1.0f;
                        verts[i + 3].Tu = 0.0f;
                        verts[i + 3].Tv = 1.0f;
                    }
                    mesh = mesh1;
                }
                finally {
                    vb.Unlock();
                }
            }
        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.ZBufferEnable = true;
            _device.RenderState.Ambient = Color.White;
            _device.Lights[0].Type = LightType.Directional;
            _device.Lights[0].Diffuse = Color.White;
            _device.Lights[0].Direction = new Vector3(0, -1, 0);
            _device.Lights[0].Update();
            _device.Lights[0].Enabled = true;
            _device.Material = meshMaterial;
            _device.SetTexture(0, texture);
        }

        protected virtual void setMatrices()
        {
            _device.Transform.World = Matrix.RotationYawPitchRoll(angle, angle, 0);
            _device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 1.0f, viewz), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, 1.33f, 1.0f, 100.0f);
        }

        protected override void rendering()
        {
            base.rendering();
            setMatrices();
            mesh.DrawSubset(0);
            _device.Transform.World = Matrix.RotationYawPitchRoll(angle, angle, 0) * Matrix.Translation(3, 0, 4);
            mesh.DrawSubset(0);
        }
    }
}
