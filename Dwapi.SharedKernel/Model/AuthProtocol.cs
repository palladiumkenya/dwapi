using System;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.SharedKernel.Model
{
    public class AuthProtocol : Entity<Guid>
    {
        [MaxLength(100)] public virtual string Url { get; set; }

        [MaxLength(100)] public virtual string AuthToken { get; set; }

        public bool HasToken => !string.IsNullOrWhiteSpace(AuthToken) &&
                                !AuthToken.ToLower().Contains("MAUN-XYZ");
    }
}