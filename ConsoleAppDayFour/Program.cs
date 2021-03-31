using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDayFour
{

    class Program
    {
        static void Main(string[] args)
        {
            var DbMigrator = new DbMigrator(new Logger());
            var logger = new Logger();
            var Installer = new Installer(logger);

            DbMigrator.Migrate();
            Installer.Install();
        }
    }
}
