using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UtopiaCatering.Models;

namespace UtopiaCatering.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // WorkOrderDetails - WorkOrder relationship (Many-to-One)
            modelBuilder.Entity<WorkOrderDetails>()
                .HasOne(wod => wod.WorkOrder)
                .WithMany(wo => wo.WorkOrderDetails)
                .HasForeignKey(wod => wod.WoID)
                .OnDelete(DeleteBehavior.Cascade);

           

            // PurchaseOrderDetails - PurchaseOrder relationship (Many-to-One)
            modelBuilder.Entity<PurchaseOrderDetails>()
                .HasOne(pod => pod.PurchaseOrder)
                .WithMany(po => po.PurchaseOrderDetails)
                .HasForeignKey(pod => pod.PoID)
                .OnDelete(DeleteBehavior.Cascade);



            // Events - Organization relationship (Many-to-One)
            modelBuilder.Entity<Events>()
                .HasOne(e => e.Organization)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OrganizationID)
                .OnDelete(DeleteBehavior.Cascade);

            // Organizer - Organization relationship (Many-to-One)
            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.Organization)
                .WithMany(org => org.Organizers)
                .HasForeignKey(o => o.OrganizationID)
                .OnDelete(DeleteBehavior.Cascade);

            // PurchaseOrder - Organization relationship (Many-to-One)
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(po => po.Organization)
                .WithMany(o => o.PurchaseOrders)
                .HasForeignKey(po => po.OrganizationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemElements>()
                .HasOne(ie => ie.Items)
                .WithMany(i => i.ItemElements)
                .HasForeignKey(ie => ie.ItemID)
                .OnDelete(DeleteBehavior.Cascade); // If Item is deleted, its ItemElements should also be deleted

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Items> Items { get; set; }
        public DbSet<ItemElements> ItemElements { get; set; }
        public DbSet<Journals> Journals { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Organizer> Organizer { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public DbSet<WorkOrder> WorkOrder { get; set; }
        public DbSet<WorkOrderDetails> WorkOrderDetails { get; set; }
        public DbSet<WorkOrderWiseEvents> WorkOrderWiseEvents { get; set; }
    }
}
