using System;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.SharedKernel.Model
{
    public class Registry : Entity<Guid>
    {
        [MaxLength(100)] public string Name { get; set; }
        [MaxLength(100)] public string Url { get; set; }
        [MaxLength(100)] public string AuthToken { get; set; }
        [MaxLength(50)] public virtual string SubscriberId { get; set; }
        public string DocketId { get; set; }

        public Registry()
        {
        }

        public Registry(string url, string docketId)
        {
            Url = url;
            DocketId = docketId;
        }

        public Registry(string name, string url, string docketId) : this(url, docketId)
        {
            Name = name;
        }

        public Registry(string name, string url, string docketId, string authToken) : this(name, url, docketId)
        {
            AuthToken = authToken;
        }

        public bool RequiresAuthentication()
        {
            return !string.IsNullOrWhiteSpace(AuthToken);
        }
    }
}