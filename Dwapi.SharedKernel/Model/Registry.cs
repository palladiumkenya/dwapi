using System;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.SharedKernel.Model
{
    public class Registry:Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Url { get; set; }
        [MaxLength(100)]
        public string AuthToken { get; set; }
        [MaxLength(50)]
        public virtual string SubscriberId { get; set; }

        public Registry()
        {
        }

        public Registry(string url)
        {
            Url = url;
        }
        public Registry(string name, string url):this(url)
        {
            Name = name;
        }

        public Registry(string name, string url, string authToken) : this(name, url)
        {
            AuthToken = authToken;
        }

        public bool RequiresAuthentication()
        {
            return !string.IsNullOrWhiteSpace(AuthToken);
        }
    }
}