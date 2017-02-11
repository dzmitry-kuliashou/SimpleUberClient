using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;

namespace MVCTypeScriptReact3.Config.Installers
{
    public static class InstallWindsorRegistrators
    {
        public static void Install(IWindsorContainer container)
        {
            //it's bulllshit for sure :) . Just a fast temporary solution
            var applicationPhysicalPath = "D:\\Programming\\React\\MVCTypeScriptReact3\\MVCTypeScriptReact3.Config\\bin\\Debug";

            container.Install(
                    FromAssembly.InDirectory(new AssemblyFilter(applicationPhysicalPath, "*.dll"))
                    );
        }
    }
}
