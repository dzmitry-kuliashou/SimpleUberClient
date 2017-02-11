using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SimpleUberWebApi.Distribution.Client.Installers
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Client"))
                .WithService.DefaultInterfaces()
                .LifestyleTransient());
        }
    }
}
