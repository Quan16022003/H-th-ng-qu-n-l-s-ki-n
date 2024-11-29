using Microsoft.AspNetCore.Mvc;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class PathProvideManager : IPathProvideManager
    {
        private readonly IServiceProvider _serviceProvider;

        public PathProvideManager(IServiceProvider service)
        {
            _serviceProvider = service;
        }

        private bool IsComponent(Type type) => type.IsSubclassOf(typeof(ViewComponent));

        private IPathProvider GetViewsProvider(string area)
        {
            IPathProvider provider = _serviceProvider.GetKeyedService<IPathProvider>(area.ToLower())
                ?? throw new ArgumentException($"Path provider not have Area: {area}");

            return provider;
        }

        private IPathProvider GetComponentProvider(string area)
        {
            IPathProvider provider = _serviceProvider.GetKeyedService<IPathProvider>($"{area.ToLower()}_component")
                ?? throw new ArgumentException($"Path provider not have Area Component: {area}_component");

            return provider;
        }

        public string Get<T>()
        {
            var type = typeof(T);
            var attribute = (AreaAttribute) type.GetCustomAttributes(typeof(AreaAttribute), true).FirstOrDefault()!;
            string area = attribute == null ? "Default" : attribute.RouteValue;

            var provider = !IsComponent(type) ? GetViewsProvider(area) : GetComponentProvider(area);

            return provider.GetViewsPath(type);
        }
    }
}
