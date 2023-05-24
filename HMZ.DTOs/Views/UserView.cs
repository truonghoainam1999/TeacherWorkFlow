using HMZ.Database.Entities.Base;

namespace HMZ.DTOs.Views
{
    public class UserView : BaseEntity
    {
        public String? Email { get; set; }
        public String? Username { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? FullName => $"{FirstName} {LastName}";
        public String? Image { get; set; }
        public List<String>? Roles { get; set; }
        public List<RoleView>? RolesView { get; set; }
        public String? Token { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Int32? TotalRecords { get; set; }
        public String? IdString { get; set; }
    }
}
