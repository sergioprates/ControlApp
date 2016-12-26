using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlAppWebApp.Models
{
    public class PermissionModel
    {
        public string Hash { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public bool Read { get; set; }

        public bool Write { get; set; }

        /// <summary>
        /// Return the key of permission
        /// </summary>
        /// <returns></returns>
        public string Feature{ get; set; }
    }
}