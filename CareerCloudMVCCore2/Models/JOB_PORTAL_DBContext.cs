using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CareerCloudMVCCore2.Models
{
    public partial class JOB_PORTAL_DBContext : DbContext
    {
        public JOB_PORTAL_DBContext()
        {
        }

        public JOB_PORTAL_DBContext(DbContextOptions<JOB_PORTAL_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicantEducations> ApplicantEducations { get; set; }
        public virtual DbSet<ApplicantJobApplications> ApplicantJobApplications { get; set; }
        public virtual DbSet<ApplicantProfiles> ApplicantProfiles { get; set; }
        public virtual DbSet<ApplicantResumes> ApplicantResumes { get; set; }
        public virtual DbSet<ApplicantSkills> ApplicantSkills { get; set; }
        public virtual DbSet<ApplicantWorkHistory> ApplicantWorkHistory { get; set; }
        public virtual DbSet<CompanyDescriptions> CompanyDescriptions { get; set; }
        public virtual DbSet<CompanyJobEducations> CompanyJobEducations { get; set; }
        public virtual DbSet<CompanyJobSkills> CompanyJobSkills { get; set; }
        public virtual DbSet<CompanyJobs> CompanyJobs { get; set; }
        public virtual DbSet<CompanyJobsDescriptions> CompanyJobsDescriptions { get; set; }
        public virtual DbSet<CompanyLocations> CompanyLocations { get; set; }
        public virtual DbSet<CompanyProfiles> CompanyProfiles { get; set; }
        public virtual DbSet<SecurityLogins> SecurityLogins { get; set; }
        public virtual DbSet<SecurityLoginsLog> SecurityLoginsLog { get; set; }
        public virtual DbSet<SecurityLoginsRoles> SecurityLoginsRoles { get; set; }
        public virtual DbSet<SecurityRoles> SecurityRoles { get; set; }
        public virtual DbSet<SystemCountryCodes> SystemCountryCodes { get; set; }
        public virtual DbSet<SystemLanguageCodes> SystemLanguageCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=VISHWANTMAMIDI\\HUMBERBRIDGING;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True");
//            }
                 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducations>(entity =>
            {
                entity.ToTable("Applicant_Educations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CertificateDiploma)
                    .HasColumnName("Certificate_Diploma")
                    .HasMaxLength(100);

                entity.Property(e => e.CompletionDate)
                    .HasColumnName("Completion_Date")
                    .HasColumnType("date");

                entity.Property(e => e.CompletionPercent).HasColumnName("Completion_Percent");

                entity.Property(e => e.Major)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate)
                    .HasColumnName("Start_Date")
                    .HasColumnType("date");

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.ApplicantNavigation)
                    .WithMany(p => p.ApplicantEducations)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Educations_Applicant_Profiles");
            });

            modelBuilder.Entity<ApplicantJobApplications>(entity =>
            {
                entity.ToTable("Applicant_Job_Applications");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ApplicationDate)
                    .HasColumnName("Application_Date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.ApplicantNavigation)
                    .WithMany(p => p.ApplicantJobApplications)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Job_Applications_Applicant_Profiles");

                entity.HasOne(d => d.JobNavigation)
                    .WithMany(p => p.ApplicantJobApplications)
                    .HasForeignKey(d => d.Job)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Job_Applications_Company_Jobs");
            });

            modelBuilder.Entity<ApplicantProfiles>(entity =>
            {
                entity.ToTable("Applicant_Profiles");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CityTown)
                    .HasColumnName("City_Town")
                    .HasMaxLength(100);

                entity.Property(e => e.CountryCode)
                    .HasColumnName("Country_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Currency)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CurrentRate)
                    .HasColumnName("Current_Rate")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentSalary)
                    .HasColumnName("Current_Salary")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StateProvinceCode)
                    .HasColumnName("State_Province_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StreetAddress)
                    .HasColumnName("Street_Address")
                    .HasMaxLength(100);

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.ZipPostalCode)
                    .HasColumnName("Zip_Postal_Code")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.ApplicantProfiles)
                    .HasForeignKey(d => d.CountryCode)
                    .HasConstraintName("FK_Applicant_Profiles_System_Country_Codes");

                entity.HasOne(d => d.LoginNavigation)
                    .WithMany(p => p.ApplicantProfiles)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Profiles_Security_Logins");
            });

            modelBuilder.Entity<ApplicantResumes>(entity =>
            {
                entity.ToTable("Applicant_Resumes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("Last_Updated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Resume).IsRequired();

                entity.HasOne(d => d.ApplicantNavigation)
                    .WithMany(p => p.ApplicantResumes)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Resumes_Applicant_Profiles");
            });

            modelBuilder.Entity<ApplicantSkills>(entity =>
            {
                entity.ToTable("Applicant_Skills");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndMonth).HasColumnName("End_Month");

                entity.Property(e => e.EndYear).HasColumnName("End_Year");

                entity.Property(e => e.Skill)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SkillLevel)
                    .IsRequired()
                    .HasColumnName("Skill_Level")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StartMonth).HasColumnName("Start_Month");

                entity.Property(e => e.StartYear).HasColumnName("Start_Year");

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.ApplicantNavigation)
                    .WithMany(p => p.ApplicantSkills)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Skills_Applicant_Profiles");
            });

            modelBuilder.Entity<ApplicantWorkHistory>(entity =>
            {
                entity.ToTable("Applicant_Work_History");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("Company_Name")
                    .HasMaxLength(150);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("Country_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EndMonth).HasColumnName("End_Month");

                entity.Property(e => e.EndYear).HasColumnName("End_Year");

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasColumnName("Job_Description")
                    .HasMaxLength(500);

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasColumnName("Job_Title")
                    .HasMaxLength(50);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartMonth).HasColumnName("Start_Month");

                entity.Property(e => e.StartYear).HasColumnName("Start_Year");

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.ApplicantNavigation)
                    .WithMany(p => p.ApplicantWorkHistory)
                    .HasForeignKey(d => d.Applicant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Work_Experiences_Applicant_Profiles");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.ApplicantWorkHistory)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicant_Work_History_System_Country_Codes");
            });

            modelBuilder.Entity<CompanyDescriptions>(entity =>
            {
                entity.ToTable("Company_Descriptions");

                entity.HasIndex(e => new { e.Company, e.LanguageId })
                    .HasName("IX_UNQ_Company_Descriptions")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CompanyDescription)
                    .IsRequired()
                    .HasColumnName("Company_Description")
                    .HasMaxLength(1000);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("Company_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasColumnName("LanguageID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CompanyNavigation)
                    .WithMany(p => p.CompanyDescriptions)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Descriptions_Company_Profiles");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.CompanyDescriptions)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Descriptions_System_Language_Codes");
            });

            modelBuilder.Entity<CompanyJobEducations>(entity =>
            {
                entity.ToTable("Company_Job_Educations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Major)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.JobNavigation)
                    .WithMany(p => p.CompanyJobEducations)
                    .HasForeignKey(d => d.Job)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Job_Educations_Company_Jobs");
            });

            modelBuilder.Entity<CompanyJobSkills>(entity =>
            {
                entity.ToTable("Company_Job_Skills");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Skill)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SkillLevel)
                    .IsRequired()
                    .HasColumnName("Skill_Level")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.JobNavigation)
                    .WithMany(p => p.CompanyJobSkills)
                    .HasForeignKey(d => d.Job)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Job_Skills_Company_Jobs");
            });

            modelBuilder.Entity<CompanyJobs>(entity =>
            {
                entity.ToTable("Company_Jobs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsCompanyHidden).HasColumnName("Is_Company_Hidden");

                entity.Property(e => e.IsInactive).HasColumnName("Is_Inactive");

                entity.Property(e => e.ProfileCreated).HasColumnName("Profile_Created");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CompanyNavigation)
                    .WithMany(p => p.CompanyJobs)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Jobs_Company_Profiles");
            });

            modelBuilder.Entity<CompanyJobsDescriptions>(entity =>
            {
                entity.ToTable("Company_Jobs_Descriptions");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.JobDescriptions)
                    .HasColumnName("Job_Descriptions")
                    .HasMaxLength(1000);

                entity.Property(e => e.JobName)
                    .HasColumnName("Job_Name")
                    .HasMaxLength(100);

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.JobNavigation)
                    .WithMany(p => p.CompanyJobsDescriptions)
                    .HasForeignKey(d => d.Job)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Jobs_Descriptions_Company_Jobs");
            });

            modelBuilder.Entity<CompanyLocations>(entity =>
            {
                entity.ToTable("Company_Locations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CityTown)
                    .HasColumnName("City_Town")
                    .HasMaxLength(100);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("Country_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StateProvinceCode)
                    .HasColumnName("State_Province_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StreetAddress)
                    .HasColumnName("Street_Address")
                    .HasMaxLength(100);

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.ZipPostalCode)
                    .HasColumnName("Zip_Postal_Code")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CompanyNavigation)
                    .WithMany(p => p.CompanyLocations)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Locations_Company_Profiles");
            });

            modelBuilder.Entity<CompanyProfiles>(entity =>
            {
                entity.ToTable("Company_Profiles");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CompanyLogo).HasColumnName("Company_Logo");

                entity.Property(e => e.CompanyWebsite)
                    .HasColumnName("Company_Website")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasColumnName("Contact_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasColumnName("Contact_Phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate).HasColumnName("Registration_Date");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<SecurityLogins>(entity =>
            {
                entity.ToTable("Security_Logins");

                entity.HasIndex(e => e.Login)
                    .HasName("IX_UNQ_Security_Logins")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgreementAcceptedDate).HasColumnName("Agreement_Accepted_Date");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("Email_Address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ForceChangePassword).HasColumnName("Force_Change_Password");

                entity.Property(e => e.FullName)
                    .HasColumnName("Full_Name")
                    .HasMaxLength(100);

                entity.Property(e => e.IsInactive).HasColumnName("Is_Inactive");

                entity.Property(e => e.IsLocked).HasColumnName("Is_Locked");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUpdateDate).HasColumnName("Password_Update_Date");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrefferredLanguage)
                    .HasColumnName("Prefferred_Language")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<SecurityLoginsLog>(entity =>
            {
                entity.ToTable("Security_Logins_Log");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsSuccesful).HasColumnName("Is_Succesful");

                entity.Property(e => e.LogonDate)
                    .HasColumnName("Logon_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SourceIp)
                    .IsRequired()
                    .HasColumnName("Source_IP")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.LoginNavigation)
                    .WithMany(p => p.SecurityLoginsLog)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Log_Security_Logins");
            });

            modelBuilder.Entity<SecurityLoginsRoles>(entity =>
            {
                entity.ToTable("Security_Logins_Roles");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("Time_Stamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.LoginNavigation)
                    .WithMany(p => p.SecurityLoginsRoles)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Roles_Security_Logins");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.SecurityLoginsRoles)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Security_Logins_Roles_Security_Roles");
            });

            modelBuilder.Entity<SecurityRoles>(entity =>
            {
                entity.ToTable("Security_Roles");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsInactive).HasColumnName("Is_Inactive");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemCountryCodes>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("System_Country_Codes");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SystemLanguageCodes>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("PK_Culture_CultureID");

                entity.ToTable("System_Language_Codes");

                entity.Property(e => e.LanguageId)
                    .HasColumnName("LanguageID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NativeName)
                    .IsRequired()
                    .HasColumnName("Native_Name")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
