namespace WcfService.DbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using EntityModels;

    public class PatientsContext : DbContext
    {

        public PatientsContext()
             : base("name=Pacjent")
        {
            Database.SetInitializer<PatientsContext>(new CreateDatabaseIfNotExists<PatientsContext>());
        }

        public DbSet<Illness> TableIllness { get; set; }
        public DbSet<IllnessHasSymptom> TableIllnessHasSymptom { get; set; }
        public DbSet<Symptom> TableSymptom { get; set; }
        public DbSet<Patient> TablePatient { get; set; }
        public DbSet<LifeFuncMeasure> TableLifeFuncMeasure { get; set; }
        public DbSet<PatientWasSick> TablePatientWasSick { get; set; }
        public DbSet<Role> TableRole { get; set; }
        public DbSet<User> TableUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}