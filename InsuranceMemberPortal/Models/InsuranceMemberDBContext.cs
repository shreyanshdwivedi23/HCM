using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace InsuranceMemberPortal.Models
{
    public partial class InsuranceMemberDBContext : DbContext
    {
        public InsuranceMemberDBContext()
        {
        }

        public InsuranceMemberDBContext(DbContextOptions<InsuranceMemberDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblInsurancePolicy> TblInsurancePolicies { get; set; }
        public virtual DbSet<TblMemberRegistration> TblMemberRegistrations { get; set; }
        public virtual DbSet<TblPolicyStatus> TblPolicyStatuses { get; set; }
        public virtual DbSet<TblPolicyType> TblPolicyTypes { get; set; }
        public virtual DbSet<TblUserRegistration> TblUserRegistrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-I3ENK0M\\SQLEXPRESS;Initial Catalog=InsuranceMemberDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblInsurancePolicy>(entity =>
            {
                entity.HasKey(e => e.PolicyId)
                    .HasName("PK__Tbl_Insu__78E3A9226EFB41DD");

                entity.ToTable("Tbl_InsurancePolicy");

                entity.Property(e => e.PolicyId).HasColumnName("policyId");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.PolicyCreatedBy).HasColumnName("policyCreatedBy");

                entity.Property(e => e.PolicyCreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("policyCreatedDate");

                entity.Property(e => e.PolicyEffectiveDate)
                    .HasColumnType("date")
                    .HasColumnName("policyEffectiveDate");

                entity.Property(e => e.PolicyPremiumAmount).HasColumnName("policyPremiumAmount");

                entity.Property(e => e.PolicyStatusId).HasColumnName("policyStatusId");

                entity.Property(e => e.PolicyTypeId).HasColumnName("policyTypeId");

                entity.Property(e => e.PolicyUpdatedBy).HasColumnName("policyUpdatedBy");

                entity.Property(e => e.PolicyUpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("policyUpdatedDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.TblInsurancePolicies)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Tbl_Insur__membe__70DDC3D8");

                entity.HasOne(d => d.PolicyStatus)
                    .WithMany(p => p.TblInsurancePolicies)
                    .HasForeignKey(d => d.PolicyStatusId)
                    .HasConstraintName("FK__Tbl_Insur__polic__6FE99F9F");
            });

            modelBuilder.Entity<TblMemberRegistration>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK__Tbl_Memb__7FD7CF1641A66037");

                entity.ToTable("Tbl_MemberRegistration");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.MemberAddress).HasColumnName("memberAddress");

                entity.Property(e => e.MemberCity)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("memberCity");

                entity.Property(e => e.MemberConfirmPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("memberConfirmPassword");

                entity.Property(e => e.MemberCreatedBy).HasColumnName("memberCreatedBy");

                entity.Property(e => e.MemberCreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("memberCreatedDate");

                entity.Property(e => e.MemberDateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("memberDateOfBirth");

                entity.Property(e => e.MemberEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("memberEmail");

                entity.Property(e => e.MemberFirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("memberFirstName");

                entity.Property(e => e.MemberLastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("memberLastName");

                entity.Property(e => e.MemberNo)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("memberNo")
                    .HasComputedColumnSql("('MEM'+right('00000000'+CONVERT([varchar](8),[memberId]),(8)))", true);

                entity.Property(e => e.MemberPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("memberPassword");

                entity.Property(e => e.MemberState)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("memberState");

                entity.Property(e => e.MemberUsername)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("memberUsername");

                entity.Property(e => e.MemberupdatedBy).HasColumnName("memberupdatedBy");

                entity.Property(e => e.MemberupdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("memberupdatedDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblMemberRegistrations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tbl_Membe__userI__6C190EBB");
            });

            modelBuilder.Entity<TblPolicyStatus>(entity =>
            {
                entity.HasKey(e => e.PolicyStatusId)
                    .HasName("PK__Tbl_Poli__7BB9BE3BB842AA46");

                entity.ToTable("Tbl_PolicyStatus");

                entity.Property(e => e.PolicyStatusId).HasColumnName("policyStatusId");

                entity.Property(e => e.CreatedBy).HasColumnType("date");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.PolicyStatusName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("policyStatusName");

                entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updatedDate");
            });

            modelBuilder.Entity<TblPolicyType>(entity =>
            {
                entity.HasKey(e => e.PolicyTypeId)
                    .HasName("PK__Tbl_Poli__4945E456ADEFC0D8");

                entity.ToTable("Tbl_PolicyType");

                entity.Property(e => e.PolicyTypeId).HasColumnName("policyTypeId");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.MemberCreatedBy)
                    .HasColumnType("date")
                    .HasColumnName("memberCreatedBy");

                entity.Property(e => e.PolicyType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("policyType");

                entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updatedDate");
            });

            modelBuilder.Entity<TblUserRegistration>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Tbl_User__CB9A1CFF454E26F4");

                entity.ToTable("Tbl_UserRegistration");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.UserCreatedBy).HasColumnName("userCreatedBy");

                entity.Property(e => e.UserCreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("userCreatedDate");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userPassword");

                entity.Property(e => e.UserRole)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("userRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
