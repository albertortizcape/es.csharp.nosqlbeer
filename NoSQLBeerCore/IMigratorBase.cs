using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLBeerCore
{
    public interface IMigratorBase
    {
        void Migrate();
        void MigrateBreweries();
        void MigrateBreweriesGeocodes();
        void MigrateBeers();
        void MigrateStyles();
    }
}
