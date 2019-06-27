using System.Windows;

namespace RealBigCompany
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Title = "Землю крестьянам! Фабрики рабочим!";
            wnd.Show();
        }
    }
}
