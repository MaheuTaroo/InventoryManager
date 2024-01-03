using InventoryManager.Utils;

namespace InventoryManager.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Login());

            Utils.UtilData.Source = new DBDataSource("127.0.0.1", 3306, "root", "", "inventory_medical", "items");

            Application.Run(new Main());
        }
    }
}