using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.Domain.Models.Author
{
    public class AuthorModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
    }
}
