namespace Web.Utils.ViewsPathServices.Implementations
{
    public class PathProvideManager : IPathProvideManager
    {
        private readonly IServiceProvider _serviceProvider;

        public PathProvideManager(IServiceProvider service)
        {
            _serviceProvider = service;
        }

        public string Get<T>(string? area = null)
        {
            area ??= "Default";

            IPathProvider provider = _serviceProvider.GetKeyedService<IPathProvider>(area) 
                ?? throw new ArgumentException($"Path provider not have Area: {area}");

            return provider.GetViewsPath(typeof(T));
        }
    }
}
