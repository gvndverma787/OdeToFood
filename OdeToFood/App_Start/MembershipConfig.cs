using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace OdeToFood
{
    public class MembershipConfig
    {
        public static void RegisterMembership()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection
                    ("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
    }
}