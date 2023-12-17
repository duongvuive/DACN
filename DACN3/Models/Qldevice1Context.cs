using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DACN3.Models;

public partial class Qldevice1Context : DbContext
{
    public Qldevice1Context()
    {
    }

    public Qldevice1Context(DbContextOptions<Qldevice1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountInformation> AccountInformations { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Borrow> Borrows { get; set; }

    public virtual DbSet<BrokenHistory> BrokenHistories { get; set; }

    public virtual DbSet<ClassDetail> ClassDetails { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<ConfirmEquipmentVoucher> ConfirmEquipmentVouchers { get; set; }

    public virtual DbSet<Confirmation> Confirmations { get; set; }

    public virtual DbSet<DetailsEquipmentVoucher> DetailsEquipmentVouchers { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceClassfication> DeviceClassfications { get; set; }

    public virtual DbSet<DeviceRepairProcess> DeviceRepairProcesses { get; set; }

    public virtual DbSet<DeviceWarehouse> DeviceWarehouses { get; set; }

    public virtual DbSet<EquipmentVoucher> EquipmentVouchers { get; set; }

    public virtual DbSet<Floor> Floors { get; set; }

    public virtual DbSet<ImportExportWarehouse> ImportExportWarehouses { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<ProcessDevice> ProcessDevices { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NEIOBVT;Database=QLDevice1;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Account_Information_ID");

            entity.ToTable("AccountInformation");

            entity.HasIndex(e => e.FullName, "KEY_AccountInformation_FullName").IsUnique();

            entity.HasIndex(e => e.IdAspNetUsers, "KEY_AccountInformation_IdAspNetUsers").IsUnique();

            entity.HasIndex(e => e.Phone, "KEY_AccountInformation_Phone").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Addres).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAspNetUsersNavigation).WithOne(p => p.AccountInformation)
                .HasForeignKey<AccountInformation>(d => d.IdAspNetUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountInformation_AspNetUsers_Id");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AREA_ID");

            entity.ToTable("AREA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(u => u.UserRoles)
           .WithOne(ur => ur.User)
           .HasForeignKey(ur => ur.UserId)
          .IsRequired();
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });
        });
        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Borrow>(entity =>
        {
            entity.HasKey(e => e.BorrowId).HasName("PK__Borrow__B7FA24ECBED08EAC");

            entity.ToTable("Borrow");

            entity.Property(e => e.BorrowId)
                .ValueGeneratedNever()
                .HasColumnName("Borrow_ID");
            entity.Property(e => e.BorrowDate)
                .HasColumnType("date")
                .HasColumnName("Borrow_Date");
            entity.Property(e => e.Borrower).HasMaxLength(80);
            entity.Property(e => e.DeviceClassroomId).HasColumnName("Device_Classroom_ID");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("date")
                .HasColumnName("Return_Date");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.DeviceClassroom).WithMany(p => p.Borrows)
                .HasForeignKey(d => d.DeviceClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Borrow__Device_C__345EC57D");

            entity.HasOne(d => d.User).WithMany(p => p.Borrows)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Borrow_AspNetUsers_Id");
        });

        modelBuilder.Entity<BrokenHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Status_History_ID");

            entity.ToTable("Broken_History");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeviceClassroomId).HasColumnName("Device_Classroom_ID");
            entity.Property(e => e.SenderId)
                .HasMaxLength(450)
                .HasColumnName("Sender_ID");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.DeviceClassroom).WithMany(p => p.BrokenHistories)
                .HasForeignKey(d => d.DeviceClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Status_History_Class_Details_ID");

            entity.HasOne(d => d.Sender).WithMany(p => p.BrokenHistories)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Broken_History_AspNetUsers_Id");
        });

        modelBuilder.Entity<ClassDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ClassDetails_ID");

            entity.ToTable("Class_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdClassroom).HasColumnName("Id_Classroom");
            entity.Property(e => e.IdDevice).HasColumnName("Id_Device");

            entity.HasOne(d => d.IdClassroomNavigation).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.IdClassroom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassDetails_Classroom_ID");

            entity.HasOne(d => d.IdDeviceNavigation).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.IdDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassDetails_Device_ID");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Classroom_ID");

            entity.ToTable("Classroom");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdFloor).HasColumnName("ID_Floor");
            entity.Property(e => e.Name)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFloorNavigation).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.IdFloor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classroom_Floor_ID");
        });

        modelBuilder.Entity<ConfirmEquipmentVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Confirm_Equipment_Vouchers_ID");

            entity.ToTable("Confirm_Equipment_Vouchers");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdEquipmentVouchers).HasColumnName("ID_Equipment_Vouchers");
            entity.Property(e => e.IdUserConfirm)
                .HasMaxLength(450)
                .HasColumnName("ID_User_Confirm");
            entity.Property(e => e.Reason).HasMaxLength(450);

            entity.HasOne(d => d.IdEquipmentVouchersNavigation).WithMany(p => p.ConfirmEquipmentVouchers)
                .HasForeignKey(d => d.IdEquipmentVouchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Confirm_Equipment_Vouchers_Equipment_Vouchers_ID");

            entity.HasOne(d => d.IdUserConfirmNavigation).WithMany(p => p.ConfirmEquipmentVouchers)
                .HasForeignKey(d => d.IdUserConfirm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Confirm_Equipment_Vouchers_AspNetUsers_Id");
        });

        modelBuilder.Entity<Confirmation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Confirmation _ID");

            entity.ToTable("Confirmation ");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdNotification).HasColumnName("ID_Notification");
            entity.Property(e => e.IdUserConfirms)
                .HasMaxLength(450)
                .HasColumnName("ID_User_Confirms");
            entity.Property(e => e.Reason).HasMaxLength(150);

            entity.HasOne(d => d.IdNotificationNavigation).WithMany(p => p.Confirmations)
                .HasForeignKey(d => d.IdNotification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Confirmation _Notification_ID");

            entity.HasOne(d => d.IdUserConfirmsNavigation).WithMany(p => p.Confirmations)
                .HasForeignKey(d => d.IdUserConfirms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Confirmation _AspNetUsers_Id");
        });

        modelBuilder.Entity<DetailsEquipmentVoucher>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Details_Equipment_Vouchers");

            entity.Property(e => e.IdDevice).HasColumnName("ID_Device");
            entity.Property(e => e.IdVouchers).HasColumnName("ID_Vouchers");

            entity.HasOne(d => d.IdDeviceNavigation).WithMany()
                .HasForeignKey(d => d.IdDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Details_Equipment_Vouchers_Device_ID");

            entity.HasOne(d => d.IdVouchersNavigation).WithMany()
                .HasForeignKey(d => d.IdVouchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Details_Equipment_Vouchers_Equipment_Vouchers_ID");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Device_ID");

            entity.ToTable("Device");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdDeviceClassfication).HasColumnName("Id_Device_Classfication");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdDeviceClassficationNavigation).WithMany(p => p.Devices)
                .HasForeignKey(d => d.IdDeviceClassfication)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_DeviceClassification_ID");

            entity.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.Devices)
                .HasForeignKey(d => d.IdSupplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_Supplier_ID");
        });

        modelBuilder.Entity<DeviceClassfication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DeviceClassification_ID");

            entity.ToTable("Device_Classfication");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(80);
        });

        modelBuilder.Entity<DeviceRepairProcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Device_Repair_Progress_ID");

            entity.ToTable("Device_Repair_Process");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChangeTime)
                .HasColumnType("datetime")
                .HasColumnName("Change_Time");
            entity.Property(e => e.IdProcessDevice).HasColumnName("Id_Process_Device");
            entity.Property(e => e.IdRepair).HasColumnName("Id_Repair");

            entity.HasOne(d => d.IdProcessDeviceNavigation).WithMany(p => p.DeviceRepairProcesses)
                .HasForeignKey(d => d.IdProcessDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeviceLocationHistory_Situation_ID");

            entity.HasOne(d => d.IdRepairNavigation).WithMany(p => p.DeviceRepairProcesses)
                .HasForeignKey(d => d.IdRepair)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_Repair_Progress_Repair_ID");
        });

        modelBuilder.Entity<DeviceWarehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Device_Inventory_ID");

            entity.ToTable("Device_Warehouse");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdDevice).HasColumnName("Id_Device");
            entity.Property(e => e.IdWarehouse).HasColumnName("Id_Warehouse");
            entity.Property(e => e.Quantity).HasColumnName("Quantity ");

            entity.HasOne(d => d.IdDeviceNavigation).WithMany(p => p.DeviceWarehouses)
                .HasForeignKey(d => d.IdDevice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_Warehouse_Device_ID");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.DeviceWarehouses)
                .HasForeignKey(d => d.IdWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_Warehouse_Warehouse_ID");
        });

        modelBuilder.Entity<EquipmentVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Equipment_Vouchers_ID");

            entity.ToTable("Equipment_Vouchers");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate)
                .HasColumnType("date")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.IdRequester)
                .HasMaxLength(450)
                .HasColumnName("ID_Requester");
            entity.Property(e => e.Reason).HasMaxLength(450);

            entity.HasOne(d => d.IdRequesterNavigation).WithMany(p => p.EquipmentVouchers)
                .HasForeignKey(d => d.IdRequester)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipment_Vouchers_AspNetUsers_Id");
        });

        modelBuilder.Entity<Floor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Floor_ID");

            entity.ToTable("Floor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdArea).HasColumnName("ID_AREA");
            entity.Property(e => e.Name)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Floors)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Floor_AREA_ID");
        });

        modelBuilder.Entity<ImportExportWarehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_WarehouseDetails_ID");

            entity.ToTable("Import_Export_Warehouse");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IdDeviceWarehouse).HasColumnName("Id_Device_Warehouse");
            entity.Property(e => e.IsImport).HasColumnName("Is_Import");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.IdDeviceWarehouseNavigation).WithMany(p => p.ImportExportWarehouses)
                .HasForeignKey(d => d.IdDeviceWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Import_Export_Warehouse_Device_Warehouse_ID");

            entity.HasOne(d => d.User).WithMany(p => p.ImportExportWarehouses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Import_Export_Warehouse_AspNetUsers_Id");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Notification_ID");

            entity.ToTable("Notification");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasDefaultValueSql("((1))");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.IdBrokenHistory).HasColumnName("ID_Broken_History");
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBrokenHistoryNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdBrokenHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_Broken_History_ID");
        });

        modelBuilder.Entity<ProcessDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Situation_ID");

            entity.ToTable("Process_Device");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Repair__3214EC27A52E06BB");

            entity.ToTable("Repair");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeviceClassroomId).HasColumnName("Device_Classroom_ID");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("End_Date");
            entity.Property(e => e.RepairSolution)
                .HasMaxLength(255)
                .HasColumnName("Repair_Solution");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("Start_Date");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.DeviceClassroom).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.DeviceClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Repair__Device_C__251C81ED");

            entity.HasOne(d => d.User).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Repair_AspNetUsers_Id");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Supplier_ID");

            entity.ToTable("Supplier");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Warehouse_ID");

            entity.ToTable("Warehouse");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.IdArea).HasColumnName("ID_AREA");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("NAME");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_AREA_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
