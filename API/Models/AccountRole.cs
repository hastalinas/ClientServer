using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_tr_account_roles")]
public class AccountRole : BaseEntity
{
    [Column("acoount_guid")]
    public Guid AccountGuid { get; set; }
    [Column("role_guid")]
    public Guid RoleGuid { get; set; }

    //Cardinality 1 account role 1 account
    public Account? Account { get; set; }

    // Cardinality 1 account role 1 role
    public Role? Role { get; set; }
}
