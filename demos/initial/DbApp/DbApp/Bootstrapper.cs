using Prism.Modularity;
using System.Windows;

namespace DbApp
{
    public class Bootstrapper : Prism.Unity.UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new MainWindow();
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ApplicationModule));
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);

            (this.Shell as Window).Show();
        }
    }
}
