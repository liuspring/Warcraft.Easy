using Abp.AutoMapper;

namespace TaskManager.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserListOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

    }
}
