namespace Ent_EFDesigner
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SampleModel1 : DbContext
    {
        public SampleModel1()
            : base("name=SampleModel1")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("ex2pabo");
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SampleModel1>());
            // Database.SetInitializer<ex2table>(new RecreateDatabaseIfModelChanges<ex2table>());
        }

        public virtual DbSet<ex2table> ex2table { get; set; }
    }

    public partial class ex2table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }

        [Column("在庫")]
        public int Stock { get; set; }

        [StringLength(20)]
        [Column("品番")]
        public string PartNumber { get; set; }

        [StringLength(20)]
        [Column("品名")]
        public string Name { get; set; }

        [StringLength(20)]
        [Column("スペック")]
        public string Spec { get; set; }

        [StringLength(20)]
        [Column("メーカー")]
        public string Maker { get; set; }

        [StringLength(20)]
        [Column("仕入先")]
        public string Distributor { get; set; }

        [StringLength(20)]
        [Column("EOL情報")]
        public string EolInfo { get; set; }

        [StringLength(20)]
        [Column("代替品候補")]
        public string ReplacementPart { get; set; }
    }
}
