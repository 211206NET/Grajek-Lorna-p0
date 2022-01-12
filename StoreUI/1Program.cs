global using Models;
global using StoreDL;
global using StoreBL;
global using UI;
global using System.Collections.Generic;
global using System.Linq;
global using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(@"..\StoreDL\logger.txt")
    .CreateLogger();
MenuFactory.GetMenu("main").Start();
