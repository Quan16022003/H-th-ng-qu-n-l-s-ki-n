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

        public string Get<T>()
        {
            var type = typeof(T);
            var attribute = (AreaAttribute) type.GetCustomAttributes(typeof(AreaAttribute), true).FirstOrDefault()!;
            string area = attribute == null ? "Default" : attribute.RouteValue;

            IPathProvider provider = _serviceProvider.GetKeyedService<IPathProvider>(area.ToLower()) 
                ?? throw new ArgumentException($"Path provider not have Area: {area}");

            return provider.GetViewsPath(type);
        }
    }
}
