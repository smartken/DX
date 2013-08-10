using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Kull.DX.Polygon;
using Kull.DX.Light;

namespace Kull.DX
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new GrapForm());
            using (BaseGrapForm form = new Kull.DX.Meshs.TextureForm()) {
               form.Show();
               while (form.Created) {
                   form.draw();
                   Application.DoEvents();
               }
            }
        }
    }
}
