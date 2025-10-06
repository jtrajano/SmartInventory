using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Domain.Entities;
public class User : BaseEntity
{
  
    public string Email { get; private set; } = string.Empty;
    public  string FullName { get; private set; } = string.Empty;
    public  string Role { get; private set; } = string.Empty;
    public string PasswordHash { get;private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get;  set; } = DateTime.Now;

    private User()
    {
        
    }

    public User(string email, string fullName, string role)
    {
        Email = email;
        FullName = fullName;
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.Now;
    }

    public void Deactivate()=> IsActive = false;


}
