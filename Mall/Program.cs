using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DAL.EFContext;
using System.Threading.Tasks;

namespace Mall
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("Office 2013");

            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            using (DatabaseUpdate databaseUpdate = new DatabaseUpdate())
            {
                databaseUpdate.Update();
            }

            Application.Run(new main());
        }
    }
}
