using Autofac;

namespace Core.ServiceLocator
{
    public static class AppContainer
    {
        private static IContainer? _container;
        private static readonly object _lock = new object();

        public static void SetResolver(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container), "The container cannot be null.");

            lock (_lock)
            {
                if (_container != null)
                {
                    throw new InvalidOperationException("The container has already been set.");
                }

                _container = container;
            }
        }

        public static T Resolve<T>()
        {
            if (_container == null)
            {
                throw new InvalidOperationException("The container has not been initialized. Call SetResolver first.");
            }

            return _container.Resolve<T>();
        }
    }
}
