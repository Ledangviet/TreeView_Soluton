using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excercise_2_Data_Access_Layer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Excercise_2_Data_Access_Layer.Data.DbContexts
{
    public class NodeDbContext : DbContext
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public NodeDbContext(DbContextOptions<NodeDbContext> options) : base(options)
        {
        }       
        #region dbSet
        public DbSet<Node> Nodes { get; set; }
        public DbSet<NodeAttribute> Attributes { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion
    }
}
