namespace BluePrints.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DevExpress.Mvvm;
    using System.Diagnostics;
    using BluePrints.Data.Helpers;

    public class EntityFrameworkConfiguration : DbConfiguration
    {
        public EntityFrameworkConfiguration()
        {
            AddInterceptor(new SoftDeleteInterceptor());
            AddInterceptor(new CreatedAndUpdatedDateInterceptor());
        }
    }

    public partial class BluePrintsEntities : DbContext
    {
        public BluePrintsEntities()
            : base("name=BluePrintsEntities")
        {
        }

        public virtual DbSet<AREA> AREAS { get; set; }
        public virtual DbSet<BASELINE> BASELINES { get; set; }
        public virtual DbSet<BASELINE_ITEM> BASELINE_ITEMS { get; set; }
        public virtual DbSet<COMMODITY> COMMODITIES { get; set; }
        public virtual DbSet<COMMODITY_CODE> COMMODITY_CODES { get; set; }
        public virtual DbSet<DEPARTMENT> DEPARTMENTS { get; set; }
        public virtual DbSet<DISCIPLINE> DISCIPLINES { get; set; }
        public virtual DbSet<DOCTYPE> DOCTYPES { get; set; }
        public virtual DbSet<ESTIMATION> ESTIMATIONS { get; set; }
        public virtual DbSet<ESTIMATION_ITEM> ESTIMATION_ITEMS { get; set; }
        public virtual DbSet<PHASE> PHASES { get; set; }
        public virtual DbSet<PROGRESS> PROGRESSES { get; set; }
        public virtual DbSet<PROGRESS_ITEM> PROGRESS_ITEMS { get; set; }
        public virtual DbSet<PROJECT> PROJECTS { get; set; }
        public virtual DbSet<PROJECT_REPORT> PROJECT_REPORTS { get; set; }
        public virtual DbSet<RATE> RATES { get; set; }
        public virtual DbSet<REGISTER> REGISTERS { get; set; }
        public virtual DbSet<ROLE> ROLES { get; set; }
        public virtual DbSet<ROLE_PERMISSION> ROLE_PERMISSIONS { get; set; }
        public virtual DbSet<SETTINGS_GLOBAL> SETTINGS_GLOBALS { get; set; }
        public virtual DbSet<UOM> UOMS { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<VARIATION> VARIATIONS { get; set; }
        public virtual DbSet<VARIATION_ITEM> VARIATION_ITEMS { get; set; }
        public virtual DbSet<WORKPACK> WORKPACKS { get; set; }
        public virtual DbSet<WORKPACK_ASSIGNMENT> WORKPACK_ASSIGNMENTS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AREA>()
                .HasMany(e => e.BASELINE_ITEMS)
                .WithOptional(e => e.AREA)
                .HasForeignKey(e => e.GUID_AREA);

            modelBuilder.Entity<AREA>()
                .HasMany(e => e.ESTIMATION_ITEMS)
                .WithOptional(e => e.AREA)
                .HasForeignKey(e => e.GUID_AREA);

            modelBuilder.Entity<AREA>()
                .HasMany(e => e.WORKPACKS)
                .WithRequired(e => e.AREA)
                .HasForeignKey(e => e.GUID_DAREA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BASELINE>()
                .Property(e => e.ACTUAL_UNITS)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BASELINE>()
                .Property(e => e.BUDGETED_UNITS)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BASELINE>()
                .HasMany(e => e.BASELINE_ITEMS)
                .WithOptional(e => e.BASELINE)
                .HasForeignKey(e => e.GUID_BASELINE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BASELINE>()
                .HasMany(e => e.VARIATIONS)
                .WithOptional(e => e.ORIBASELINE)
                .HasForeignKey(e => e.GUID_BASELINE);

            modelBuilder.Entity<BASELINE>()
                .HasMany(e => e.VARIATIONS1)
                .WithOptional(e => e.TOBASELINE)
                .HasForeignKey(e => e.GUID_ORIBASELINE);

            modelBuilder.Entity<COMMODITY>()
                .HasMany(e => e.ESTIMATION_ITEMS)
                .WithRequired(e => e.COMMODITY)
                .HasForeignKey(e => e.GUID_COMMODITY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DEPARTMENT>()
                .HasMany(e => e.BASELINE_ITEMS)
                .WithOptional(e => e.DEPARTMENT)
                .HasForeignKey(e => e.GUID_DEPARTMENT);

            modelBuilder.Entity<DEPARTMENT>()
                .HasMany(e => e.DOCTYPES)
                .WithRequired(e => e.DEPARTMENT)
                .HasForeignKey(e => e.GUID_DDEPARTMENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DEPARTMENT>()
                .HasMany(e => e.RATES)
                .WithRequired(e => e.DEPARTMENT)
                .HasForeignKey(e => e.GUID_DEPARTMENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DEPARTMENT>()
                .HasMany(e => e.WORKPACKS)
                .WithRequired(e => e.DEPARTMENT)
                .HasForeignKey(e => e.GUID_DDEPARTMENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPLINE>()
                .HasMany(e => e.BASELINE_ITEMS)
                .WithOptional(e => e.DISCIPLINE)
                .HasForeignKey(e => e.GUID_DISCIPLINE);

            modelBuilder.Entity<DISCIPLINE>()
                .HasMany(e => e.COMMODITY_CODES)
                .WithRequired(e => e.DISCIPLINE)
                .HasForeignKey(e => e.GUID_DISCIPLINE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPLINE>()
                .HasMany(e => e.ESTIMATION_ITEMS)
                .WithOptional(e => e.DISCIPLINE)
                .HasForeignKey(e => e.GUID_DISCIPLINE);

            modelBuilder.Entity<DISCIPLINE>()
                .HasMany(e => e.RATES)
                .WithRequired(e => e.DISCIPLINE)
                .HasForeignKey(e => e.GUID_DISCIPLINE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPLINE>()
                .HasMany(e => e.WORKPACKS)
                .WithRequired(e => e.DISCIPLINE)
                .HasForeignKey(e => e.GUID_DDISCIPLINE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DOCTYPE>()
                .HasMany(e => e.BASELINE_ITEMS)
                .WithOptional(e => e.DOCTYPE)
                .HasForeignKey(e => e.GUID_DOCTYPE);

            modelBuilder.Entity<DOCTYPE>()
                .HasMany(e => e.ESTIMATION_ITEMS)
                .WithOptional(e => e.DOCTYPE)
                .HasForeignKey(e => e.GUID_TYPE);

            modelBuilder.Entity<DOCTYPE>()
                .HasMany(e => e.WORKPACKS)
                .WithRequired(e => e.DOCTYPE)
                .HasForeignKey(e => e.GUID_DDOCTYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ESTIMATION>()
                .HasMany(e => e.ESTIMATION_ITEMS)
                .WithRequired(e => e.ESTIMATION)
                .HasForeignKey(e => e.GUID_ESTIMATION)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHASE>()
                .HasMany(e => e.BASELINE_ITEMS)
                .WithOptional(e => e.PHASE)
                .HasForeignKey(e => e.GUID_PHASE);

            modelBuilder.Entity<PHASE>()
                .HasMany(e => e.WORKPACKS)
                .WithOptional(e => e.PHASE)
                .HasForeignKey(e => e.GUID_DPHASE);

            modelBuilder.Entity<PROGRESS>()
                .HasMany(e => e.PROGRESS_ITEMS)
                .WithRequired(e => e.PROGRESS)
                .HasForeignKey(e => e.GUID_PROGRESS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROGRESS_ITEM>()
                .Property(e => e.EARNED_UNITS)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PROJECT>()
                .Property(e => e.CURRENCYCONVERSION)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PROJECT>()
                .Property(e => e.REVIEWPERCENTAGE)
                .HasPrecision(2, 2);

            modelBuilder.Entity<PROJECT>()
                .Property(e => e.REVIEWPERIOD)
                .HasPrecision(2, 0);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.AREAS)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.WORKPACKS)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.BASELINES)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.COMMODITIES)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.ESTIMATIONS)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.PHASES)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.PROGRESSES)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.REGISTERS)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.PROJECT_REPORTS)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.RATES)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.VARIATIONS)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROJECT>()
                .HasMany(e => e.WORKPACKS)
                .WithRequired(e => e.PROJECT)
                .HasForeignKey(e => e.GUID_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RATE>()
                .Property(e => e.RATE1)
                .HasPrecision(5, 2);

            modelBuilder.Entity<REGISTER>()
                .Property(e => e.UNIQUE_H_NUM)
                .IsFixedLength();

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.ROLE_PERMISSIONS)
                .WithRequired(e => e.ROLE)
                .HasForeignKey(e => e.GUID_ROLE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.USERS)
                .WithOptional(e => e.ROLE)
                .HasForeignKey(e => e.GUID_ROLE);

            modelBuilder.Entity<SETTINGS_GLOBAL>()
                .Property(e => e.REVIEW_PERCENTAGE)
                .HasPrecision(2, 2);

            modelBuilder.Entity<SETTINGS_GLOBAL>()
                .Property(e => e.REVIEW_PERIOD)
                .HasPrecision(2, 0);

            modelBuilder.Entity<VARIATION>()
                .HasMany(e => e.VARIATION_ITEMS)
                .WithRequired(e => e.VARIATION)
                .HasForeignKey(e => e.GUID_VARIATION)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VARIATION>()
                .HasMany(e => e.BASELINE_ITEMS)
                .WithOptional(e => e.VARIATION)
                .HasForeignKey(e => e.GUID_VARIATION)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WORKPACK>()
                .HasMany(e => e.WORKPACK_ASSIGNMENTS)
                .WithRequired(e => e.WORKPACK)
                .HasForeignKey(e => e.GUID_WORKPACK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WORKPACK>()
                .HasMany(e => e.BASELINE_ITEMS)
                .WithOptional(e => e.WORKPACK)
                .HasForeignKey(e => e.GUID_WORKPACK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WORKPACK_ASSIGNMENT>()
                .Property(e => e.LOW_VALUE)
                .HasPrecision(10, 2);

            modelBuilder.Entity<WORKPACK_ASSIGNMENT>()
                .Property(e => e.HIGH_VALUE)
                .HasPrecision(10, 2);
        }

        /// <summary>
        /// Allow redo operation to undo deleted record
        /// If a record with generated GUID passes through it'll be formatted as a modified record
        /// recommended to explicitly set New Guid for appropriate entities -- http://msdn.microsoft.com/en-us/library/dd283139.aspx
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            IEnumerable<System.Data.Entity.Infrastructure.DbEntityEntry> AddedDbEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            if(AddedDbEntries.Count() > 0)
            {
                Type entityType = AddedDbEntries.First().Entity.GetType();
                PropertyInfo keyPropertyInfo = DataUtils.GetKeyPropertyInfo(entityType);
                DatabaseGeneratedAttribute keyPropertyInfoDbGenerationAttribute = null;
                if (keyPropertyInfo != null)
                    keyPropertyInfoDbGenerationAttribute = entityType.GetProperty(keyPropertyInfo.Name).GetCustomAttributes(
                        typeof(DatabaseGeneratedAttribute), true).Cast<DatabaseGeneratedAttribute>().Single();

                if(keyPropertyInfo != null && keyPropertyInfoDbGenerationAttribute != null && keyPropertyInfoDbGenerationAttribute.DatabaseGeneratedOption != DatabaseGeneratedOption.Identity)
                {
                    foreach(var dbEntry in AddedDbEntries)
                    {
                        var entryKeyMember = dbEntry.Property(keyPropertyInfo.Name);
                        if (entryKeyMember.CurrentValue.GetType() == typeof(Guid))
                        {
                            Guid entryKeyMemberValue = (Guid)entryKeyMember.CurrentValue;

                            //If key field already has generated guid it means that record should be modified, assuming redo operation have entity with null value in deleted field
                            //This will essentially undelete the record
                            if (entryKeyMemberValue != Guid.Empty)
                                dbEntry.State = EntityState.Modified;
                            //Generate a new guid if record have an empty guid key field
                            else
                            {
                                entryKeyMember.CurrentValue = Guid.NewGuid();
                                if(entityType.BaseType == typeof(BASELINE_ITEM))
                                {
                                    PropertyInfo OGPropertyInfo = entityType.GetProperty("GUID_ORIGINAL");
                                    if(OGPropertyInfo.GetValue(dbEntry.Entity).ToString() == Guid.Empty.ToString())
                                        OGPropertyInfo.SetValue(dbEntry.Entity, entryKeyMember.CurrentValue);
                                }
                            }
                        }
                        else
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
