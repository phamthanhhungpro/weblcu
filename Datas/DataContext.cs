using Castle.Components.DictionaryAdapter.Xml;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<GroupFunction> GroupFunctions { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { set; get; }

        public DbSet<NewsCategory> NewsCategories { set; get; }
        public DbSet<News> News { set; get; }

        public DbSet<PeopleCategory> PeopleCategories { set; get; }
        public DbSet<People> Peoples { set; get; }
        public DbSet<Location> Locations { set; get; }
        public DbSet<NationalCostumeCategory> NationalCostumeCategories { set; get; }
        public DbSet<NationalCostume> NationalCostumes { set; get; }

        public DbSet<Instrument> Instruments { set; get; }
        public DbSet<InstrumentCategory> InstrumentCategories { set; get; }
        public DbSet<Image360> Images { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<District> Districts { set; get; }
        public DbSet<Ward> Wards { set; get; }
        public DbSet<Language> Languages { set; get; }
        public DbSet<Models.DomainModels.File> Files { set; get; }
        public DbSet<Attachment> Attachments { set; get; }

        public DbSet<PeopleConfirm> PeopleConfirmes { set; get; }
        public DbSet<LogData> Logs { set; get; }
        public DbSet<ProduceToolCategory> ProduceToolCategories { set; get; }
        public DbSet<ProduceTool> ProduceTools { set; get; }
        public DbSet<CustomsTradition> CustomsTraditions { set; get; }
        public DbSet<Festival> Festivals { set; get; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>()
                .HasMany(e => e.Languages)
                .WithMany(e => e.Peoples);
        }
    }
}