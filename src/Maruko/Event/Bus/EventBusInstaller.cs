using System.Reflection;
using Autofac;
using Maruko.Event.Bus.Factories;
using Maruko.Event.Bus.Handlers;
using Maruko.Utils;

namespace Maruko.Event.Bus
{
    /// <summary>
    /// Installs event bus system and registers all handlers automatically.
    /// </summary>
    internal class EventBusInstaller
    {

        private IEventStore _eventStore;
        
        public void Install()
        {
            Kernel_ComponentRegistered();
        }

        private void Kernel_ComponentRegistered()
        {
            _eventStore = ContainerManager.Current.Resolve<IEventStore>();
            var interfaces = typeof(IEventHandler).GetTypeInfo().GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    continue;
                }

                var genericArgs = @interface.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    //_eventStore.AddRegister(@interface,)
                }
            }
        }
    }
}
