using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAPI.Model
{
    public class Follow
    {
        public int Id { get; set; }
        public string Follower { get; set; }
        public string Followed { get; set; }
    }
}
