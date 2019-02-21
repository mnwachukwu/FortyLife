using FortyLife.DataAccess;

namespace FortyLife.App
{
    public static class AppInitialization
    {
        public static void OnStartup()
        {
            ApplicationUserEngine.Initialize();
        }
    }
}