//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.BLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserLogin
    {
        public long Id { get; set; }
        public Nullable<long> UserId { get; set; }
        public string IPAddress { get; set; }
        public bool Successful { get; set; }
        public string TokenId { get; set; }
        public string AppId { get; set; }
        public System.DateTime DateUtc { get; set; }
        public string ProtectedTicket { get; set; }
        public bool IsLoggedIn { get; set; }
    
        public virtual User User { get; set; }
    }
}