using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using EventCloud.Events;

namespace EventCloud.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class AccountListOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

    }
}
