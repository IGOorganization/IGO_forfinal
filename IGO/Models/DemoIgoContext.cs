using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IGO.Models
{
    public partial class DemoIgoContext : DbContext
    {
        public DemoIgoContext()
        {
        }

        public DemoIgoContext(DbContextOptions<DemoIgoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCategory> TCategories { get; set; }
        public virtual DbSet<TCity> TCities { get; set; }
        public virtual DbSet<TCollection> TCollections { get; set; }
        public virtual DbSet<TCoupon> TCoupons { get; set; }
        public virtual DbSet<TCustomer> TCustomers { get; set; }
        public virtual DbSet<TDiscount> TDiscounts { get; set; }
        public virtual DbSet<TFeedbackManagement> TFeedbackManagements { get; set; }
        public virtual DbSet<TOrder> TOrders { get; set; }
        public virtual DbSet<TOrderDetail> TOrderDetails { get; set; }
        public virtual DbSet<TPayment> TPayments { get; set; }
        public virtual DbSet<TPhotoSite> TPhotoSites { get; set; }
        public virtual DbSet<TProduct> TProducts { get; set; }
        public virtual DbSet<TProductsPhoto> TProductsPhotos { get; set; }
        public virtual DbSet<TSeat> TSeats { get; set; }
        public virtual DbSet<TShipper> TShippers { get; set; }
        public virtual DbSet<TShoppingCart> TShoppingCarts { get; set; }
        public virtual DbSet<TStatus> TStatuses { get; set; }
        public virtual DbSet<TSubCategory> TSubCategories { get; set; }
        public virtual DbSet<TSupplier> TSuppliers { get; set; }
        public virtual DbSet<TTicketAndProduct> TTicketAndProducts { get; set; }
        public virtual DbSet<TTicketType> TTicketTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DemoIgo;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<TCategory>(entity =>
            {
                entity.HasKey(e => e.FCategoryId)
                    .HasName("PK_Category_1");

                entity.ToTable("tCategory");

                entity.Property(e => e.FCategoryId).HasColumnName("fCategoryID");

                entity.Property(e => e.FCategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("fCategoryName");
            });

            modelBuilder.Entity<TCity>(entity =>
            {
                entity.HasKey(e => e.FCityId)
                    .HasName("PK_City");

                entity.ToTable("tCity");

                entity.Property(e => e.FCityId).HasColumnName("fCityID");

                entity.Property(e => e.FCityName)
                    .HasMaxLength(20)
                    .HasColumnName("fCityName");

                entity.Property(e => e.FCityPhotoPath)
                    .HasMaxLength(50)
                    .HasColumnName("fCityPhotoPath");
            });

            modelBuilder.Entity<TCollection>(entity =>
            {
                entity.HasKey(e => e.FCollectionId)
                    .HasName("PK_Collection");

                entity.ToTable("tCollection");

                entity.Property(e => e.FCollectionId).HasColumnName("fCollectionID");

                entity.Property(e => e.FCollectionDate)
                    .HasMaxLength(50)
                    .HasColumnName("fCollectionDate");

                entity.Property(e => e.FCollectionState).HasColumnName("fCollectionState");

                entity.Property(e => e.FCustomerId).HasColumnName("fCustomerID");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.HasOne(d => d.FCustomer)
                    .WithMany(p => p.TCollections)
                    .HasForeignKey(d => d.FCustomerId)
                    .HasConstraintName("FK_tCollection_tCustomers");

                entity.HasOne(d => d.FProduct)
                    .WithMany(p => p.TCollections)
                    .HasForeignKey(d => d.FProductId)
                    .HasConstraintName("FK_tCollection_tProducts");
            });

            modelBuilder.Entity<TCoupon>(entity =>
            {
                entity.HasKey(e => e.FCouponId);

                entity.ToTable("tCoupon");

                entity.Property(e => e.FCouponId).HasColumnName("fCouponID");

                entity.Property(e => e.FCouponName)
                    .HasMaxLength(50)
                    .HasColumnName("fCouponName");

                entity.Property(e => e.FDeadTime)
                    .HasMaxLength(50)
                    .HasColumnName("fDeadTime");

                entity.Property(e => e.FDiscount)
                    .HasMaxLength(50)
                    .HasColumnName("fDiscount");

                entity.Property(e => e.FProductId1).HasColumnName("fProductID1");

                entity.Property(e => e.FProductId2).HasColumnName("fProductID2");

                entity.Property(e => e.FProductId3).HasColumnName("fProductID3");

                entity.Property(e => e.FProductId4).HasColumnName("fProductID4");

                entity.Property(e => e.FProductId5).HasColumnName("fProductID5");

                entity.Property(e => e.FTimeOut).HasColumnName("fTimeOut");

                entity.HasOne(d => d.FProductId1Navigation)
                    .WithMany(p => p.TCouponFProductId1Navigations)
                    .HasForeignKey(d => d.FProductId1)
                    .HasConstraintName("FK_tCoupon_tProducts");

                entity.HasOne(d => d.FProductId2Navigation)
                    .WithMany(p => p.TCouponFProductId2Navigations)
                    .HasForeignKey(d => d.FProductId2)
                    .HasConstraintName("FK_tCoupon_tProducts1");

                entity.HasOne(d => d.FProductId3Navigation)
                    .WithMany(p => p.TCouponFProductId3Navigations)
                    .HasForeignKey(d => d.FProductId3)
                    .HasConstraintName("FK_tCoupon_tProducts2");

                entity.HasOne(d => d.FProductId4Navigation)
                    .WithMany(p => p.TCouponFProductId4Navigations)
                    .HasForeignKey(d => d.FProductId4)
                    .HasConstraintName("FK_tCoupon_tProducts3");

                entity.HasOne(d => d.FProductId5Navigation)
                    .WithMany(p => p.TCouponFProductId5Navigations)
                    .HasForeignKey(d => d.FProductId5)
                    .HasConstraintName("FK_tCoupon_tProducts4");
            });

            modelBuilder.Entity<TCustomer>(entity =>
            {
                entity.HasKey(e => e.FCustomerId)
                    .HasName("PK_Customers");

                entity.ToTable("tCustomers");

                entity.Property(e => e.FCustomerId).HasColumnName("fCustomerID");

                entity.Property(e => e.FAddress)
                    .HasMaxLength(50)
                    .HasColumnName("fAddress");

                entity.Property(e => e.FBirth)
                    .HasMaxLength(50)
                    .HasColumnName("fBirth");

                entity.Property(e => e.FCityId).HasColumnName("fCityID");

                entity.Property(e => e.FEmail)
                    .HasMaxLength(50)
                    .HasColumnName("fEmail");

                entity.Property(e => e.FFirstName)
                    .HasMaxLength(10)
                    .HasColumnName("fFirstName");

                entity.Property(e => e.FGender)
                    .HasMaxLength(10)
                    .HasColumnName("fGender");

                entity.Property(e => e.FLastName)
                    .HasMaxLength(10)
                    .HasColumnName("fLastName");

                entity.Property(e => e.FPassword)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("fPassword");

                entity.Property(e => e.FPhone)
                    .HasMaxLength(10)
                    .HasColumnName("fPhone");

                entity.Property(e => e.FSignUpDate)
                    .HasMaxLength(50)
                    .HasColumnName("fSignUpDate");

                entity.Property(e => e.FUserPhoto)
                    .HasMaxLength(50)
                    .HasColumnName("fUserPhoto");

                entity.HasOne(d => d.FCity)
                    .WithMany(p => p.TCustomers)
                    .HasForeignKey(d => d.FCityId)
                    .HasConstraintName("FK_tCustomers_tCity");
            });

            modelBuilder.Entity<TDiscount>(entity =>
            {
                entity.HasKey(e => e.FDiscountId);

                entity.ToTable("tDiscount");

                entity.Property(e => e.FDiscountId).HasColumnName("fDiscountID");

                entity.Property(e => e.FDeadTime)
                    .HasMaxLength(50)
                    .HasColumnName("fDeadTime");

                entity.Property(e => e.FDiscount)
                    .HasMaxLength(50)
                    .HasColumnName("fDiscount");

                entity.Property(e => e.FDiscountName)
                    .HasMaxLength(50)
                    .HasColumnName("fDiscountName");

                entity.Property(e => e.FTimeOut)
                    .HasMaxLength(50)
                    .HasColumnName("fTimeOut");
            });

            modelBuilder.Entity<TFeedbackManagement>(entity =>
            {
                entity.HasKey(e => e.FFeedbackId)
                    .HasName("PK_FeedbackManagement");

                entity.ToTable("tFeedbackManagement");

                entity.Property(e => e.FFeedbackId).HasColumnName("fFeedbackID");

                entity.Property(e => e.FCustomerId).HasColumnName("fCustomerID");

                entity.Property(e => e.FFeedbackContent)
                    .HasMaxLength(250)
                    .HasColumnName("fFeedbackContent");

                entity.Property(e => e.FFeedbackDate)
                    .HasMaxLength(50)
                    .HasColumnName("fFeedbackDate");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FRanking).HasColumnName("fRanking");

                entity.HasOne(d => d.FCustomer)
                    .WithMany(p => p.TFeedbackManagements)
                    .HasForeignKey(d => d.FCustomerId)
                    .HasConstraintName("FK_tFeedbackManagement_tCustomers");

                entity.HasOne(d => d.FProduct)
                    .WithMany(p => p.TFeedbackManagements)
                    .HasForeignKey(d => d.FProductId)
                    .HasConstraintName("FK_tFeedbackManagement_tProducts");
            });

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.HasKey(e => e.FOrderId)
                    .HasName("PK_Orders");

                entity.ToTable("tOrders");

                entity.Property(e => e.FOrderId).HasColumnName("fOrderID");

                entity.Property(e => e.FCustomerId).HasColumnName("fCustomerID");

                entity.Property(e => e.FOrderDate)
                    .HasMaxLength(50)
                    .HasColumnName("fOrderDate");

                entity.Property(e => e.FOrderNum)
                    .HasMaxLength(50)
                    .HasColumnName("fOrderNum");

                entity.Property(e => e.FPayTypeId).HasColumnName("fPayTypeID");

                entity.Property(e => e.FShippedDate)
                    .HasMaxLength(50)
                    .HasColumnName("fShippedDate");

                entity.Property(e => e.FShipperId).HasColumnName("fShipperID");

                entity.Property(e => e.FStatusId).HasColumnName("fStatusID");

                entity.Property(e => e.FTotalPrice)
                    .HasColumnType("money")
                    .HasColumnName("fTotalPrice");

                entity.HasOne(d => d.FCustomer)
                    .WithMany(p => p.TOrders)
                    .HasForeignKey(d => d.FCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tOrders_tCustomers");

                entity.HasOne(d => d.FPayType)
                    .WithMany(p => p.TOrders)
                    .HasForeignKey(d => d.FPayTypeId)
                    .HasConstraintName("FK_tOrders_tPayment");

                entity.HasOne(d => d.FShipper)
                    .WithMany(p => p.TOrders)
                    .HasForeignKey(d => d.FShipperId)
                    .HasConstraintName("FK_tOrders_tShipper");

                entity.HasOne(d => d.FStatus)
                    .WithMany(p => p.TOrders)
                    .HasForeignKey(d => d.FStatusId)
                    .HasConstraintName("FK_tOrders_tStatus");
            });

            modelBuilder.Entity<TOrderDetail>(entity =>
            {
                entity.HasKey(e => e.FOrderDetailsId)
                    .HasName("PK_OrderDetail");

                entity.ToTable("tOrderDetail");

                entity.Property(e => e.FOrderDetailsId).HasColumnName("fOrderDetailsID");

                entity.Property(e => e.FBookingTime)
                    .HasMaxLength(50)
                    .HasColumnName("fBookingTime");

                entity.Property(e => e.FCouponId).HasColumnName("fCouponID");

                entity.Property(e => e.FOrderId).HasColumnName("fOrderID");

                entity.Property(e => e.FPrice)
                    .HasColumnType("money")
                    .HasColumnName("fPrice");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FQuantity).HasColumnName("fQuantity");

                entity.Property(e => e.FTicketId).HasColumnName("fTicketID");

                entity.HasOne(d => d.FCoupon)
                    .WithMany(p => p.TOrderDetails)
                    .HasForeignKey(d => d.FCouponId)
                    .HasConstraintName("FK_tOrderDetail_tCoupon");

                entity.HasOne(d => d.FOrder)
                    .WithMany(p => p.TOrderDetails)
                    .HasForeignKey(d => d.FOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tOrderDetail_tOrders");

                entity.HasOne(d => d.FProduct)
                    .WithMany(p => p.TOrderDetails)
                    .HasForeignKey(d => d.FProductId)
                    .HasConstraintName("FK_tOrderDetail_tProducts");

                entity.HasOne(d => d.FTicket)
                    .WithMany(p => p.TOrderDetails)
                    .HasForeignKey(d => d.FTicketId)
                    .HasConstraintName("FK_tOrderDetail_tTicketTypes");
            });

            modelBuilder.Entity<TPayment>(entity =>
            {
                entity.HasKey(e => e.FPayTypeId)
                    .HasName("PK_PayType");

                entity.ToTable("tPayment");

                entity.Property(e => e.FPayTypeId).HasColumnName("fPayTypeID");

                entity.Property(e => e.FPayType)
                    .HasMaxLength(20)
                    .HasColumnName("fPayType");
            });

            modelBuilder.Entity<TPhotoSite>(entity =>
            {
                entity.HasKey(e => e.FPhotoSiteId);

                entity.ToTable("tPhotoSite");

                entity.Property(e => e.FPhotoSiteId).HasColumnName("fPhotoSiteID");

                entity.Property(e => e.FPhotoplace)
                    .HasMaxLength(50)
                    .HasColumnName("fPhotoplace");
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasKey(e => e.FProductId)
                    .HasName("PK_Products");

                entity.ToTable("tProducts");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FAddress)
                    .HasMaxLength(50)
                    .HasColumnName("fAddress");

                entity.Property(e => e.FCityId).HasColumnName("fCityID");

                entity.Property(e => e.FDiscountOrNot).HasColumnName("fDiscountOrNot");

                entity.Property(e => e.FEndTime)
                    .HasMaxLength(50)
                    .HasColumnName("fEndTime");

                entity.Property(e => e.FIntroduction)
                    .HasMaxLength(250)
                    .HasColumnName("fIntroduction");

                entity.Property(e => e.FProductName)
                    .HasMaxLength(50)
                    .HasColumnName("fProductName");

                entity.Property(e => e.FQuantity).HasColumnName("fQuantity");

                entity.Property(e => e.FStartTime)
                    .HasMaxLength(50)
                    .HasColumnName("fStartTime");

                entity.Property(e => e.FSubCategoryId).HasColumnName("fSubCategoryID");

                entity.Property(e => e.FSupplierId).HasColumnName("fSupplierID");

                entity.HasOne(d => d.FCity)
                    .WithMany(p => p.TProducts)
                    .HasForeignKey(d => d.FCityId)
                    .HasConstraintName("FK_tProducts_tCity");

                entity.HasOne(d => d.FSubCategory)
                    .WithMany(p => p.TProducts)
                    .HasForeignKey(d => d.FSubCategoryId)
                    .HasConstraintName("FK_tProducts_tSubCategory");

                entity.HasOne(d => d.FSupplier)
                    .WithMany(p => p.TProducts)
                    .HasForeignKey(d => d.FSupplierId)
                    .HasConstraintName("FK_tProducts_tSupplier");
            });

            modelBuilder.Entity<TProductsPhoto>(entity =>
            {
                entity.HasKey(e => e.FProductPhotoId)
                    .HasName("PK_ProductsPhoto");

                entity.ToTable("tProductsPhoto");

                entity.Property(e => e.FProductPhotoId).HasColumnName("fProductPhotoID");

                entity.Property(e => e.FPhotoPath)
                    .HasMaxLength(50)
                    .HasColumnName("fPhotoPath");

                entity.Property(e => e.FPhotoSiteId).HasColumnName("fPhotoSiteID");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.HasOne(d => d.FPhotoSite)
                    .WithMany(p => p.TProductsPhotos)
                    .HasForeignKey(d => d.FPhotoSiteId)
                    .HasConstraintName("FK_tProductsPhoto_tPhotoSite");

                entity.HasOne(d => d.FProduct)
                    .WithMany(p => p.TProductsPhotos)
                    .HasForeignKey(d => d.FProductId)
                    .HasConstraintName("FK_tProductsPhoto_tProducts");
            });

            modelBuilder.Entity<TSeat>(entity =>
            {
                entity.HasKey(e => e.FSeatId)
                    .HasName("PK_Seat");

                entity.ToTable("tSeat");

                entity.Property(e => e.FSeatId).HasColumnName("fSeatID");

                entity.Property(e => e.FOrderDetailId).HasColumnName("fOrderDetailID");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FSeatName)
                    .HasMaxLength(10)
                    .HasColumnName("fSeatName");

                entity.HasOne(d => d.FOrderDetail)
                    .WithMany(p => p.TSeats)
                    .HasForeignKey(d => d.FOrderDetailId)
                    .HasConstraintName("FK_tSeat_tOrderDetail");

                entity.HasOne(d => d.FProduct)
                    .WithMany(p => p.TSeats)
                    .HasForeignKey(d => d.FProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tSeat_tProducts");
            });

            modelBuilder.Entity<TShipper>(entity =>
            {
                entity.HasKey(e => e.FShipperId)
                    .HasName("PK_Shipper");

                entity.ToTable("tShipper");

                entity.Property(e => e.FShipperId).HasColumnName("fShipperID");

                entity.Property(e => e.FPhone)
                    .HasMaxLength(20)
                    .HasColumnName("fPhone");

                entity.Property(e => e.FShipperName)
                    .HasMaxLength(30)
                    .HasColumnName("fShipperName");
            });

            modelBuilder.Entity<TShoppingCart>(entity =>
            {
                entity.HasKey(e => e.FShoppingCartId)
                    .HasName("PK_Temp");

                entity.ToTable("tShoppingCart");

                entity.Property(e => e.FShoppingCartId).HasColumnName("fShoppingCartID");

                entity.Property(e => e.FBookingTime)
                    .HasMaxLength(50)
                    .HasColumnName("fBookingTime");

                entity.Property(e => e.FCouponId).HasColumnName("fCouponID");

                entity.Property(e => e.FCustomerId).HasColumnName("fCustomerID");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FQuantity).HasColumnName("fQuantity");

                entity.Property(e => e.FSeats)
                    .HasMaxLength(100)
                    .HasColumnName("fSeats");

                entity.Property(e => e.FTempOrder)
                    .HasMaxLength(10)
                    .HasColumnName("fTempOrder");

                entity.Property(e => e.FTicketId).HasColumnName("fTicketID");

                entity.Property(e => e.FTotalPrice)
                    .HasColumnType("money")
                    .HasColumnName("fTotalPrice");

                entity.HasOne(d => d.FCoupon)
                    .WithMany(p => p.TShoppingCarts)
                    .HasForeignKey(d => d.FCouponId)
                    .HasConstraintName("FK_tShoppingCart_tCoupon");

                entity.HasOne(d => d.FCustomer)
                    .WithMany(p => p.TShoppingCarts)
                    .HasForeignKey(d => d.FCustomerId)
                    .HasConstraintName("FK_tShoppingCart_tCustomers");

                entity.HasOne(d => d.FProduct)
                    .WithMany(p => p.TShoppingCarts)
                    .HasForeignKey(d => d.FProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tShoppingCart_tProducts");
            });

            modelBuilder.Entity<TStatus>(entity =>
            {
                entity.HasKey(e => e.FStatusId)
                    .HasName("PK_Status");

                entity.ToTable("tStatus");

                entity.Property(e => e.FStatusId).HasColumnName("fStatusID");

                entity.Property(e => e.FStatusName)
                    .HasMaxLength(10)
                    .HasColumnName("fStatusName");
            });

            modelBuilder.Entity<TSubCategory>(entity =>
            {
                entity.HasKey(e => e.FSubCategoryId)
                    .HasName("PK_Category");

                entity.ToTable("tSubCategory");

                entity.Property(e => e.FSubCategoryId).HasColumnName("fSubCategoryID");

                entity.Property(e => e.FCategoryId).HasColumnName("fCategoryID");

                entity.Property(e => e.FSubCategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("fSubCategoryName");

                entity.Property(e => e.FSubCategoryPath)
                    .HasMaxLength(50)
                    .HasColumnName("fSubCategoryPath");

                entity.HasOne(d => d.FCategory)
                    .WithMany(p => p.TSubCategories)
                    .HasForeignKey(d => d.FCategoryId)
                    .HasConstraintName("FK_tSubCategory_tCategory");
            });

            modelBuilder.Entity<TSupplier>(entity =>
            {
                entity.HasKey(e => e.FSupplierId)
                    .HasName("PK_Supplier");

                entity.ToTable("tSupplier");

                entity.Property(e => e.FSupplierId).HasColumnName("fSupplierID");

                entity.Property(e => e.FAddress)
                    .HasMaxLength(50)
                    .HasColumnName("fAddress");

                entity.Property(e => e.FCompanyName)
                    .HasMaxLength(30)
                    .HasColumnName("fCompanyName");

                entity.Property(e => e.FPhone)
                    .HasMaxLength(20)
                    .HasColumnName("fPhone");
            });

            modelBuilder.Entity<TTicketAndProduct>(entity =>
            {
                entity.HasKey(e => e.FTicketAndProductId)
                    .HasName("PK_TicketAndProduct");

                entity.ToTable("tTicketAndProduct");

                entity.Property(e => e.FTicketAndProductId).HasColumnName("fTicketAndProductID");

                entity.Property(e => e.FPrice)
                    .HasColumnType("money")
                    .HasColumnName("fPrice");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FTicketId).HasColumnName("fTicketID");

                entity.HasOne(d => d.FProduct)
                    .WithMany(p => p.TTicketAndProducts)
                    .HasForeignKey(d => d.FProductId)
                    .HasConstraintName("FK_tTicketAndProduct_tProducts");

                entity.HasOne(d => d.FTicket)
                    .WithMany(p => p.TTicketAndProducts)
                    .HasForeignKey(d => d.FTicketId)
                    .HasConstraintName("FK_tTicketAndProduct_tTicketTypes");
            });

            modelBuilder.Entity<TTicketType>(entity =>
            {
                entity.HasKey(e => e.FTicketId)
                    .HasName("PK_TicketTypes");

                entity.ToTable("tTicketTypes");

                entity.Property(e => e.FTicketId).HasColumnName("fTicketID");

                entity.Property(e => e.FSubCategoryId).HasColumnName("fSubCategoryID");

                entity.Property(e => e.FTicketName)
                    .HasMaxLength(10)
                    .HasColumnName("fTicketName");

                entity.HasOne(d => d.FSubCategory)
                    .WithMany(p => p.TTicketTypes)
                    .HasForeignKey(d => d.FSubCategoryId)
                    .HasConstraintName("FK_tTicketTypes_tSubCategory");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
