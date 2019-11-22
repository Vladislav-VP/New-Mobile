using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ApiModels
{
    public abstract class BaseApiModel
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
