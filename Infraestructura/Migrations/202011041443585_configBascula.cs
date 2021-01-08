namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configBascula : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configuracion", "ActivarBascula", c => c.Boolean(nullable: false));
            AddColumn("dbo.Configuracion", "EtiquetaPorPeso", c => c.Boolean(nullable: false));
            AddColumn("dbo.Configuracion", "EtiquetaPorPrecio", c => c.Boolean(nullable: false));
            AddColumn("dbo.Configuracion", "CodigoBascula", c => c.String(maxLength: 400, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configuracion", "CodigoBascula");
            DropColumn("dbo.Configuracion", "EtiquetaPorPrecio");
            DropColumn("dbo.Configuracion", "EtiquetaPorPeso");
            DropColumn("dbo.Configuracion", "ActivarBascula");
        }
    }
}
