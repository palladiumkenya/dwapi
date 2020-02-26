using System;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.SharedKernel.Model
{
    public class AuthProtocol : Entity<Guid>
    {
        [MaxLength(100)] public virtual string Url { get; set; }

        [MaxLength(100)] public virtual string AuthToken { get; set; }

        [MaxLength(100)] public virtual string UserName { get; set; }

        [MaxLength(100)] public virtual string Password { get; set; }

        public bool HasToken => !string.IsNullOrWhiteSpace(AuthToken) &&
                                !AuthToken.ToLower().Contains("MAUN-XYZ");

        public bool HasAuth => !string.IsNullOrWhiteSpace(UserName);
    }
}
