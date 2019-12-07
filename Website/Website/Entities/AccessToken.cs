using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class AccessToken : BaseEntity
    {        
        public string Token { get; set; }        

        public DateTime ExpirationDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
