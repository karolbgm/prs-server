using prs_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prsTest;
public static class TestDataHelper
{
    public static List<User> GetFakeUserList()
    {
        return new List<User>()
            {
                new User
                {
                    
                    Username = "karolbgm@gmail.com",
                    Password = "123"
                },
                new User
                {
                    Username = "greg@gmail.com",
                    Password = "greg"
                }
            };
    }
}
