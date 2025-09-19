using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class EntityFrameworkContext : DbContext
{
    public EntityFrameworkContext()
    {
    }

    public EntityFrameworkContext(DbContextOptions<EntityFrameworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgenciaUsuario> AgenciaUsuarios { get; set; }

    public virtual DbSet<Agencium> Agencia { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<CargoUsuario> CargoUsuarios { get; set; }

    public virtual DbSet<GerenciaUsuario> GerenciaUsuarios { get; set; }

    public virtual DbSet<Gerencium> Gerencia { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuExterno> MenuExternos { get; set; }

    public virtual DbSet<MenuPerfil> MenuPerfils { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<PerfilUsuario> PerfilUsuarios { get; set; }

    public virtual DbSet<Sistema> Sistemas { get; set; }

    public virtual DbSet<SistemaUsuario> SistemaUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioEstado> UsuarioEstados { get; set; }

    public virtual DbSet<Variable> Variables { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgenciaUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("agencia_usuario_pkey");

            entity.ToTable("agencia_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgenciaId).HasColumnName("agencia_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Esprincipal)
                .HasDefaultValue(false)
                .HasColumnName("esprincipal");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        });

        modelBuilder.Entity<Agencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("agencia_pkey");

            entity.ToTable("agencia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cargo_pkey");

            entity.ToTable("cargo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo1)
                .HasMaxLength(150)
                .HasColumnName("cargo");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.GerenciaId).HasColumnName("gerencia_id");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<CargoUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cargo_usuario_pkey");

            entity.ToTable("cargo_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CargoId).HasColumnName("cargo_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        });

        modelBuilder.Entity<GerenciaUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gerencia_usuario_pkey");

            entity.ToTable("gerencia_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.GerenciaId).HasColumnName("gerencia_id");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        });

        modelBuilder.Entity<Gerencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gerencia_pkey");

            entity.ToTable("gerencia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Gerencia)
                .HasMaxLength(50)
                .HasColumnName("gerencia");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_pkey");

            entity.ToTable("menu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Icon)
                .HasMaxLength(200)
                .HasColumnName("icon");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Menu1)
                .HasMaxLength(200)
                .HasColumnName("menu");
            entity.Property(e => e.MenuPadreId).HasColumnName("menu_padre_id");
            entity.Property(e => e.MenuTipoId).HasColumnName("menu_tipo_id");
            entity.Property(e => e.Orden).HasColumnName("orden");
            entity.Property(e => e.SistemaId).HasColumnName("sistema_id");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Url)
                .HasMaxLength(400)
                .HasColumnName("url");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<MenuExterno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_externo_pkey");

            entity.ToTable("menu_externo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Menu)
                .HasMaxLength(100)
                .HasColumnName("menu");
            entity.Property(e => e.MenuPadreId).HasColumnName("menu_padre_id");
            entity.Property(e => e.Orden).HasColumnName("orden");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<MenuPerfil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_perfil_pkey");

            entity.ToTable("menu_perfil");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.PerfilId).HasColumnName("perfil_id");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("perfil_pkey");

            entity.ToTable("perfil");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Perfil1)
                .HasMaxLength(100)
                .HasColumnName("perfil");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<PerfilUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("perfil_usuario_pkey");

            entity.ToTable("perfil_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.PerfilId).HasColumnName("perfil_id");
            entity.Property(e => e.SistemaId).HasColumnName("sistema_id");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sistema_pkey");

            entity.ToTable("sistema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("url");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<SistemaUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sistema_usuario_pkey");

            entity.ToTable("sistema_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.SistemaId).HasColumnName("sistema_id");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(200)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .HasColumnName("correo");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.DocumentoNumero)
                .HasMaxLength(20)
                .HasColumnName("documento_numero");
            entity.Property(e => e.DocumentoTipoId).HasColumnName("documento_tipo_id");
            entity.Property(e => e.Esbloqueado)
                .HasDefaultValue(false)
                .HasColumnName("esbloqueado");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(100)
                .HasColumnName("usuario");
            entity.Property(e => e.UsuarioEstadoId).HasColumnName("usuario_estado_id");
        });

        modelBuilder.Entity<UsuarioEstado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_estado_pkey");

            entity.ToTable("usuario_estado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
        });

        modelBuilder.Entity<Variable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("variable_pkey");

            entity.ToTable("variable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usercreatedid).HasColumnName("usercreatedid");
            entity.Property(e => e.Usermodifiedid).HasColumnName("usermodifiedid");
            entity.Property(e => e.Valor).HasColumnName("valor");
            entity.Property(e => e.Variable1)
                .HasMaxLength(50)
                .HasColumnName("variable");
            entity.Property(e => e.VariableTipo)
                .HasMaxLength(20)
                .HasColumnName("variable_tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
