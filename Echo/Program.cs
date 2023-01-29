using Kosher.Extensions.Log;
using Kosher.Log;

namespace Echo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}KosherLog.config"));
            LogBuilder.Build();

            try
            {
                ServerModule serverModule = new ServerModule();
                serverModule.Run();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex.Message);
            }            
        }
    }
}