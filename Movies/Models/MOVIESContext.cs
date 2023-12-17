using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Movies.Models
{
    public partial class MOVIESContext : DbContext
    {
        public MOVIESContext()
        {
        }

        public MOVIESContext(DbContextOptions<MOVIESContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; } = null!;
        public virtual DbSet<Cast> Casts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Episode> Episodes { get; set; } = null!;
        public virtual DbSet<FeatureFilm> FeatureFilms { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieCategory> MovieCategories { get; set; } = null!;
        public virtual DbSet<Nation> Nations { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;
        public virtual DbSet<StoreVideo> StoreVideos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        private string GetConnectionString()

        {

            IConfiguration config = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

            .AddJsonFile("appsettings.json", true, true)

            .Build();

            var strConn = config["ConnectionStrings:MyCnn"];

            return strConn;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("Actor");

                entity.Property(e => e.ActorId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ActorID");

                entity.Property(e => e.DoB).HasColumnType("datetime");

                entity.Property(e => e.LinkImage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameActor)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NationId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NationID");

                entity.HasOne(d => d.Nation)
                    .WithMany()
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK__Actor__NationID__4E88ABD4");
            });

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.MovieId })
                    .HasName("PK__Cast__E30EC3682DF2123D");

                entity.ToTable("Cast");

                entity.Property(e => e.ActorId).HasColumnName("ActorID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.CharacterName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cast__ActorID__5CD6CB2B");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cast__MovieID__5DCAEF64");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.ToTable("Episode");

                entity.Property(e => e.EpisodeId).HasColumnName("EpisodeID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SeasonId).HasColumnName("SeasonID");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK__Episode__SeasonI__6383C8BA");
            });

            modelBuilder.Entity<FeatureFilm>(entity =>
            {
                entity.HasKey(e => e.FeatureId)
                    .HasName("PK__featuref__82230BC90FB02B10");

                entity.ToTable("FeatureFilm");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LinkMovie)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LinkThumbnail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LinkTrailer)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NationId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NationID");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VietnamName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Feature)
                    .WithMany()
                    .HasForeignKey(d => d.FeatureId)
                    .HasConstraintName("FK__Movies__FeatureI__5629CD9C");

                entity.HasOne(d => d.Nation)
                    .WithMany()
                    .HasForeignKey(d => d.NationId)
                    .HasConstraintName("FK__Movies__NationID__571DF1D5");
            });

            modelBuilder.Entity<MovieCategory>(entity =>
            {
                entity.HasKey(mc => new { mc.MovieId, mc.CategoryId });

                entity.ToTable("MovieCategory");

                //entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                //entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__movie_cat__Categ__59063A47");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieCategories)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__movie_cat__Movie__59FA5E80");
            });

            modelBuilder.Entity<Nation>(entity =>
            {
                entity.ToTable("Nation");

                entity.Property(e => e.NationId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NationID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("Season");

                entity.Property(e => e.SeasonId).HasColumnName("SeasonID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Seasons)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Season__MovieID__60A75C0F");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("Video");

                entity.Property(e => e.VideoId).HasColumnName("VideoID");

                entity.Property(e => e.EpisodeId).HasColumnName("EpisodeID");

                entity.Property(e => e.Link)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Episode)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.EpisodeId)
                    .HasConstraintName("FK__video__EpisodeID__66603565");
            });

            modelBuilder.Entity<StoreVideo>(entity =>
            {
                entity.HasKey(e => e.StoreVideoId);

                entity.ToTable("StoreVideo");

                entity.Property(e => e.Index).HasColumnName("Index");

                entity.Property(e => e.VideoData).HasColumnName("VideoData");

                entity.Property(e => e.VideoId).HasColumnName("VideoID");

                entity.HasOne(d => d.Video)
                    .WithMany()
                    .HasForeignKey(d => d.VideoId)
                    .HasConstraintName("FK__StoreVide__Video__6FE99F9F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
