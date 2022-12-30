using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMongoDB;

public static class MagicHelper
{
    public static readonly string ConnectionString = "mongodb+srv://vulcan:pass123@vulcanmongo.hptf95d.mongodb.net/?retryWrites=true&w=majority";
    public static readonly string LabDatabase = "MyCRUD";
    public static readonly string LabCollection = "Todo";
}
