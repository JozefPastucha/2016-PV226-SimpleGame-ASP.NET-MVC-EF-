using System;
using System.ComponentModel.DataAnnotations;
using BrockAllen.MembershipReboot.Relational;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    /// <summary>
    /// Base class for application user, 
    /// all declared properties MUST be virtual
    /// 
    /// Please note this class needs to be referenced even from PL,
    /// (AuthenticationManager), therefore its placed apart from 
    /// other entities in DAL.
    /// </summary>
    public class UserAccount : RelationalUserAccount, IEntity<Guid>
    {  
        
    }
}
